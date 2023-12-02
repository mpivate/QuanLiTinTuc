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
            model.NgayDang = DateTime.Now;

            db.TinTucs.Add(model);
            db.SaveChanges();

            return View();
        }

        public ActionResult SuaXoaTinTuc()
        {
            List<TinTuc> list_TinTuc = db.TinTucs.ToList();
            return View(list_TinTuc);
        }
        public ActionResult SuaTinTucView(int Id)
        {
            var baiviet = db.TinTucs.Where(e => e.Id == Id).FirstOrDefault();
            return View(baiviet);
        }

        public ActionResult SuaTinTuc(int Id)
        {
            var baiviet = db.TinTucs.Find(Id);

            string NoiDung = Request.Unvalidated.Form["str_NoiDung"];
            string ChuDe = Request["ChuDe"];
            string TieuDe = Request["TieuDe"];
            string TomTat = Request["TomTat"];
            string Anh = Request["Anh"];
            string TacGia = Request["TacGia"];

            baiviet.NoiDung = NoiDung;
            baiviet.ChuDe = ChuDe;
            baiviet.TomTat = TomTat;
            baiviet.TacGia = TacGia;
            baiviet.Anh = Anh;
            baiviet.TieuDe = TieuDe;

            db.SaveChanges();

            return RedirectToAction("SuaXoaTinTuc");
        }

        public ActionResult XoaTinTuc(int Id)
        {
            var baiviet = db.TinTucs.Find(Id);

            if (baiviet == null)
            {
                // Tin tức không tồn tại, có thể xử lý thông báo lỗi hoặc chuyển hướng
                return HttpNotFound();
            }

            db.TinTucs.Remove(baiviet);
            db.SaveChanges();

            return RedirectToAction("SuaXoaTinTuc");
        }
    }
}