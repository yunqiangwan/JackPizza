using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JackPizza.ViewModels
{
    public class Address
    {
        
        public int AddressID { get; set; }
        public string SUnitNumber { get; set; }
        public string SStreetNumber { get; set; }
        public string SStreet { get; set; }
        public string SSuburb { get; set; }

        public string DUnitNumber { get; set; }
        public string DStreetNumber { get; set; }
        public string DStreet { get; set; }
        public string DSuburb { get; set; }

    }
}