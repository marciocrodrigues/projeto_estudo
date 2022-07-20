using System.Threading.Tasks;

namespace ProjetoParaEstudo.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
