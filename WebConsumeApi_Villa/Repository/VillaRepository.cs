using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebConsumeApi_Villa.Data;
using WebConsumeApi_Villa.Models;
using WebConsumeApi_Villa.Repository.IRepository;

namespace WebConsumeApi_Villa.Repository
{
   
    public class VillaRepository:IVillaRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public VillaRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Villa entity)
        {
           await _dbContext.villas.AddAsync(entity);
           await SaveAsync();
        }
        public async Task UpdateAsync(Villa entity)
        {
            _dbContext.villas.Update(entity);
            await SaveAsync();
        }

        public async Task<Villa> GetAsync(Expression<Func<Villa,bool>> filter = null, bool tracked = true)
        {
            IQueryable<Villa> query = _dbContext.villas;
            if(!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Villa>> GetAllAsync(Expression<Func<Villa,bool>> filter = null)
        {
            IQueryable<Villa> query = _dbContext.villas;
            if(filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task RemoveAsync(Villa entity)
        {
            _dbContext.villas.Remove(entity);
            await SaveAsync();

        }

        public async Task SaveAsync()
        {
           await _dbContext.SaveChangesAsync();
        }
        
    }
    
}
