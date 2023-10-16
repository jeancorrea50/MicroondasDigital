
namespace MicroondasDigital.Domain.Security
{
    public class TokenConfigurations
    {
        public string Secret { get; set; }
        public int ExpiracaoHoras { get; set; }
        public int ExpirationInMinutes { get; set; }
        public string Issuer { get; set; }
        public string ValidOn { get; set; }
        public int RefreshExpirationInMinutes { get; set; }
        public string Emissor { get; set; }
        public string LocalHost { get; set; }
        public string Producao { get; set; }
    }
}
