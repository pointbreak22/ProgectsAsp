using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Desk { get; set; }
        public List<Car> Cars { get; set; }     //список товаров в категорию
    }
}