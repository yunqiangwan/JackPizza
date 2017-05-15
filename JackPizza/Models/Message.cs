using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JackPizza.Models
{
    public class Message
    {
        public int ID { get; set; }
        public string MessageHead { get; set; }
        public string MessageBody { get; set; }

        public string MessagePurpose { get; set; }
        public int TargetCustomers { get; set; }
    }
}