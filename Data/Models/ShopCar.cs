using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Models
{
    public class ShopCar
    {
        private readonly AppDbContent appDbContent;

        public ShopCar(AppDbContent appDbContent)
        {
            this.appDbContent = appDbContent;
        }

        public string ShopCarId { get; set; }
        public List<ShopCarItem> listShopItems { get; set; }

        public static ShopCar GetCar(IServiceProvider service)
        {
            //обьект для работы с сессиями
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = service.GetService<AppDbContent>();
            string shopCarId = session.GetString("CarId") ?? Guid.NewGuid().ToString();
            session.SetString("CarId", shopCarId);
            return new ShopCar(context) { ShopCarId = shopCarId };
        }

        public void AddToCar(Car car)
        {
            this.appDbContent.ShopCarItem.Add(new ShopCarItem
            {
                ShopCarId = ShopCarId,
                car = car,
                price = car.Prise
            });
            appDbContent.SaveChanges();
        }

        public List<ShopCarItem> GetShopItems()
        {
            return appDbContent.ShopCarItem.Where(c => c.ShopCarId == ShopCarId).Include(s => s.car).ToList();
        }
    }
}