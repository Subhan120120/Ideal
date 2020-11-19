using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using QonaqWebApp.AppCode.Infrastructure;
using QonaqWebApp.AppCode.Repositories;
using QonaqWebApp.Models.Context;
using QonaqWebApp.Models.Entity;
using System;
using System.IO;

namespace QonaqWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOutputCaching();
            services.AddDistributedMemoryCache(); //before AddControllersWithViews
            services.AddSession(options =>
           {
               options.IdleTimeout = TimeSpan.FromMinutes(10);
               options.Cookie.HttpOnly = true;
               options.Cookie.IsEssential = true;
           });

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddDbContext<QonaqContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("QonaqDBString")));

            RepositoryDIImplementation(services);
        }

        private void RepositoryDIImplementation(IServiceCollection services)
        {
            services.AddScoped<IRepository<AppDetail>, AppDetailRepository>();
            services.AddScoped<IRepository<MenuItem>, MenuItemRepository>();
            services.AddScoped<IRepository<MenuItemGroup>, MenuItemGroupRepository>();
            services.AddScoped<IRepository<Reservation>, ReservationRepository>();
            services.AddScoped<IRepository<DineInTable>, DineInTableRepository>();
            services.AddScoped<IRepository<DineInTableGroup>, DineInTableGroupRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions()
            {
                RequestPath = new PathString("/npm"),
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "node_modules"))
            });

            app.UseRouting();

            app.UseOutputCaching();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
                //defaults: new { area = "Admin", controller = "Dashboard", action = "Index" }
                );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=About}/{id?}");
            });
        }
    }
}
