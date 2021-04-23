using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Models
{
    public class Order
    {
        [BindNever]    //поле никогда не показывается
        public int Id { get; set; }

        [Display(Name = "Введите имя")]
        [StringLength(5)]
        [Required(ErrorMessage = "Длина имени не менее 5 символов")]     //проверка входа
        public string name { get; set; }

        [Display(Name = "Введите Фамилию")]
        [StringLength(5)]
        [Required(ErrorMessage = "Длина имени не менее 5 символов")]
        public string surname { get; set; }

        [Display(Name = "Введите Адрес")]
        [StringLength(5)]
        [Required(ErrorMessage = "Длина имени не менее 5 символов")]
        public string adress { get; set; }

        [Display(Name = "Номер телефона")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(15)]
        [Required(ErrorMessage = "Длина имени не менее 5 символов")]
        public string phone { get; set; }

        [Display(Name = "Введите имя")]
        [DataType(DataType.EmailAddress)]
        //   [StringLength(5)]
        // [Required(ErrorMessage = "Длина имени не менее 5 символов")]
        public string email { get; set; }

        [BindNever]    //поле никогда не показывается
        [ScaffoldColumn(false)]              //чтоб вообще не отображалось в исходном коде
        public DateTime olderTime { get; set; }

        public List<OrderDetail> OrderDetails { get; set; } //описание всех товаров
    }
}