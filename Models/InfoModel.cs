using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrokerRecon.Models
{
    public class InfoModel
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Match { get; set; }
        public string Warning { get; set; }
        public string MisMatch { get; set; }

        public List<FileModel> Files { get; set; }
    }
}