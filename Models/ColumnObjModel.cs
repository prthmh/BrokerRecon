using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrokerRecon.Models
{
    public class ColumnObjModel
    {
        public int ID { get; set; }
        public string ColumnName { get; set; }
        public string DataType { get; set; }

        public string Value {  get; set; }
    }
}