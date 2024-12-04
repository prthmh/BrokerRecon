using BrokerRecon.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BrokerRecon.Helper
{
    public class InfoHelper
    {
        public static DataTable InfoData() { 
            DataTable dt = new DataTable();

            DataColumn dc = dt.Columns.Add(nameof(InfoModel.ID), typeof(int));
            dc.AutoIncrement = true;
            dc.AutoIncrementSeed = 1;
            dc.AutoIncrementStep = 1;

            dt.Columns.Add(nameof(InfoModel.Code), typeof(string));
            dt.Columns.Add(nameof(InfoModel.Name), typeof(string));
            dt.Columns.Add(nameof(InfoModel.Description), typeof(string));
            dt.Columns.Add(nameof(InfoModel.Match), typeof(string));
            dt.Columns.Add(nameof(InfoModel.Warning), typeof(string));
            dt.Columns.Add(nameof(InfoModel.MisMatch), typeof(string));

            dt.Columns.Add($"{nameof(FileModel.Indicator)}_Source", typeof(string));
            dt.Columns.Add($"{nameof(FileModel.FileAdaptorType)}_Source", typeof(string));
            dt.Columns.Add($"{nameof(FileModel.ERReport)}_Source", typeof(string));
            //dt.Columns.Add($"{nameof(FileModel.ExcelFile)}_Source", typeof(string));

            dt.Columns.Add($"{nameof(FileModel.Indicator)}_Target", typeof(string));
            dt.Columns.Add($"{nameof(FileModel.FileAdaptorType)}_Target", typeof(string));
            dt.Columns.Add($"{nameof(FileModel.ERReport)}_Target", typeof(string));
            //dt.Columns.Add($"{nameof(FileModel.ExcelFile)}_Target", typeof(string));

            return dt.Copy();
        }
    }
}