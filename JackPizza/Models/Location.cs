using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JackPizza.Models
{
    public class Location
    {
        public int LocationID { get; set; }
        public string UnitNumber { get; set; }
        public string StreetNumber { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}