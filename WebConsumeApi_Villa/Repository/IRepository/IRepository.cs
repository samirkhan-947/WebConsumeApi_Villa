using System.Linq.Expressions;
using WebConsumeApi_Villa.Models;

namespace WebConsumeApi_Villa.Repository.IRepository
{
    public interface IRepository<T>where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true);
        Task CreateAsync(T entity);       
        Task RemoveAsync(T entity);
        Task SaveAsync();
    }
}
