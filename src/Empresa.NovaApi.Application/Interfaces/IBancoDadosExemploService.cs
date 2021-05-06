using System.Threading.Tasks;

namespace Empresa.NovaApi.Application.Interfaces
{
    public interface IBancoDadosExemploService
    {
        Task<string> RetornarPerfilSeguranca();
    }
}
