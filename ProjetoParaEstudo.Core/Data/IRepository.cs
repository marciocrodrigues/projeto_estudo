using ProjetoParaEstudo.Core.DomainObjects;
using System;

namespace ProjetoParaEstudo.Core.Data
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
