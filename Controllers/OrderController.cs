using Microsoft.AspNetCore.Mvc;
using Shop.Data.Interfaces;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IAllAlders allorder;
        private readonly ShopCar shopCar;

        public OrderController(IAllAlders allorder, ShopCar shopCar)
        {
            this.allorder = allorder;
            this.shopCar = shopCar;
        }

        public IActionResult CheckOut()     //форма с действиями от пользователя
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckOut(Order order)     //отлавливает Post обьекта
        {
            shopCar.listShopItems = shopCar.GetShopItems();
            if (shopCar.listShopItems.Count == 0)
            {
                ModelState.AddModelError("", "У вас должны быть товары");
            }

            if (ModelState.IsValid)  //проверка модели на валидацию
            {
                allorder.createOrder(order);
                return RedirectToAction("Complete");
            }
            return View(order);
        }

        public ActionResult Complete()
        {
            ViewBag.Message = "Заказ успешно обработан";
            return View();
        }
    }
}