using System.Linq.Expressions;
using WebConsumeApi_Villa.Models;

namespace WebConsumeApi_Villa.Repository.IRepository
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {
        Task<VillaNumber> UpdateAsync(VillaNumber entity);
    }
}
