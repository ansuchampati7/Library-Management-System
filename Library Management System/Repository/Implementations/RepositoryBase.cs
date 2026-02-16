using Library_Management_System.Domain.Infrastructure;
using System.Threading.Tasks;

namespace Library_Management_System.Repository.Implementations
{
    public abstract class RepositoryBase<TEntity> where TEntity : class
    {
        protected readonly LibraryDbContext _context;

        protected RepositoryBase(LibraryDbContext context)
        {
            _context = context;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
