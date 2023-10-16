using MicroondasDigital.Domain.Extensions.Exceptions;
using MicroondasDigital.Domain.Interfaces.Repository;
using MicroondasDigital.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroondasDigital.Data.Repository
{
    public class ProgramaCustomizadoRepository : IProgramaCustomizadoRepository
    {
        private string FilePath { get; }

        public ProgramaCustomizadoRepository()
        {
            FilePath = "c:\\Temp\\ProgramasCustomizados.Json";

            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, "[]");
            }
        }

        public bool Salvar(Microonda item)
        {
            try
            {
                List<Microonda> lista;

                if (File.Exists(FilePath))
                {
                    using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string json = sr.ReadToEnd();
                        lista = JsonConvert.DeserializeObject<List<Microonda>>(json);
                    }
                }
                else
                {
                    lista = new List<Microonda>();
                }

                int quantidadeAtual = 5;
                int novoId = (quantidadeAtual + (lista.Count == 0 ? 1 : lista.Count));

                item.Id = novoId;

                lista.Add(item);
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
        }

        public List<Microonda> ObterProgramasCustomizados()
        {
            List<Microonda> lista = new List<Microonda>();

            if (File.Exists(FilePath))
            {
                try
                {
                    using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string json = sr.ReadToEnd();
                        lista = JsonConvert.DeserializeObject<List<Microonda>>(json);
                    }
                }
                catch (FalhaServidorException ex)
                {
                    throw new FalhaServidorException(ex.Message, ex);

                }
            }

            return lista;
        }
    }
}
