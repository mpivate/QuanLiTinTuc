using QuanLiTinTuc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLiTinTuc.Controllers
{
    public class HomeController : Controller
    {
        QLTinTuc db = new QLTinTuc();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TheGioi()
        {
            List<TinTuc> DanhSachTinTuc = db.TinTucs.ToList();
            return View(DanhSachTinTuc);
        }
        public ActionResult TheGioiView(int Id)
        {
            TinTuc TinThoaMan = db.TinTucs.Where(e => e.Id == Id).FirstOrDefault(); 
            return View(TinThoaMan);
            
        }
        public ActionResult TheGioiTest()
        {
            List<TinTuc> DanhSachTinTuc = db.TinTucs.ToList();
            return View(DanhSachTinTuc);
        }



    }
}