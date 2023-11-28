using QuanLiTinTuc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace QuanLiTinTuc.Controllers
{
    public class QuanLiTaiKhoanController : Controller
    {
        // GET: QuanLiTaiKhoan
        QLTinTuc db = new QLTinTuc();

        // GET: QuanLiAccount
        public ActionResult Index()
        {
            List<Account> DanhSachTk = db.Accounts.ToList();
            return View(DanhSachTk);
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Account acc)
        {
            string fullName = Request["fullName"];
            string userName = Request["userName"];
            string password = Request["password"];
            string retypePassword = Request["retypePassword"];

            List <string> list_acc = db.Accounts.Select(p => p.UserName).ToList();
            if (list_acc.Contains(userName))
            {
                var errorMessage = "Tên người dùng đã tồn tại!";
                return Json(new { success = false, message = errorMessage, status = 500 }, JsonRequestBehavior.AllowGet);
            }

            acc.UserName = userName;
            acc.PassWord = password;
            acc.FullName = fullName;
            acc.QuyenTruyCap = "nguoidung";

            db.Accounts.Add(acc);
            db.SaveChanges();

            return Json(new { success = true, message = "Đăng kí thành công" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Account acc)
        {
            string userName = Request["userName"];
            string password = Request["password"];
            Session["userName"] = userName;

            List<Account> list_acc = db.Accounts.ToList();
            foreach(var ac in list_acc)
            {
                if (ac.UserName == userName)
                {
                    if (ac.PassWord == password)
                    {
                        return Json(new { status = 200, message = "Đăng nhập thành công" });
                    }
                    else return Json(new { status = 403, message = "Nhập sai mật khẩu vui lòng nhập lại!" });
                }   
            }
            return Json(new { status = 404, message = "Tên đăng nhập không tồn tại!" });
        }
    }
}
