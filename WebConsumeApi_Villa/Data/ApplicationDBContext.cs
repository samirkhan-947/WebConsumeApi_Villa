using Microsoft.EntityFrameworkCore;
using System.Text;
using WebConsumeApi_Villa.Models;

namespace WebConsumeApi_Villa.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(                
                new Villa
                {
                    Id= 1,
                    Name="Banglore villa",
                    Details ="this is the banglaure villa with beautyful house and decent price ",
                    ImageUrl ="",
                    Occupancy= 5,
                    Rate= 200,
                    Sqft= 550,
                    Amenity="",
                    CreateDate= DateTime.Now,
                },
                 new Villa
                 {
                     Id = 2,
                     Name = "pune villa",
                     Details = "this is the pune villa with beautyful house and decent price ",
                     ImageUrl = "",
                     Occupancy = 4,
                     Rate = 300,
                     Sqft = 450,
                     Amenity = "",
                     CreateDate = DateTime.Now,
                 },
                 new Villa
                 {
                     Id = 3,
                     Name = "patna villa",
                     Details = "this is the pune villa with beautyful house and decent price ",
                     ImageUrl = "",
                     Occupancy = 3,
                     Rate = 600,
                     Sqft = 850,
                     Amenity = "",
                     CreateDate = DateTime.Now,
                 }
            );
        }
        public DbSet<Villa> villas { get; set; }
    }
}
