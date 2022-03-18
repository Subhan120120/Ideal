using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using QonaqWebApp.AppCode.Infrastructure;
using QonaqWebApp.AppCode.Repositories;
using QonaqWebApp.Models.Context;
using QonaqWebApp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
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

            services.AddLocalization(option => { option.ResourcesPath = "AppCode/Resources"; });

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver())
                .AddSessionStateTempDataProvider()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();

            services.Configure<RequestLocalizationOptions>(option =>
            {
                var supportedCultures = new List<CultureInfo>
                   {
                        new CultureInfo("en"),
                        new CultureInfo("tr"),
                        new CultureInfo("az")

                   };
                option.DefaultRequestCulture = new RequestCulture("tr");
                option.SupportedCultures = supportedCultures;
                option.SupportedUICultures = supportedCultures;
            });

            services.AddRazorPages();
            services.AddDbContext<IdealContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("QonaqDBString")));

            RepositoryDIImplementation(services);
        }

        private void RepositoryDIImplementation(IServiceCollection services)
        {
            services.AddScoped<IRepository<AppDetail>, AppDetailRepository>();
            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<IRepository<Category>, CategoryRepository>();
            services.AddScoped<IRepository<Reservation>, ReservationRepository>();
            services.AddScoped<IRepository<DineInTable>, DineInTableRepository>();
            services.AddScoped<IRepository<DineInTableGroup>, DineInTableGroupRepository>();
            services.AddScoped<IRepository<Order>, OrderRepository>();
            services.AddScoped<IRepository<Customer>, CustomerRepository>();
            services.AddScoped<IRepository<Brand>, BrandRepository>();
        }

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

            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
                //defaults: new { area = "Admin", controller = "Dashboard", action = "Index" }
                );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
