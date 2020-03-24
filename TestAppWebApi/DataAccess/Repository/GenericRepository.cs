using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestAppWebApi.Models;

namespace TestAppWebApi.DataAccess.Repository
{
    // Класс обобщенного репозитория
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> _entities;
        public GenericRepository(ShopDataBaseContext context)
        {
            _entities = context.Set<T>();
        }
        public void Update(T entity)
        {
            _entities.Update(entity);
        }
        public IQueryable<T> GetAll()
        {
            return _entities;
        }
        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
        }

        public T GetById(int Id)
        {
            return _entities.Find(Id);
        }
    }
}
