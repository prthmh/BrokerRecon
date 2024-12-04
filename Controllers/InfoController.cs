using BrokerRecon.Enums;
using BrokerRecon.Helper;
using BrokerRecon.Models;
using DevExpress.Utils.About;

//using ClosedXML.Excel;
using Microsoft.Ajax.Utilities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace BrokerRecon.Controllers
{
    public class InfoController : Controller
    {
        private void SetSession(string key, object value)
        {
            Session[key] = value;
        }

        private object GetSession(string key)
        {
            return Session[key];
        }


        // GET: Info
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ExcelData()
        {
            return View();
        }

        private DataTable GetInfoDataTable()
        {
            Debug.WriteLine(GetSession("InfoTable"));
            Debug.WriteLine(GetSession("InfoTable") as DataTable);
            //if(GetSession("InfoTable") == null)
            //{
            //    dt = TempData["InfoTable"] as DataTable;

            //} else
            //{
            //    dt = GetSession("InfoTable") as DataTable;
            //}

            //if (GetSession("InfoTable") != null)
            //{
            //    return GetSession("InfoTable") as DataTable;
            //}

            //DataTable dt = InfoHelper.InfoData();
            ////Session["InfoTable"] = dt;
            //SetSession("InfoTable", dt);
            //return dt;



            DataTable dt = new DataTable();
            if (TempData["InfoTable"] != null)
            {
                dt = TempData["InfoTable"] as DataTable;
                //SetSession("InfoTable", dt);
                return dt;
            }

            //if (GetSession("InfoTable") != null)
            //{
            //    dt = GetSession("InfoTable") as DataTable;
            //    return dt;
            //}

            dt = InfoHelper.InfoData();
            TempData["InfoTable"] = dt;
            return dt;

        }

        [HttpPost]

        public ActionResult Insert(InfoModel info)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    errors = ModelState.Values
                        .SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                });
            }

            DataTable dt = this.GetInfoDataTable();
            Debug.WriteLine("info.Code", info.Code);
            Debug.WriteLine("info.Name", info.Name);
            DataRow dr = dt.NewRow();
            dr[nameof(InfoModel.Code)] = info.Code ?? string.Empty;
            dr[nameof(InfoModel.Name)] = info.Name ?? string.Empty;
            dr[nameof(InfoModel.Description)] = info.Description ?? string.Empty;
            dr[nameof(InfoModel.Match)] = info.Match ?? string.Empty;
            dr[nameof(InfoModel.Warning)] = info.Warning ?? string.Empty;
            dr[nameof(InfoModel.MisMatch)] = info.MisMatch ?? string.Empty;

            string sourceFileAdaptorType = string.Empty;
            string sourceERReport = string.Empty;
            //HttpPostedFileBase sourceExcelFile;

            string targetFileAdaptorType = string.Empty;
            string targetERReport = string.Empty;
            //HttpPostedFileBase targetExcelFile;

            if (info.Files != null)
            {
                foreach (FileModel file in info.Files)
                {
                    if (file.Indicator == IndicatorType.Source)
                    {
                        sourceFileAdaptorType = file.FileAdaptorType ?? string.Empty;
                        sourceERReport = file.ERReport ?? string.Empty;
                        //if(file.ExcelFile != "")
                        //{
                        //sourceExcelFile = file.ExcelFile;
                        //}
                    }

                    if (file.Indicator == IndicatorType.Target)
                    {
                        targetFileAdaptorType = file.FileAdaptorType ?? string.Empty;
                        targetERReport = file.ERReport ?? string.Empty;
                        //targetExcelFile = file.ExcelFile ?? string.Empty;
                    }

                }
            }

            dr[$"{nameof(FileModel.Indicator)}_Source"] = IndicatorType.Source.ToString();
            dr[$"{nameof(FileModel.FileAdaptorType)}_Source"] = sourceFileAdaptorType;
            dr[$"{nameof(FileModel.ERReport)}_Source"] = sourceERReport;
            //dr[$"{nameof(FileModel.ExcelFile)}_Source"] = sourceExcelFile;

            dr[$"{nameof(FileModel.Indicator)}_Target"] = IndicatorType.Target.ToString();
            dr[$"{nameof(FileModel.FileAdaptorType)}_Target"] = targetFileAdaptorType;
            dr[$"{nameof(FileModel.ERReport)}_Target"] = targetERReport;
            //dr[$"{nameof(FileModel.ExcelFile)}_Target"] = targetExcelFile;

            dt.Rows.Add(dr);
            dt.AcceptChanges();

            //Session["InfoTable"] = dt;
            //DataTable dt2 = Session["InfoTable"] as DataTable;
            //Debug.WriteLine(dt2.Rows.Count);
            //SetSession("InfoTable", dt);
            TempData["InfoTable"] = dt;
            int newId = Convert.ToInt32(dr[nameof(InfoModel.ID)]);
            
            return Json(new { success = true, infoID = newId });
        }

        //public ActionResult Insert(InfoModel info)

        ////public ActionResult Insert(Var obj)
        //{
        //    Debug.WriteLine("info", info);
        //    //Debug.WriteLine("sourceExcelFile", sourceExcelFile);
        //    //Debug.WriteLine("targetExcelFile", targetExcelFile);
        //    if (!ModelState.IsValid)
        //    {
        //        return Json(new
        //        {
        //            success = false,
        //            errors = ModelState.Values
        //                .SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
        //        });
        //    }

        //    DataTable dt = this.GetInfoDataTable();

        //    DataRow dr = dt.NewRow();
        //    dr[nameof(InfoModel.Code)] = info.Code ?? string.Empty;
        //    dr[nameof(InfoModel.Name)] = info.Name ?? string.Empty;
        //    dr[nameof(InfoModel.Description)] = info.Description ?? string.Empty;
        //    dr[nameof(InfoModel.Match)] = info.Match ?? string.Empty;
        //    dr[nameof(InfoModel.Warning)] = info.Warning ?? string.Empty;
        //    dr[nameof(InfoModel.MisMatch)] = info.MisMatch ?? string.Empty;

        //    string sourceFileAdaptorType = string.Empty;
        //    string sourceERReport = string.Empty;
        //    string sourceExcelFileName = string.Empty;

        //    string targetFileAdaptorType = string.Empty;
        //    string targetERReport = string.Empty;
        //    string targetExcelFileName = string.Empty;

        //    if (info.Files != null)
        //    {
        //        foreach (FileModel file in info.Files)
        //        {
        //            if (file.Indicator == IndicatorType.Source)
        //            {
        //                sourceFileAdaptorType = file.FileAdaptorType ?? string.Empty;
        //                sourceERReport = file.ERReport ?? string.Empty;
        //            }

        //            if (file.Indicator == IndicatorType.Target)
        //            {
        //                targetFileAdaptorType = file.FileAdaptorType ?? string.Empty;
        //                targetERReport = file.ERReport ?? string.Empty;
        //            }
        //        }
        //    }

        //    //// Save uploaded source file
        //    //if (sourceExcelFile != null)
        //    //{
        //    //    sourceExcelFileName = SaveUploadedFile(sourceExcelFile, "~/Uploads/Source/");
        //    //}

        //    //// Save uploaded target file
        //    //if (targetExcelFile != null)
        //    //{
        //    //    targetExcelFileName = SaveUploadedFile(targetExcelFile, "~/Uploads/Target/");
        //    //}
        //    //Debug.WriteLine("sourceExcelFileName", sourceExcelFileName);
        //    //Debug.WriteLine("targetExcelFileName", targetExcelFileName);
        //    dr[$"{nameof(FileModel.Indicator)}_Source"] = IndicatorType.Source.ToString();
        //    dr[$"{nameof(FileModel.FileAdaptorType)}_Source"] = sourceFileAdaptorType;
        //    dr[$"{nameof(FileModel.ERReport)}_Source"] = sourceERReport;
        //    //dr[$"{nameof(FileModel.ExcelFile)}_Source"] = sourceExcelFileName;

        //    dr[$"{nameof(FileModel.Indicator)}_Target"] = IndicatorType.Target.ToString();
        //    dr[$"{nameof(FileModel.FileAdaptorType)}_Target"] = targetFileAdaptorType;
        //    dr[$"{nameof(FileModel.ERReport)}_Target"] = targetERReport;
        //    //dr[$"{nameof(FileModel.ExcelFile)}_Target"] = targetExcelFileName;

        //    dt.Rows.Add(dr);
        //    dt.AcceptChanges();

        //    Session["InfoTable"] = dt;
        //    int newId = Convert.ToInt32(dr[nameof(InfoModel.ID)]);

        //    Debug.WriteLine("ID", newId);
        //    Debug.WriteLine("Code", dr[nameof(InfoModel.Code)]);
        //    Debug.WriteLine("Name", dr[nameof(InfoModel.Name)]);
        //    Debug.WriteLine("Description", dr[nameof(InfoModel.Description)]);
        //    Debug.WriteLine("Match", dr[nameof(InfoModel.Match)]);
        //    Debug.WriteLine("Warning", dr[nameof(InfoModel.Warning)]);
        //    Debug.WriteLine("Mismatch", dr[nameof(InfoModel.MisMatch)]);
        //    Debug.WriteLine("Source File Adaptor", dr[$"{nameof(FileModel.FileAdaptorType)}_Source"]);
        //    Debug.WriteLine("Target File Adaptor", dr[$"{nameof(FileModel.FileAdaptorType)}_Target"]);
        //    //Debug.WriteLine("Source File", dr[$"{nameof(FileModel.ExcelFile)}_Source"]);
        //    //Debug.WriteLine("Target File", dr[$"{nameof(FileModel.ExcelFile)}_Target"]);

        //    return Json(new { success = true });
        //}

        public ActionResult UploadSourceFile(HttpPostedFileBase sourceExcelFile)
        {
            if (sourceExcelFile != null)
            {
                string _FileName = Path.GetFileName(sourceExcelFile.FileName);
                string sourceExcelFileName = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                sourceExcelFile.SaveAs(sourceExcelFileName);
                string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + sourceExcelFileName + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                DataTable data = ExcelUtility.ConvertXSLXtoDataTable(sourceExcelFileName, connString);
                
                //DataTable data = ExcelUtility.ExcelDataToDataTable(sourceExcelFileName, "Sheet1");

                DataTable colTable = GetColumnDataTable(data);
                Debug.WriteLine(colTable);
                ViewBag.Data = colTable;
                //Session["ColumnTable"] = colTable;
                SetSession("SourceTable", colTable);
                return Json(new { success = true, message = "File uploaded successfully!" });
            } else
            {
                return Json(new { success = false, message = "No file selected!" });
            }
        }


        public ActionResult UploadTargetFile(HttpPostedFileBase targetExcelFile)
        {
            if (targetExcelFile != null)
            {
                string _FileName = Path.GetFileName(targetExcelFile.FileName);
                string targetExcelFileName = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                targetExcelFile.SaveAs(targetExcelFileName);
                string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + targetExcelFileName + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                DataTable data = ExcelUtility.ConvertXSLXtoDataTable(targetExcelFileName, connString);

                //DataTable data = ExcelUtility.ExcelDataToDataTable(sourceExcelFileName, "Sheet1");

                DataTable colTable = GetColumnDataTable(data);
                Debug.WriteLine(colTable);
                ViewBag.Data = colTable;
                SetSession("TargetTable", colTable);
                //TempData["TargetTable"] = colTable;
                return Json(new { success = true, message = "File uploaded successfully!" });
            }
            else
            {
                return Json(new { success = false, message = "No file selected!" });
            }
        }

        [HttpGet]
        public ActionResult GetSourceTable()
        {
            if(GetSession("SourceTable") != null)
            {
                DataTable colTable = GetSession("SourceTable") as DataTable;
                List<ColumnObjModel> colList = new List<ColumnObjModel>();
                Debug.WriteLine(colTable);
                foreach (DataRow dr in colTable.Rows)
                {
                    colList.Add(new ColumnObjModel
                    {
                        ID = Convert.ToInt32(dr[nameof(ColumnObjModel.ID)]),
                        ColumnName = dr[nameof(ColumnObjModel.ColumnName)].ToString(),
                        Value = dr[nameof(ColumnObjModel.Value)].ToString()
                    });
                }
                return Json(colList, JsonRequestBehavior.AllowGet);
            }
            List<ColumnObjModel> colList1 = new List<ColumnObjModel>();

            return Json(colList1, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetTargetTable()
        {
            //DataTable tar = TempData["TargetTable"] as DataTable;
            if (GetSession("TargetTable") != null)
            {
                DataTable tar = GetSession("TargetTable") as DataTable;
                List<ColumnObjModel> colList = new List<ColumnObjModel>();
                Debug.WriteLine(tar);
                foreach (DataRow dr in tar.Rows)
                {
                    colList.Add(new ColumnObjModel
                    {
                        ID = Convert.ToInt32(dr[nameof(ColumnObjModel.ID)]),
                        ColumnName = dr[nameof(ColumnObjModel.ColumnName)].ToString(),
                        Value = dr[nameof(ColumnObjModel.Value)].ToString()
                    });
                }
                return Json(colList, JsonRequestBehavior.AllowGet);
            }
            //DataTable tardt = GetSession("TargetTable") as DataTable;
            //Debug.WriteLine(tardt);
            List<ColumnObjModel> colList1 = new List<ColumnObjModel>();

            return Json(colList1, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetTargetList()
        {
            if (GetSession("TargetTable") != null)
            {
                DataTable tar = GetSession("TargetTable") as DataTable;
                List<string> columnNames = tar.Rows
                    .Cast<DataRow>()
                    .Select(dr => dr[nameof(ColumnObjModel.ColumnName)].ToString())
                    .ToList();

                return Json(columnNames, JsonRequestBehavior.AllowGet);
            }

            return Json(new List<string>(), JsonRequestBehavior.AllowGet);
        }

        private DataTable GetColumnDataTable(DataTable data)
        {
            DataTable columnTable = new DataTable();
            DataColumn idColumn = columnTable.Columns.Add(nameof(ColumnObjModel.ID), typeof(int));
            idColumn.AutoIncrement = true;
            idColumn.AutoIncrementSeed = 1;
            idColumn.AutoIncrementStep = 1;

            columnTable.Columns.Add("ColumnName", typeof(string));
            columnTable.Columns.Add("Value", typeof(string));

            DataColumnCollection dc = data.Columns;
            DataRow firstDataRow = data.Rows[0];

            foreach (DataColumn column in data.Columns)
            {
                Debug.WriteLine($"Column Name: {column.ColumnName}");
                DataRow dr = columnTable.NewRow();
                dr["ColumnName"] = column.ColumnName;
                dr["Value"] = firstDataRow[column.ColumnName];
                columnTable.Rows.Add(dr);
            }



            columnTable.AcceptChanges();

            return columnTable;
        }


        //private string SaveUploadedFile(HttpPostedFileBase file, string uploadPath)
        //{
        //    string serverPath = Server.MapPath(uploadPath);

        //    // Ensure the directory exists
        //    if (!Directory.Exists(serverPath))
        //    {
        //        Directory.CreateDirectory(serverPath);
        //    }

        //    // Save the file with its original name
        //    string filePath = Path.Combine(serverPath, Path.GetFileName(file.FileName));
        //    file.SaveAs(filePath);
        //    //ReadFromExcelFileInDataSet(filePath);

        //    return file.FileName; // Return the file name
        //}

        //public DataSet ReadFromExcelFileInDataSet(string importFilePath)
        //{
        //    //DataSet data = ReadExcelInToDataset(importFilePath) ?? ReadExcelInToDatasetFromExcelToXml86(importFilePath, downloadedAttachments);
        //    //return data;
        //    DataSet data = new DataSet();
           
        //    using (XLWorkbook workBook = new XLWorkbook(importFilePath))
        //    {
        //        foreach (var sheet in workBook.Worksheets)
        //        {
        //            DataTable dataTable = new DataTable();
        //            dataTable = ReadExcelToDatatable(workBook, sheet.Name);
        //            dataTable.TableName = sheet.Name;
        //            data.Tables.Add(dataTable);
        //        }
        //        return data;
        //    }
        //}


        //private DataTable ReadExcelToDatatable(XLWorkbook workBook, string sheetName)
        //{
        //    var noofRows = workBook.Worksheet(sheetName).RowsUsed();
        //    //Create a new DataTable.
        //    DataTable dataTable = new DataTable();

        //    //Loop through the Worksheet rows.
        //    bool firstRow = true;
        //    foreach (IXLRow row in noofRows)//workSheet.Rows())
        //    {
        //        //Use the first row to add columns to DataTable.
        //        if (firstRow)
        //        {
        //            foreach (IXLCell cell in row.Cells())
        //            {
        //                if (!string.IsNullOrEmpty(cell.Value.ToString()))
        //                {

        //                    if (dataTable.Columns.Contains(cell.Value.ToString()) && cell.Value.ToString() == "Legal Description")
        //                    {
        //                        string updatedCellValue = cell.Value.ToString() + 1;
        //                        dataTable.Columns.Add(updatedCellValue);
        //                    }
        //                    else
        //                        //--
        //                        dataTable.Columns.Add(cell.Value.ToString());
        //                }
        //                else
        //                {
        //                    break;
        //                }
        //            }
        //            firstRow = false;
        //        }
        //        else
        //        {
        //            int i = 0;
        //            DataRow toInsert = dataTable.NewRow();
        //            foreach (IXLCell cell in row.Cells(1, dataTable.Columns.Count))
        //            {
        //                try
        //                {
        //                    toInsert[i] = cell.Value.ToString();
        //                }
        //                catch (Exception ex)
        //                {

        //                }
        //                i++;
        //            }
        //            dataTable.Rows.Add(toInsert);
        //        }
        //    }
        //    Debug.WriteLine("datatable rows", dataTable.Rows.Count);
        //    return dataTable;
        //}

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                
                DataTable dt = this.GetInfoDataTable();
                
                var infoList = dt.AsEnumerable().Select(row => new InfoModel
                {
                    ID = Convert.ToInt32(row[nameof(InfoModel.ID)]),
                    Code = row[nameof(InfoModel.Code)]?.ToString(),
                    Name = row[nameof(InfoModel.Name)]?.ToString(),
                    Description = row[nameof(InfoModel.Description)]?.ToString(),
                    Match = row[nameof(InfoModel.Match)]?.ToString(),
                    Warning = row[nameof(InfoModel.Warning)]?.ToString(),
                    MisMatch = row[nameof(InfoModel.MisMatch)]?.ToString(),

                    
                    Files = new List<FileModel>
            {
                new FileModel
                {
                    Indicator = IndicatorType.Source,
                    FileAdaptorType = row[$"{nameof(FileModel.FileAdaptorType)}_Source"]?.ToString(),
                    ERReport = row[$"{nameof(FileModel.ERReport)}_Source"]?.ToString(),
                    //ExcelFile = row[$"{nameof(FileModel.ExcelFile)}_Source"]?.ToString()
                },
                new FileModel
                {
                    Indicator = IndicatorType.Target,
                    FileAdaptorType = row[$"{nameof(FileModel.FileAdaptorType)}_Target"]?.ToString(),
                    ERReport = row[$"{nameof(FileModel.ERReport)}_Target"]?.ToString(),
                    //ExcelFile = row[$"{nameof(FileModel.ExcelFile)}_Target"]?.ToString()
                }
            }
                }).ToList();

                
                return Json(new
                {
                    success = true,
                    data = infoList
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                
                return Json(new
                {
                    success = false,
                    message = "Error retrieving information: " + ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult GetSavedExcelTable()
        {
            DataTable dt = GetSession("ExcelTable") as DataTable;
            var list = dt.AsEnumerable().Select(row => new Student
            {
                Name = row["Name"]?.ToString(),
                Age = Convert.ToInt32(row["Age"])
            }).ToList();

            return Json(new
            {
                success = true,
                data = list
            }, JsonRequestBehavior.AllowGet);
        }

    }


    public class ExcelUtility
    {
        public static DataTable ExcelDataToDataTable(string filePath, string sheetName, bool hasHeader = true)
        {
            var dt = new DataTable();
            var fi = new FileInfo(filePath);
            // Check if the file exists
            if (!fi.Exists)
                throw new Exception("File " + filePath + " Does Not Exists");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var xlPackage = new ExcelPackage(fi);
            // get the first worksheet in the workbook
            var worksheet = xlPackage.Workbook.Worksheets[sheetName];

            dt = worksheet.Cells[1, 1, worksheet.Dimension.End.Row, worksheet.Dimension.End.Column].ToDataTable(c =>
            {
                c.FirstRowIsColumnNames = true;
            });

            return dt;
        }


        public static DataTable ConvertXSLXtoDataTable(string strFilePath, string connString)
        {
            OleDbConnection oledbConn = new OleDbConnection(connString);
            DataTable dt = new DataTable();
            try
            {
                oledbConn.Open();
                using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn))
                {
                    OleDbDataAdapter oleda = new OleDbDataAdapter();
                    oleda.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    oleda.Fill(ds);

                    dt = ds.Tables[0];
                }
            }
            catch
            {
                // Handle exceptions
            }
            finally
            {
                oledbConn.Close();
            }

            return dt;
        }
    }
}