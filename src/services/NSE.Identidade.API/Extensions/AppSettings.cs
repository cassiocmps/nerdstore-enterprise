namespace NSE.Identidade.API.Extensions
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpiracaoHoras { get; set; }
        public string Emissor { get; set; } // issuer
        public string ValidoEm { get; set; } // audience
    }
}