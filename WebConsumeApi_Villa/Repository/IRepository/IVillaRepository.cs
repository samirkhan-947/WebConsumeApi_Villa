using System.Linq.Expressions;
using WebConsumeApi_Villa.Models;

namespace WebConsumeApi_Villa.Repository.IRepository
{
    public interface IVillaRepository:IRepository<Villa>
    {
        Task<Villa> UpdateAsync(Villa entity);
    }
}
