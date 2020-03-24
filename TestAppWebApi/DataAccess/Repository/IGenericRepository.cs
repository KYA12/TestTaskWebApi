using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TestAppWebApi.DataAccess.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task Add(T entity);
        void Remove(T entity);
        T GetById(int id);
        void Update(T entity);
    }

}
