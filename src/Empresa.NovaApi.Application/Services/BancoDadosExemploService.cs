using Dapper;
using Empresa.NovaApi.Application.Interfaces;
using Empresa.NovaApi.Infra.CrossCutting.Options;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Empresa.NovaApi.Application.Services
{
    public class BancoDadosExemploService : IBancoDadosExemploService
    {
        private readonly ConexaoBancoDados _conexaoBancoDados;
        public BancoDadosExemploService(ConexaoBancoDados conexaoBancoDados)
        {
            _conexaoBancoDados = conexaoBancoDados;
        }

        public async Task<string> RetornarPerfilSeguranca()
        {
            string retornoJson;

            using (SqlConnection conexao = new SqlConnection(_conexaoBancoDados.Seguranca))
            {
                retornoJson = await conexao.QueryFirstAsync<string>(
                    $@"select 
                        * 
                        from Perfil
                        order by Nome
                        for json path, root('Perfil')"
                    );
            }

            return retornoJson;
        }
    }
}
