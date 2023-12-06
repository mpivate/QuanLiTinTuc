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
        public ActionResult ChuDe(string ChuDe, string keyword)
        {
            var ketQuaTimKiem = db.TinTucs.AsQueryable();
            if (!string.IsNullOrEmpty(ChuDe))
            {
                ketQuaTimKiem = ketQuaTimKiem.Where(t => t.ChuDe.Contains(ChuDe));
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                ketQuaTimKiem = ketQuaTimKiem.Where(t => t.TieuDe.Contains(keyword));
            }
            return View(ketQuaTimKiem);
        }

        public ActionResult ChuDeView(int Id)
        {
            TinTuc TinThoaMan = db.TinTucs.Where(e => e.Id == Id).FirstOrDefault();
            return View(TinThoaMan);
        }
    }
}