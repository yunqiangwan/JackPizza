using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JackPizza.Models
{
    public class PizzaStore
    {
        public int PizzaStoreId { get; set; }

        [Display(Name = "Store Name")]
        [Required(ErrorMessage = "Store name is required")]
        [MaxLength(45, ErrorMessage = "The Store name can be maximum 45 characters long")]
        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        [Display(Name = "Decription")]
        public string Decription { get; set; }

    }
}