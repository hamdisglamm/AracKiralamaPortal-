using AracKiralamaPortal.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AracKiralamaPortal
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false; // E-posta do�rulama gereksinimini kald�rd�k
                options.Password.RequireDigit = false; // �ifre kurallar�n� gev�ettik
                options.Password.RequiredLength = 4; // Minimum �ifre uzunlu�unu 4 yapt�k
                options.Password.RequireNonAlphanumeric = false; // Alfasay�sal olmayan karakter zorunlulu�unu kald�rd�k
                options.Password.RequireUppercase = false; // B�y�k harf zorunlulu�unu kald�rd�k
                options.Password.RequireLowercase = false; // K���k harf zorunlulu�unu kald�rd�k
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddSignalR(); // SignalR servisi ekleme

            // UnitOfWork servisini ekle
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Rolleri ve admin hesab�n� olu�tur
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    await RoleSeeder.SeedRolesAndAdminAsync(services);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while seeding roles: {ex.Message}");
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=User}/{action=Index}/{id?}");

            app.MapHub<AracKiralamaPortal.Hubs.CarHub>("/carHub"); // SignalR hub ba�lant�s�

            app.MapRazorPages();

            app.Run();
        }
    }
}
