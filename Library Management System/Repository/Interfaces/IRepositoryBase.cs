using System.Threading.Tasks;

namespace Library_Management_System.Repository.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        Task<int> SaveChangesAsync();
    }
}
