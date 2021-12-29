using Fiorella.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.DAL
{
    public class AppDbContext :IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Layout> Layout { get; set; }
        public DbSet<SliderImage> SliderImages { get; set; }
        public DbSet<Slider> Slider { get; set; }
    }
}
