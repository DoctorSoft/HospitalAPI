﻿using System.Web.Mvc;

namespace HospitalMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // For test
            return RedirectToAction("Index", "LogIn");

            return View();
        }
    }
}