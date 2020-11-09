using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Services.Abstract;
using Core.Services.Concrete;
using Domain.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ContentMS
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
            //public void ConfigureServices(IServiceCollection services)
            //{
                //services.AddDbContext<FbContext>(options =>
                //                                         options.UseSqlServer(Configuration.GetConnectionString("ConnectionStr")));

                services.AddDbContext<CmsContext>(options =>
                {
                    options.UseSqlServer(Configuration["ConnectionStr"]);
                });

                //services.AddDbContext<CmsContext>(options =>
                //    options.UseSqlServer(Configuration.GetConnectionString("ConnectionStr")));
            

            //services.AddDbContext<CmsContext>(options =>
            //{
            //    options.UseSqlServer(Configuration.GetConnectionString("ConnectionStr"));
            //});

            //            services.AddEntityFrameworkSqlServer()
            //           .AddDbContext<CmsContext>((serviceProvider, options) =>
            //options.UseSqlServer("Server=LAPTOP-56NBTJ4K\\SQLSERVERMERT;Database=ProjectCMSDB;Trusted_Connection=True;MultipleActiveResultSets=true;")
            //      .UseInternalServiceProvider(serviceProvider));



            //public void ConfigureServices(IServiceCollection services)
            //{
            //    services.AddDbContext<CmsContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("ConnectionStr")));
            //    services.AddControllers();




            services.AddScoped<IPageService, PageService>(); //addtransient farkýný ögren
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<ILayoutService, LayoutService>();
            //services.AddTransient<IHostingEnvironment, env>();

            services.AddControllersWithViews();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern:"{area}/{controller=Home}/{action=Index}/{id?}"
                    );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
