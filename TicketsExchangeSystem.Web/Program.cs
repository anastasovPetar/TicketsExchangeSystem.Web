namespace TicketsExchangeSystem.Web
{
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using TicketExchangeSystem.Services.Data.Interfaces;

    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services
                .AddDbContext<TicketsExchangedbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => 
                {
                    options.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("Identity:SighIn:RequireConfirmedAccount");

                    options.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
                    options.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
                    options.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
                    options.Password.RequiredLength = builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
                })
                .AddEntityFrameworkStores<TicketsExchangedbContext>();

            builder.Services.AddControllersWithViews();

            //builder.Services.AddScoped<ITicketService, ITicketService>();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();
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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();


            app.Run();
        }
    }
}