using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rotativa.AspNetCore;
using SistemaSocial.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial
{
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("SqlServerConnection")));

            services.AddIdentity<Usuario, IdentityRole>(options => {
                //Restricciones de contraseña
                options.Password.RequireDigit = false; 
                options.Password.RequireLowercase = false; /* Minúsculas */
                options.Password.RequireUppercase = false; /* Mayúsculas */
                options.Password.RequireNonAlphanumeric = false;

                //Restricciones de Usuario
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = 
                "abcçdefgğhijklmnoöpqrsştuüvwxyz" +
                "ABCÇDEFGĞHIİJKLMNOÖPQRSŞTUÜVWXYZ" +
                "0123456789-._";
            })
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddAuthorization(options => {
                options.AddPolicy("PolicyAdmin", policy => policy.RequireClaim("Admin"));
                options.AddPolicy("PolicyEditor", policy => policy.RequireClaim("Editor"));
            });
            services.AddControllersWithViews();      
            
        }

        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } 
            else {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
                
            var hostingEnvironment = app.ApplicationServices.GetService<Microsoft.AspNetCore.Hosting.IHostingEnvironment>();
            RotativaConfiguration.Setup(env.WebRootPath,"Rotativa");
        }
    }
}