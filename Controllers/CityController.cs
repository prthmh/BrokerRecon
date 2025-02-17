﻿using BrokerRecon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrokerRecon.Controllers
{
    public class CityController : Controller
    {
        // GET: City
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAllCitites()
        {
            return Json(SampleCityData.cities, JsonRequestBehavior.AllowGet);
        }
    }
}