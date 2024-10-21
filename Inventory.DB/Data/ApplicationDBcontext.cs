using Inventory.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Data
{

    public partial class ApplicationDBcontext : IdentityDbContext<User>
    {
        public ApplicationDBcontext(DbContextOptions<ApplicationDBcontext> options)
       : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Alert> Alerts { get; set; }

		//protected override void OnModelCreating(ModelBuilder builder)
		//{
		//	//base.OnModelCreating(builder);
  //          //builder.Entity<User>().ToTable("users");
  //          //builder.Entity<IdentityRole>().ToTable("Roles");
		//}

	}
}
