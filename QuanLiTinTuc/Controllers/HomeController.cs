using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLiTinTuc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string loginName, string password)
        {
            if (loginName.ToLower() == "admin" && password == "1234")
            {
                Session["loginName"] = loginName;
                return RedirectToAction("index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}