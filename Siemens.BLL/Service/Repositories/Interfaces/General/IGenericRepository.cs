using Siemens.DAL.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Siemens.BLL.Service.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity Add(TEntity entity);

        void Remove(Guid id);

        TEntity GetById(Guid id);

        List<TEntity> GetAll();

        IQueryable<TEntity> GetAllWithQueryable();

        List<TEntity> GetListWithQuery(Expression<Func<TEntity, bool>> filter);

        bool Any(Expression<Func<TEntity, bool>> filter);
    }
}
