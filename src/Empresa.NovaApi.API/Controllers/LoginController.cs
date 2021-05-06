
using Empresa.NovaApi.API.Json;
using Empresa.NovaApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Empresa.NovaApi.API.Controllers
{
    [AllowAnonymous]
    public class LoginController : BaseController
    {
        private readonly ISegurancaService _senacSegurancaService;
        private readonly IBancoDadosExemploService _bancoDadosExemploService;

        public LoginController(ISegurancaService senacSegurancaService, IBancoDadosExemploService bancoDadosExemploService)
        {
            _senacSegurancaService = senacSegurancaService;
            _bancoDadosExemploService = bancoDadosExemploService;
        }

        [HttpGet("login-token")]
        public async Task<IActionResult> LoginToken(string token)
        {
            return ResultResponse(await _senacSegurancaService.Autorizar(token));
        }

        [HttpGet("obter-perfis")]

        public async Task<IActionResult> ObterPerfils()
        {
            //return Content(await _bancoDadosExemploService.RetornarPerfilSeguranca(), "application/json");
            return ResultResponse(await _bancoDadosExemploService.RetornarPerfilSeguranca());
        }



        [HttpGet("obter-json-grande")]
        public  IActionResult ObterJsonGrande() {

            var lista = new List<TesteJson>();

            lista.Add(new TesteJson
            {
                Teste = 1

            });


            lista.Add(new TesteJson
            {
                Teste = 1

            });

            lista.Add(new TesteJson
            {
                Teste = 1

            });

            lista.Add(new TesteJson
            {
                Teste = 1

            });

            lista.Add(new TesteJson
            {
                Teste = 1

            });
            lista.Add(new TesteJson
            {
                Teste = 1

            });


            return new JsonResult(lista);
        }

        private string  LoadJson()
        {
            using StreamReader r = new StreamReader("Json/exemplo.json");
            return r.ReadToEnd();
        }


        private JsonResult GetJsonProdutos()
        {
            return new JsonResult(
                LoadJson(),
                new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                }
            );
        }

    }
}
