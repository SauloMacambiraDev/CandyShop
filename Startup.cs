using CandyShop.Data;
using CandyShop.Models;
using CandyShop.Models.Interfaces;
using CandyShop.Models.Persistences;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop
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
            services.AddControllersWithViews();

            //Adding dependency that enable us to connect to our database with EntityFramework
            services.AddDbContext<CandyShopDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICandyRepository, CandyRepository>();
            services.AddScoped<ShoppingCart>(services => ShoppingCart.GetCart(services));
            services.AddScoped<IOrderRepository, OrderRepository>();
            /* We could do that by doing it
                services.AddScoped<ShoppingCart>(services =>
                {
                    ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
                    CandyShopDbContext db = services.GetRequiredService<CandyShopDbContext>();
                    string shoppingCartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
                    session.SetString("CartId", shoppingCartId);

                    return new ShoppingCart(db)
                    {
                        ShoppingCartId = shoppingCartId
                    };
                });
            */

            //Enable application to get HttpContext through dependecy injection
            //This funcionality is necessary in order to ShoppingCart class pursuit the
            //Session that exist in an HTTP Request by reading Cookies
            services.AddHttpContextAccessor();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(300); // How much time the session can be idle before its contents are abandoned.
                options.Cookie.Name = ".CandyShop.Session";
                options.Cookie.HttpOnly = true; // Cannot be manipulated or accessed on ClientSide
                options.Cookie.IsEssential = true;
            });
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
            app.UseSession(); // it has to be setted before routing managment 'UseRouting()'

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    //pattern: "{controller=Candy}/{action=Index}/{id?}");
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
