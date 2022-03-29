using System;
using NSE.Core.DomainObjects;

namespace NSE.Core.Data
{
    // somente entidades devem ser persistidas em banco, portanto herdar desse repositorio
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        
    }
}