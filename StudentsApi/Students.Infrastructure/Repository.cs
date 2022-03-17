using Microsoft.EntityFrameworkCore;
using Students.Domain.Base;
using Students.Domain.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Students.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbFactory _dbFactory;
        private DbSet<T> _dbSet;

        protected DbSet<T> DbSet => _dbSet ??= _dbFactory.DbContext.Set<T>();

        public Repository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public IQueryable<T> GetAll()
        {
            return DbSet.AsNoTracking();
        }
        public T Get(int id)
        {
            return DbSet.Find(id);
        }
        public IQueryable<T> Find(Expression<Func<T, bool>> expression)
        {
            return DbSet.Where(expression).AsNoTracking();
        }
        public void Add(T entity)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
            {
                ((IAuditEntity)entity).CreatedDate = DateTime.UtcNow;
            }
            DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            if (typeof(IDeleteEntity).IsAssignableFrom(typeof(T)))
            {
                ((IDeleteEntity)entity).IsDeleted = true;
                DbSet.Update(entity);
            }
            else
                DbSet.Remove(entity);
        }

        public IQueryable<T> List(Expression<Func<T, bool>> expression)
        {
            return DbSet.Where(expression);
        }

        public void Update(T entity)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
            {
                ((IAuditEntity)entity).UpdatedDate = DateTime.UtcNow;
            }
            DbSet.Update(entity);
        }
    }
}