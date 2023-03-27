using Dialogue_Visualizer.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dialogue_Visualizer.Repositories
{
    public class GenericRepository<T> where T : class
    {
        internal DialogueDbContext context;
        internal DbSet<T> dbSet;

        public GenericRepository(DialogueDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "") {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual T? GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(T entity)
        {
            dbSet.Add(entity);
        }
        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            dbSet.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Delete(T? entity)
        {
            if (entity == null)
                return;

            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }
        public virtual void Delete(object id)
        {
            var entity = dbSet.Find(id);
            Delete(entity);
        }

        public virtual void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
