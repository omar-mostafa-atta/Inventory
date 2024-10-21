using Inventory.Data;
using Inventory.Service.Sevices.AlertService;
using Inventory.Service.Sevices.CategoryService;
using Inventory.Service.Sevices.ProductService;
using Inventory.Service.Sevices.Reports;
using Inventory.Service.Sevices.SupplierService;
using Inventory.Service.Sevices.TransactionService;
using Inventory.Service.Sevices.UserService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task.Repositories;

namespace Inventory
{
    public class Program
    {
        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<ITransactionService,TransactionService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ISupplierService, SupplierService>();
            builder.Services.AddScoped<IReportService, ReportService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IAlertService, AlertService>();
            builder.Services.AddDbContext<ApplicationDBcontext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<User,IdentityRole>(option =>
            {
                //options for editing in register rules

                option.Password.RequiredLength = 6;
                option.Password.RequireDigit = true;
                option.Password.RequireUppercase = false;

            })
                .AddEntityFrameworkStores<ApplicationDBcontext>()
                .AddDefaultUI()
                .AddRoles<IdentityRole>()
                .AddDefaultTokenProviders();
           
           //adding identity
            
           

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapRazorPages();      
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


			app.Use(async (context, next) =>
			{
				if (context.Request.Path == "/Identity/Account/Login")
				{
					context.Response.Redirect("/Account/Login");
					return;
				}
				await next();
			});
			app.Use(async (context, next) =>
			{
				if (context.Request.Path == "/Identity/Account/Register")
				{
					context.Response.Redirect("/Account/Register");
					return;
				}
				await next();
			});

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new[] { "admin", "user" };

                foreach (var role in roles)
                {
                    if (!roleManager.RoleExistsAsync(role).Result)
                    {
                        roleManager.CreateAsync(new IdentityRole(role)).Wait();
                    }
                    else
                    {
                        Console.WriteLine($"Role '{role}' already exists. Skipping creation.");
                    }
                }
            }
            app.Run();
        }
    }
}