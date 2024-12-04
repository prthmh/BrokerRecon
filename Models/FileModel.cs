using BrokerRecon.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BrokerRecon.Models
{
    public class FileModel
    {
        public IndicatorType Indicator { get; set; }
        public string FileAdaptorType { get; set; }
        //public HttpPostedFileBase ExcelFile { get; set; }
        public string ERReport {  get; set; }
    }
}