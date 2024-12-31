using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Role_Identity.Data;
using Role_Identity.Interfaces;
using Role_Identity.Models;
using Role_Identity.Services;

namespace Role_Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            builder.Services.Configure<IdentityOptions>(op =>
            {
                op.Password.RequireDigit = false;
                op.Password.RequireUppercase = false;
                op.Password.RequireNonAlphanumeric = false;
                op.Lockout.MaxFailedAccessAttempts = 5;
                //op.SignIn.RequireConfirmedEmail = true;
            });
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

            builder.Services.AddTransient<IEmailSender, EmailSender>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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

            app.Run();
        }
    }
}
