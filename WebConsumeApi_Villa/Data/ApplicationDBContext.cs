using Microsoft.EntityFrameworkCore;
using WebConsumeApi_Villa.Models;

namespace WebConsumeApi_Villa.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        {

        }
        public DbSet<Villa> villas { get; set; }
    }
}
