using MicroondasDigital.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using MicroondasDigital.Domain.Security;
using MicroondasDigital.Domain.Interfaces.Repository;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using MicroondasDigital.Domain.Extensions.Exceptions;

namespace MicroondasDigital.Data.Repository
{
    public class AutenticacaoRepository : IAutenticacaoRepository
    {
        private readonly TokenConfigurations _tokenConfigurations;
        private string FilePath { get; }

        public AutenticacaoRepository(IOptions<TokenConfigurations> tokenConfigurations)
        {
            FilePath = "c:\\Temp\\Usuarios.Json";

            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, "[]");
            }

            _tokenConfigurations = tokenConfigurations.Value;
        }

        public bool SalvarNovoUsuario(string senha)
        {
            try
            {
                List<Usuario> lista;

                if (File.Exists(FilePath))
                {
                    using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string json = sr.ReadToEnd();
                        lista = JsonConvert.DeserializeObject<List<Usuario>>(json);
                    }
                }
                else
                {
                    lista = new List<Usuario>();
                }

                string senhaCriptografa = CriptografaSHA256.CriptografarSenha(senha);

                Usuario usuario = new Usuario(Guid.NewGuid(),senhaCriptografa); 

                lista.Add(usuario);
                string updatedJson = JsonConvert.SerializeObject(lista, Formatting.Indented);

                using (FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(updatedJson);
                }
                return true;
            }
            catch (FalhaServidorException ex)
            {
                throw new FalhaServidorException(ex.Message, ex);
            }

            return false;
        }
       
        public string Login(string senha)
        {
            string mensagemRetorno = string.Empty;

            if (ValidarSenha(senha))
            {
                return CriarToken();
            }

            return mensagemRetorno = string.Empty;
        }

        private bool ValidarSenha(string senhaInserida)
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    List<Usuario> lista;

                    using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string json = sr.ReadToEnd();
                        lista = JsonConvert.DeserializeObject<List<Usuario>>(json);
                    }

                    // Comparar a senha inserida pelo usuário com as senhas criptografadas no arquivo JSON
                    foreach (var usuario in lista)
                    {
                        if (CriptografaSHA256.ValidaSenha(senhaInserida, usuario.Senha))
                        {
                            return true;
                        }
                    }
                }
            }
            catch (FalhaServidorException ex)
            {
                throw new FalhaServidorException(ex.Message, ex);
            }

            // Senha inválida
            return false;
        }

        private string CriarToken()
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                byte[] key = Encoding.ASCII.GetBytes(_tokenConfigurations.Secret);

                var token = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, "UserBanner"),
                        new Claim(ClaimTypes.Role, "admin")
                    }),

                    Expires = DateTime.UtcNow.AddHours(8),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                return tokenHandler.WriteToken(tokenHandler.CreateToken(token));
            }
            catch (FalhaServidorException ex)
            {
                throw new FalhaServidorException(ex.Message, ex);
            }
        }
    }
}
