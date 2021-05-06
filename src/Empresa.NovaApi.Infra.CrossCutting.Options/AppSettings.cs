namespace Empresa.NovaApi.Infra.CrossCutting.Options
{
    public class AppSettings
    {
        public string ModuloNovaSolucaoSenacAppId { get; set; }
        public SegurancaAPI SegurancaAPI { get; set; }
    }

    public class SegurancaAPI
    {
        public string UrlBase { get; set; }
        public string ModuloAppId { get; set; }
    }
}
