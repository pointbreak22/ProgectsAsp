using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data
{
    public class DBObjects
    {
        public static void Initial(AppDbContent content)
        {
            //если нет любых категории
            if (!content.Category.Any())
            {
                content.Category.AddRange(Categories.Select(c => c.Value));
            }
            if (!content.Car.Any())
            {
                content.AddRange(
                   new Car
                   {
                       Name = "Tesla",
                       ShortDesc = "Быстрый",
                       LongDesc = "красивый",
                       Imgs = "/images/s1200.jpg",
                       Prise = 4500,
                       IsFavourite = true,
                       Available = true,
                       Category = Categories["Электромобили"]
                   },

                    new Car
                    {
                        Name = "Tesla",
                        ShortDesc = "Быстрый",
                        LongDesc = "красивый",
                        Imgs = "/images/i.webp",
                        Prise = 4500,
                        IsFavourite = true,
                        Available = true,
                        Category = Categories["Электромобили"]
                    }

                );
            }
            content.SaveChanges();
        }

        private static Dictionary<string, Category> categoty;

        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (categoty == null)
                {
                    var list = new Category[]
                    {
                         new Category{CategoryName="Электромобили",Desk="Современный автомобиль" },
                         new Category{CategoryName="Класические автомобили",Desk="Внутреннее сгорание" },
                    };
                    categoty = new Dictionary<string, Category>();
                    foreach (Category el in list)
                    {
                        categoty.Add(el.CategoryName, el);
                    }
                }
                return categoty;
            }
        }
    }
}