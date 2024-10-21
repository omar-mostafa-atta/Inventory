using Inventory.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Task.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDBcontext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationDBcontext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }
        public IQueryable<TEntity> GetAllQueryable()
        {
            return _dbSet; // Allows for further querying with Includes
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }
		public TEntity GetById(string id)
		{
			return _dbSet.Find(id);
		}

		public void Add(TEntity entity)
        {
          
          _dbSet.Add(entity);
          _context.SaveChanges();
          
        }
        public IEnumerable<TEntity> Find(Expression<Func<TEntity,bool>> predicate)
        {

           return _dbSet.Where(predicate);

        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }
		public void Delete(string id)
		{
			var entity = _dbSet.Find(id);
			if (entity != null)
			{
				_dbSet.Remove(entity);
				_context.SaveChanges();
			}
		}
	}
}
