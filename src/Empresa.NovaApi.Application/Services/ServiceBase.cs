using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
//using System.Text.Json;
using System.Threading.Tasks;

namespace Empresa.NovaApi.Application.Services
{
    public abstract class ServiceBase
    {
        protected StringContent ObterConteudo(object dado)
        {
            return new StringContent(
                System.Text.Json.JsonSerializer.Serialize(dado),
                Encoding.UTF8,
                "application/json");
        }

        protected async Task<T> DeserializarObjetoResponse<T>(HttpResponseMessage responseMessage)
        {
            var teste = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(await responseMessage.Content.ReadAsStringAsync());
        }

        protected async Task<string> DeserializarObjetoResponse(HttpResponseMessage responseMessage)
        {
            return await responseMessage.Content.ReadAsStringAsync();
        }


        protected bool TratarErrosResponse(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500:
                case 400:
                    return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}
