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
            return RedirectToAction("ChuDe", new { ChuDe = "TheGioi" });
        }

        //Chủ đề
        public ActionResult ChuDe(string ChuDe)
        {
            List<TinTuc> DanhSachTinTuc = db.TinTucs.Where(e => e.ChuDe == ChuDe).ToList();
            return View(DanhSachTinTuc);
        }
        public ActionResult ChuDeView(int Id)
        {
            TinTuc TinThoaMan = db.TinTucs.Where(e => e.Id == Id).FirstOrDefault();
            return View(TinThoaMan);
        }
    }
}