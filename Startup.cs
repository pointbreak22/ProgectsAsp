using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Data;
using Shop.Data.Interfaces;
using Shop.Data.Mocks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Shop.Data.Repository;
using Shop.Data.Models;

namespace Shop
{
    public class Startup
    {
        private IConfigurationRoot _confstring;

        //получение подключения из файла
        [Obsolete]
        public Startup(IHostingEnvironment hostEnv)
        {
            _confstring = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        //регистрация различных модулей, плагинов
        public void ConfigureServices(IServiceCollection services)
        {
            //получение подключения из файла
            services.AddDbContext<AppDbContent>(options => options.UseSqlServer(_confstring.GetConnectionString("DefaultConnection")));
            //обьеденить интерфейс и класс который реализует интерфейс
            services.AddTransient<IAllCars, CarsRepository>();
            services.AddTransient<ICarsCategory, CategoryRepository>();
            services.AddTransient<IAllAlders, OrdersRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();   //работа с сессиями
            services.AddScoped(sp => ShopCar.GetCar(sp)); //для каждого разная корзина

            services.AddMvc(options => options.EnableEndpointRouting = false);         //регистрация основного плагина
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage(); //показывание ошибок в режиме дебага
            app.UseStatusCodePages();//показывает код страницы
            app.UseStaticFiles(); //статические файлы, картинки и прочее
            app.UseSession();
            app.UseRouting();
            //      app.UseMvcWithDefaultRoute();// выслеживание url адрес
            app.UseMvc(routes =>
               {
                   routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"); //контроллер по умолчанию и представление по умолчанию
                   routes.MapRoute(name: "categoryFilter", template: "Car/{action}/{category?}", defaults: new { Controller = "Car", action = "List" }); //известный контроллер, любое представление, передается category, в defaults по умолчанию представление
               }
            );

            //app.UseCors();

            using (var score = app.ApplicationServices.CreateScope())
            {
                AppDbContent content = score.ServiceProvider.GetRequiredService<AppDbContent>();
                DBObjects.Initial(content);
            }
            //;

            //if (env.IsDevelopment())                     //Production
            //{
            //}

            ////первый способ
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //}
            //);

            ////второй способ
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }
    }
}