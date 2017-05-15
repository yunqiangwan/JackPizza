using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JackPizza.Models;

namespace JackPizza.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}