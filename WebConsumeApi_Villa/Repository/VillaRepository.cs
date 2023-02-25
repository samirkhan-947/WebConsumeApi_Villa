using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebConsumeApi_Villa.Data;
using WebConsumeApi_Villa.Models;
using WebConsumeApi_Villa.Repository.IRepository;

namespace WebConsumeApi_Villa.Repository
{

    public class VillaRepository :Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public VillaRepository(ApplicationDBContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdateDate = DateTime.Now;
            _dbContext.villas.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;

        }
    }
    
}
