using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JackPizza.Models
{
    public class SuburbStore
    {
        public int suburbstoreID { get; set; }
        public string suburb { get; set; }
        public string store { get; set; }

        public int? postcode { get; set; }
    }
}