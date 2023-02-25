using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebConsumeApi_Villa.Data;
using WebConsumeApi_Villa.Models;
using WebConsumeApi_Villa.Repository.IRepository;

namespace WebConsumeApi_Villa.Repository
{

    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public VillaNumberRepository(ApplicationDBContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _dbContext.villaNumbers.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;

        }
    }
    
}
