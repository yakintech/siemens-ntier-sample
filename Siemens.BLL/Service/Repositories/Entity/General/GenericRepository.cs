using Microsoft.EntityFrameworkCore;
using Siemens.DAL.ORM.Context;
using Siemens.DAL.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Siemens.BLL.Service.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        internal SiemensContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(SiemensContext siemensContext)
        {
            this.context = siemensContext;
            this.dbSet = context.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            entity.AddDate = DateTime.Now;
            entity.UpdateDate = DateTime.Now;
            entity.IsDeleted = false;

            dbSet.Add(entity);
            context.SaveChanges();

            return entity;
        }

        public bool Any(Expression<Func<TEntity, bool>> filter)
        {
            bool status = dbSet.Where(q => q.IsDeleted == false).Any(filter);
            return status;
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter)
        {
            var result = dbSet.Where(q => q.IsDeleted == false).FirstOrDefault(filter);
            return result;
        }

        public virtual List<TEntity> GetAll()
        {
            var result = dbSet.Where(q => q.IsDeleted == false).OrderByDescending(x => x.AddDate).ToList();
            return result;
        }

        public IQueryable<TEntity> GetAllWithQueryable()
        {
            var result = dbSet.Where(q => q.IsDeleted == false);
            return result;
        }

        public virtual TEntity GetById(Guid id)
        {
            var entity = dbSet.FirstOrDefault(q => q.IsDeleted == false && q.Id == id);
            return entity;
        }

        public virtual List<TEntity> GetListWithQuery(Expression<Func<TEntity, bool>> filter)
        {
            var result = dbSet.Where(q => q.IsDeleted == false).OrderByDescending(q => q.AddDate).Where(filter).ToList();
            return result;
        }

        public virtual void Remove(Guid id)
        {
            var entity = dbSet.FirstOrDefault(q => q.Id == id);
            entity.IsDeleted = true;
            entity.DeleteDate = DateTime.Now;

            context.SaveChanges();
        }
    }
}
