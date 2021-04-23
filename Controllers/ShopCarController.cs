using Microsoft.AspNetCore.Mvc;
using Shop.Data.Interfaces;
using Shop.Data.Models;

using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class ShopCarController : Controller
    {
        private readonly IAllCars _Carrep;
        private readonly ShopCar _shopCar;

        public ShopCarController(IAllCars Carrep, ShopCar shopCar)
        {
            _Carrep = Carrep; _shopCar = shopCar;
        }

        public ViewResult Index()
        {
            var items = _shopCar.GetShopItems();
            _shopCar.ListShopItems = items;

            var obj = new ShopCarViewModel
            {
                ShopCar = _shopCar
            };
            return View(obj);
        }

        public RedirectToActionResult AddToCar(int id)
        {
            var item = _Carrep.Cars.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                _shopCar.AddToCar(item);
            }
            return RedirectToAction("Index");  //при срабатывания кода переадрисация на index
        }
    }
}