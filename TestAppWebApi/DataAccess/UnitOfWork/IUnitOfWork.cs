using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppWebApi.DataAccess.Repository;

namespace TestAppWebApi.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IConsultantRepository Consultants { get; }
        IShopRepository Shops { get; }
        Task<bool> Complete();
        void Rollback();
    }
}
