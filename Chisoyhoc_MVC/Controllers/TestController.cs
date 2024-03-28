﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassChung;
using System.IO;

namespace Chisoyhoc_MVC.Controllers
{
    public class TestController : Controller
    {
        double unit = new UnitOf.Length().FromMeters(1).ToMillimeters();        
        
        //
        // GET: /Test/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            KetnoiDB db = new KetnoiDB();
            ViewBag.Message = db.GetTenchiso("C_A01");
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult ProcessInput(string inputData)
        {
            ViewBag.InputData = inputData;
            return View("Index");
        }

        public ActionResult DownloadExcel()
        {
            // Path to the Excel file in the Temp folder
            string filePath = Server.MapPath("~/Temp/Test.xlsx");

            // Check if the file exists
            if (System.IO.File.Exists(filePath))
            {
                // Return the file as a downloadable response
                return File(filePath, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Test.xlsx");
            }
            else
            {
                // If the file doesn't exist, return a HttpNotFound result or any other appropriate response
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult UploadExcel(HttpPostedFileBase excelFile)
        {
            if (excelFile != null && excelFile.ContentLength > 0)
            {
                try
                {
                    string fileName = Path.GetFileName(excelFile.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Temp"), fileName);

                    // Save the uploaded file, overwriting if it already exists
                    excelFile.SaveAs(filePath);

                    ViewBag.UploadMessage = "File uploaded successfully.";
                }
                catch (Exception ex)
                {
                    ViewBag.UploadMessage = "An error occurred: " + ex.Message;
                }
            }
            else
            {
                ViewBag.UploadMessage = "Please select a file to upload.";
            }
            return View("Index");
        }
	}
}