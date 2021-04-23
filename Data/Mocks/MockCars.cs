using Shop.Data.Interfaces;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Mocks
{
    public class MockCars : IAllCars
    {
        private ICarsCategory _categoryCars = new MockCategory();

        public IEnumerable<Car> Cars
        {
            get
            {
                return new List<Car> {
                    new Car
                    {
                        Name="Tesla",
                        ShortDesc="Быстрый",
                        LongDesc="красивый",
                          Imgs = "/images/s1200.jpg",
                        Prise=4500,
                        IsFavourite=true,
                        Available=true,
                        Category=_categoryCars.AllCategories.First()
                    },

                    new Car
                    {
                        Name="Tesla",
                        ShortDesc="Быстрый",
                        LongDesc="красивый",
                        Imgs = "/images/i.webp",
                        Prise=4500,
                        IsFavourite=true,
                        Available=true,
                        Category=_categoryCars.AllCategories.First()
                    }
               };
            }
        }

        public IEnumerable<Car> getFavCars { get; set; }

        public Car getJbjectCar(int carID)
        {
            throw new NotImplementedException();
        }
    }
}