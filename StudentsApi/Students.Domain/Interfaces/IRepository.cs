using System;
using System.Linq;
using System.Linq.Expressions;

namespace Students.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    IQueryable<T> GetAll();
    T Get(int id);
    IQueryable<T> Find(Expression<Func<T, bool>> expression);
    void Add(T entity);
    void Delete(T entity);
    void Update(T entity);
    IQueryable<T> List(Expression<Func<T, bool>> expression);
}