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

        //��������� ����������� �� �����
        [Obsolete]
        public Startup(IHostingEnvironment hostEnv)
        {
            _confstring = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        //����������� ��������� �������, ��������
        public void ConfigureServices(IServiceCollection services)
        {
            //��������� ����������� �� �����
            services.AddDbContext<AppDbContent>(options => options.UseSqlServer(_confstring.GetConnectionString("DefaultConnection")));
            //���������� ��������� � ����� ������� ��������� ���������
            services.AddTransient<IAllCars, CarsRepository>();
            services.AddTransient<ICarsCategory, CategoryRepository>();
            services.AddTransient<IAllAlders, OrdersRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();   //������ � ��������
            services.AddScoped(sp => ShopCar.GetCar(sp)); //��� ������� ������ �������

            services.AddMvc(options => options.EnableEndpointRouting = false);         //����������� ��������� �������
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage(); //����������� ������ � ������ ������
            app.UseStatusCodePages();//���������� ��� ��������
            app.UseStaticFiles(); //����������� �����, �������� � ������
            app.UseSession();
            app.UseRouting();
            //      app.UseMvcWithDefaultRoute();// ������������ url �����
            app.UseMvc(routes =>
               {
                   routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"); //���������� �� ��������� � ������������� �� ���������
                   routes.MapRoute(name: "categoryFilter", template: "Car/{action}/{category?}", defaults: new { Controller = "Car", action = "List" }); //��������� ����������, ����� �������������, ���������� category, � defaults �� ��������� �������������
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

            ////������ ������
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //}
            //);

            ////������ ������
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