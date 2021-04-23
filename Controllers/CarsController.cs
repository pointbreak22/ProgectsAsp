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
    public class CarsController : Controller
    {
        private readonly IAllCars _allCars;
        private readonly ICarsCategory _allCategories;

        public CarsController(IAllCars allCars, ICarsCategory CarsCategory)
        {
            _allCars = allCars; _allCategories = CarsCategory;
        }

        [Route("Cars/List")]
        [Route("Cars/List/{category}")]
        public ViewResult List(string category)
        {
            string _category = category;
            IEnumerable<Car> Cars = null;
            string curtCategory = "";
            if (string.IsNullOrEmpty(_category))
            {
                Cars = _allCars.Cars.OrderBy(i => i.Id);
            }
            else
            {
                if (string.Equals("electro", _category, StringComparison.OrdinalIgnoreCase))
                {
                    Cars = _allCars.Cars.Where(i => i.Category.CategoryName.Equals("Электромобили")).OrderBy(i => i.Id);
                    curtCategory = "Электромобили";
                }
                else if (string.Equals("fuel", _category, StringComparison.OrdinalIgnoreCase))
                {
                    Cars = _allCars.Cars.Where(i => i.Category.CategoryName.Equals("Класические автомобили")).OrderBy(i => i.Id);
                    curtCategory = "Класические автомобили";
                }
            }
            var CarObj = new CarsListViewModel
            {
                AllCars = Cars,
                CuurCategory = curtCategory
            };
            ViewBag.Title = "Страница с автомобилями";

            return View(CarObj);                //возвращение представления
        }
    }
}