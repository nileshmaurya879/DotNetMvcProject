using Microsoft.EntityFrameworkCore;
using MyWebApplication.DataAccessLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApplication.DataAccessLayer.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly ApplicationDBContext _context;

        private DbSet<T> _dbSet;

        public Repository(ApplicationDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void Add(T entity) => _dbSet.Add(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);

        public void DeleteRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);

        public T GetT(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate).FirstOrDefault();

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }
    }
}
