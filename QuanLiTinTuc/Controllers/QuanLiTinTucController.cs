using QuanLiTinTuc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLiTinTuc.Controllers
{
    public class QuanLiTinTucController : Controller
    {
        QLTinTuc db = new QLTinTuc();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ThemTinTuc() 
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemTinTuc(TinTuc model)
        {
            string NoiDung = Request.Unvalidated.Form["str_NoiDung"];
            string ChuDe = Request["ChuDe"];
            string TieuDe = Request["TieuDe"];
            string TomTat = Request["TomTat"];
            string Anh = Request["Anh"];
            string TacGia = Request["TacGia"];

            model.NoiDung = NoiDung;
            model.ChuDe = ChuDe;
            model.TieuDe = TieuDe;  
            model.TomTat = TomTat;
            model.Anh = Anh;
            model.TacGia = TacGia;

            db.TinTucs.Add(model);
            db.SaveChanges();

            return View();
        }
    }
}