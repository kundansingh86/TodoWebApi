using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ToDo.Core.Interfaces;
using ToDo.Infra;

namespace ToDo.Infra.Repos
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly ToDoDbContext _toDoDbContext;

        public RepositoryBase(ToDoDbContext toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }

        public void Create(T entity)
        {
            _toDoDbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _toDoDbContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return _toDoDbContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition)
        {
            return _toDoDbContext.Set<T>().Where(condition).AsNoTracking();
        }

        public void Update(T entity)
        {
            _toDoDbContext.Set<T>().Update(entity);
        }
    }
}
