using Fiorella.Areas.Admin.Controllers.Constants;
using Fiorella.DAL;
using Fiorella.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorella
{
    public class Startup
    {
        private IConfiguration _config;
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration config, IWebHostEnvironment env)
        {
            this._config = config;
            _env = env;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 5;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
            });
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();
            FileConstants.ImagePath = Path.Combine(_env.WebRootPath, "img");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
