using System.Threading.Tasks;

namespace Empresa.NovaApi.Application.Interfaces
{
    public interface ISegurancaService
    {
        Task<object> Autorizar(string token);
    }
}
