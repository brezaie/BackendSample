using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTest.Repository
{
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> Insert(TEntity entity);

        Task Update(TEntity entity);
        
        Task Delete(TEntity entity);
    }
}