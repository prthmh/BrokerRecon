using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrokerRecon.Models
{
    public class CityModel
    {
        public int id { get; set; }
        public string city { get; set; }
        public int stateId { get; set; }
    }
}