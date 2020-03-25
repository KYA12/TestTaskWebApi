using System;
using System.Linq;
using System.Threading.Tasks;
using TestAppWebApi.Models;
using TestAppWebApi.DataAccess.Repository;

namespace TestAppWebApi.DataAccess.UnitOfWork
{
    //Реализация паттерна Unit of Work
    public class UnitOfWorkEFCore : IUnitOfWork
    {
        private readonly ShopDataBaseContext context;
        private IConsultantRepository consultantRepository;
        private IShopRepository shopRepository;

        public UnitOfWorkEFCore(ShopDataBaseContext con)
        {
            context = con;
        }
        public IConsultantRepository Consultants
        {
            get
            {
                if (consultantRepository == null)
                    consultantRepository = new ConsultantRepository(context);
                return consultantRepository;
            }
        }

        public IShopRepository Shops
        {
            get
            {
                if (shopRepository == null)
                    shopRepository = new ShopRepository(context);
                return shopRepository;
            }
        }

        public async Task<bool> Complete()
        {
            if (await context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        private bool disposed = false;

        public void Rollback()
        {
            context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
