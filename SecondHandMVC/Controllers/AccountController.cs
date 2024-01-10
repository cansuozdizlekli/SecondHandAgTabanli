using System;
using Microsoft.AspNetCore.Mvc;

namespace SecondHandMVC.Controllers
{
	public class AccountController : Controller
	{
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}

