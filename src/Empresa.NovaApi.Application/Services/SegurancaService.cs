using Empresa.NovaApi.Application.Interfaces;
using Empresa.NovaApi.Infra.CrossCutting.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Empresa.NovaApi.Application.Services
{
    public class SegurancaService : ServiceBase, ISegurancaService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;
        public SegurancaService(HttpClient httpClient, AppSettings appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings;
            _httpClient.BaseAddress = new Uri(_appSettings?.SegurancaAPI?.UrlBase);
        }

        public async Task<object> Autorizar(string token)
        {
            var login = new
            {
                Token = token,
                ModuloId = _appSettings.ModuloNovaSolucaoSenacAppId
            };

            var itemContent = ObterConteudo(login);

            var response = await _httpClient.PostAsync("v2/login-token", itemContent);

            TratarErrosResponse(response);

            var result = await DeserializarObjetoResponse(response);

            return result;
        }
    }
}
