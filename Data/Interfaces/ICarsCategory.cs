using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Interfaces
{                          //получение всех категории в проекте
    public interface ICarsCategory
    {
        IEnumerable<Category> AllCategories { get; }
    }
}