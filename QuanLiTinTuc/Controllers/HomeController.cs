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
            return RedirectToAction("TheGioi");
        }

        //Chủ đề Thế giới
        public ActionResult TheGioi()
        {
            List<TinTuc> DanhSachTinTuc = db.TinTucs.Where(e => e.ChuDe == "TheGioi").ToList();
            return View(DanhSachTinTuc);
        }
        public ActionResult TheGioiView(int Id)
        {
            TinTuc TinThoaMan = db.TinTucs.Where(e => e.Id == Id).FirstOrDefault(); 
            return View(TinThoaMan);
        }

        //Chủ đề Kinh Doanh
        public ActionResult KinhDoanh()
        {
            List<TinTuc> DanhSachTinTuc = db.TinTucs.Where(e => e.ChuDe == "KinhDoanh").ToList();
            return View(DanhSachTinTuc);
        }
        public ActionResult KinhDoanhView(int Id)
        {
            TinTuc TinThoaMan = db.TinTucs.Where(e => e.Id == Id).FirstOrDefault();
            return View(TinThoaMan);
        }

        //Chủ đề Bất động sản
        public ActionResult BatDongSan()
        {
            List<TinTuc> DanhSachTinTuc = db.TinTucs.Where(e => e.ChuDe == "BatDongSan").ToList();
            return View(DanhSachTinTuc);
        }
        public ActionResult BatDongSanView(int Id)
        {
            TinTuc TinThoaMan = db.TinTucs.Where(e => e.Id == Id).FirstOrDefault();
            return View(TinThoaMan);
        }

        //Chủ đề khoa học
        public ActionResult KhoaHoc()
        {
            List<TinTuc> DanhSachTinTuc = db.TinTucs.Where(e => e.ChuDe == "KhoaHoc").ToList();
            return View(DanhSachTinTuc);
        }
        public ActionResult KhoaHocView(int Id)
        {
            TinTuc TinThoaMan = db.TinTucs.Where(e => e.Id == Id).FirstOrDefault();
            return View(TinThoaMan);
        }

        //Chủ đề Giải trí
        public ActionResult GiaiTri()
        {
            List<TinTuc> DanhSachTinTuc = db.TinTucs.Where(e => e.ChuDe == "GiaiTri").ToList();
            return View(DanhSachTinTuc);
        }
        public ActionResult GiaiTriView(int Id)
        {
            TinTuc TinThoaMan = db.TinTucs.Where(e => e.Id == Id).FirstOrDefault();
            return View(TinThoaMan);
        }



    }
}