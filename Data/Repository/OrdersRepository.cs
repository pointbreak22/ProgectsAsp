using Shop.Data.Interfaces;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Repository
{
    public class OrdersRepository : IAllAlders
    {
        private readonly AppDbContent appDbContent;
        private readonly ShopCar shopCar;

        public OrdersRepository(AppDbContent appDbContent, ShopCar shopCar)
        {
            this.appDbContent = appDbContent;
            this.shopCar = shopCar;
        }

        public void CreateOrder(Order order)
        {
            order.OlderTime = DateTime.Now;
            appDbContent.Orders.Add(order);
            appDbContent.SaveChanges();
            var items = shopCar.ListShopItems;

            foreach (var el in items)
            {
                var orderDetail = new OrderDetail()
                {
                    CarId = el.Car.Id,
                    OrderId = order.Id,
                    Price = el.Car.Prise
                };
                appDbContent.OrderDetails.Add(orderDetail);
            }

            appDbContent.SaveChanges();
        }
    }
}