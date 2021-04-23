using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CarId { get; set; }
        public uint price { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public virtual Car Car { get; set; }
        public virtual Order Order { get; set; } //описание всех товаров
    }
}