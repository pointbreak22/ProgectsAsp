using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Data.Interfaces;
using Shop.Data.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        //регистрация различных модулей, плагинов
        public void ConfigureServices(IServiceCollection services)
        {     //обьеденить интерфейс и класс который реализует интерфейс
            services.AddTransient<IAllCars, MockCars>();
            services.AddTransient<ICarsCategory, MockCategory>();
            services.AddMvc(options => options.EnableEndpointRouting = false);         //регистрация основного плагина
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage(); //показывание ошибок в режиме дебага
            app.UseStatusCodePages();//показывает код страницы
            app.UseStaticFiles(); //статические файлы, картинки и прочее

            app.UseMvcWithDefaultRoute();// выслеживание url адрес
            app.UseRouting();
            app.UseCors();

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