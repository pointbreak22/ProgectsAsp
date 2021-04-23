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

        public void createOrder(Order order)
        {
            order.olderTime = DateTime.Now;
            appDbContent.Orders.Add(order);
            appDbContent.SaveChanges();
            var items = shopCar.listShopItems;

            foreach (var el in items)
            {
                var orderDetail = new OrderDetail()
                {
                    CarId = el.car.Id,
                    OrderId = order.Id,
                    price = el.car.Prise
                };
                appDbContent.OrderDetails.Add(orderDetail);
            }

            appDbContent.SaveChanges();
        }
    }
}