using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Template.Domain.Interfaces
{
    /// <summary>
    /// Репозиторий
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Получение данных
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null);

        public TEntity GetById(object id);

        public Task Insert(TEntity entity);

        public void Delete(TEntity entityToDelete);

        public void Update(TEntity entityToUpdate);
    }
}