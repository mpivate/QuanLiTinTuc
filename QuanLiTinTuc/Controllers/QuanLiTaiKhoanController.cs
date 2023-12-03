using Newtonsoft.Json;
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
        //Get view:QLTaiKhoanAdmin
        public ActionResult QLTaiKhoanAdmin()
        {
            return View();
        }
        //Get view:SuaThongTinTaiKhoan
        public ActionResult SuaThongTinTaiKhoan()
        {
            return View();
        }
        //Get view:ThemTaiKhoanAdmin
        public ActionResult ThemTaiKhoanAdmin()
        {
            return View();
        }

        //Lay danh sach TK
        public string get_All()
        {
            APIResult_ett<List<Account>> rs = new APIResult_ett<List<Account>>();
            try
            {
                //truy vấn db để lấy toàn bộ dữ liệu về ds sinh viên
                var qr = db.Accounts;
                if (qr.Any())
                {
                    //có dữ liệu => chính là dssv
                    rs.ErrCode = EnumErrCode.Success;
                    rs.ErrDesc = "Lấy Danh sách tài khoản thành công";
                    rs.Data = qr.ToList();
                }
                else
                {
                    //không có dữ liệu thỏa mãn
                    rs.ErrCode = EnumErrCode.Empty;
                    rs.ErrDesc = "Danh sách tài khoản rỗng";
                    rs.Data = null;
                }
            }
            catch (Exception ex)
            {
                rs.ErrCode = EnumErrCode.Error;
                rs.ErrDesc = "Có lỗi xảy ra trong quá trình lấy về Danh sách tài khoản. Chi tiết lỗi: " + ex.Message;
                rs.Data = null;
            }

            return JsonConvert.SerializeObject(rs);
        }

        //Thêm tài khoản
        public string Add()
        {
            //ví dụ về linq to sql
            //var qr = db.tbl_SVs; //select * from tbl_SV
            //var qr1 = db.tbl_SVs.Where(o => o.MSSV == "1234"); //select * from tbl_SV where mssv == "1234"


            string tk = Request["txt_UserName"];
            string mk = Request["txt_Password"];
            string hoten = Request["txt_FullName"];
            string quyenTruyCap = Request["cbx_quyenTruyCap"];

            string nhaclaimk = Request["txt_nhaclaiPassword"];
            var qr = db.Accounts.Where(o => o.UserName == tk);

            //validate input
            if (!string.IsNullOrEmpty(tk) && !string.IsNullOrEmpty(hoten) && !string.IsNullOrEmpty(mk) && !string.IsNullOrEmpty(nhaclaimk))
            {
                if (!qr.Any())
                {
                    if (mk == nhaclaimk)
                    {
                        try
                        {
                            //trường hợp muốn insert
                            Account sv = new Account();
                            sv.UserName = tk;
                            sv.PassWord = mk;
                            sv.FullName = hoten;
                            sv.QuyenTruyCap = quyenTruyCap;

                            db.Accounts.Add(sv);
                            db.SaveChanges();

                            return "Thêm mới tài khoản thành công";

                        }
                        catch (Exception ex)
                        {
                            return "Thêm mới tài khoản thất bại. Chi tiết lỗi: " + ex.Message;
                        }
                        //ok
                        //return "MSSV: " + mssv + "; Họ tên: " + hoten + "; Mật khẩu: " + mk;
                    }
                    else
                    {
                        //trường hợp dữ liệu input không hợp lệ
                        return "Mật khẩu nhắc lại không khớp. Vui lòng kiểm tra lại";
                    }
                }
                else
                {
                    return "Tên đăng nhập đã được sử dụng. Vui lòng chọn tên đăng nhập khác";
                }
            }
            else
            {
                return "Nhập thiếu thông tin tài khoản, vui lòng kiểm tra lại";
            }


        }

        public string get_TK_Info()
        {
            string tk = Request["UserName"];
            //validate input
            if (!string.IsNullOrEmpty(tk))
            {
                try
                {
                    //trường hợp lấy được mssv từ trang suaSV
                    var qr = db.Accounts.Where(o => o.UserName == tk);
                    if (qr.Any())
                    {
                        //trường hợp có dữ liệu trả về - tìm thấy sv có mssv theo yêu cầu
                        var sv_obj = qr.SingleOrDefault();

                        return JsonConvert.SerializeObject(sv_obj);
                    }
                    else
                    {
                        return "Không tìm thấy TK có Username=" + tk;
                    }

                }
                catch (Exception ex)
                {
                    return "Lấy thông tin sinh viên thất bại. Chi tiết lỗi: " + ex.Message;
                }
                //ok
                //return "MSSV: " + mssv + "; Họ tên: " + hoten + "; Mật khẩu: " + mk;

            }
            else
            {
                return "Vui lòng chọn sinh viên để chỉnh sửa";
            }
        }

        public string Edit()
        {
            //ví dụ về linq to sql
            //var qr = db.tbl_SVs; //select * from tbl_SV
            //var qr1 = db.tbl_SVs.Where(o => o.MSSV == "1234"); //select * from tbl_SV where mssv == "1234"


            string tk = Request["txt_UserName"];
            string Password = Request["txt_Password"];
            string hoTen = Request["txt_FullName"];
            string nhaclaimk = Request["txt_nhaclaiPassword"];
            string quyenTruyCap = Request["cbx_quyenTruyCap"];

            //validate input
            if (!string.IsNullOrEmpty(tk) && !string.IsNullOrEmpty(hoTen) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(nhaclaimk))
            {

                if (Password == nhaclaimk)
                {
                    try
                    {
                        //trường hợp muốn update
                        var qrs = db.Accounts.Where(o => o.UserName == tk);
                        if (qrs.Any())
                        {
                            //có trả về bản ghi.
                            Account sv = qrs.SingleOrDefault();
                            sv.FullName = hoTen;
                            sv.PassWord = Password;
                            sv.QuyenTruyCap = quyenTruyCap;


                            db.SaveChanges();

                            return "Cập nhật thông tin sinh viên thành công";
                        }
                        else
                        {
                            return "KHÔNG tìm thấy Tk có tên đăng nhập = " + tk;
                        }
                    }
                    catch (Exception ex)
                    {
                        return "Cập nhật thông tin sinh viên thất bại. Chi tiết lỗi: " + ex.Message;
                    }
                }
                else
                {
                    //trường hợp dữ liệu input không hợp lệ
                    return "Mật khẩu nhắc lại không khớp. Vui lòng kiểm tra lại";
                }
            }
            else
            {
                //trường hợp mssv đã bị sửa
                return "MSSV không khớp với dữ liệu trong CSDL";
            }
        }





        public string Del_TK()
        {
            APIResult_ett<string> rs = new APIResult_ett<string>();

            try
            {
                //xử lý trường hợp xóa
                //lấy về mssv cần xóa
                string tk = Request["tk"];
                if (!string.IsNullOrEmpty(tk))
                {
                    //thực hiện xóa và nên xóa mềm
                    var qr = db.Accounts.Where(o => o.UserName == tk);
                    if (qr.Any())
                    {
                        Account taiKhoan = qr.SingleOrDefault();
                        //trường hợp có dữ liệu
                        db.Accounts.Remove(taiKhoan);
                        db.SaveChanges();

                        rs.ErrCode = EnumErrCode.Success;
                        rs.ErrDesc = "Xóa TK có tên đăng nhập " + tk + " thành công";

                    }
                    else
                    {
                        rs.ErrCode = EnumErrCode.NotExistent;
                        rs.ErrDesc = "Xóa TK có tên đăng nhập " + tk + " thất bại do không tìm thấy";
                        rs.Data = null;

                    }
                }
                else
                {
                    rs.ErrCode = EnumErrCode.Empty;
                    rs.ErrDesc = "Vui lòng nhập tài khoản cần xóa";
                    rs.Data = null;
                }
            }
            catch (Exception ex)
            {
                rs.ErrCode = EnumErrCode.Error;
                rs.ErrDesc = "Xóa sinh viên thất bại. Chi tiết lỗi: " + ex.Message;
                rs.Data = null;
            }

            return JsonConvert.SerializeObject(rs);
        }

        public string Search_TK()
        {
            APIResult_ett<List<Account>> rs = new APIResult_ett<List<Account>>();
            try
            {
                string search_val = Request["search_val"];

                if (!string.IsNullOrEmpty(search_val))
                {
                    //truy vấn db để lấy toàn bộ dữ liệu về ds sinh viên
                    IQueryable<Account> qr = null;



                    qr = db.Accounts.Where(o => o.UserName.Contains(search_val) || o.FullName.Contains(search_val) || o.QuyenTruyCap.Contains(search_val));



                    if (qr.Any())
                    {
                        //có dữ liệu => chính là dssv
                        rs.ErrCode = EnumErrCode.Success;
                        rs.ErrDesc = "Tìm kiếm tài khoản thành công";
                        rs.Data = qr.ToList();
                    }
                    else
                    {
                        //không có dữ liệu thỏa mãn
                        rs.ErrCode = EnumErrCode.Empty;
                        rs.ErrDesc = "Không tìm thấy tài khoản thỏa mãn điều kiện tìm kiếm";
                        rs.Data = null;
                    }
                }
                else
                {
                    //get all
                    var qr = db.Accounts;
                    if (qr.Any())
                    {
                        //có dữ liệu => chính là dssv
                        rs.ErrCode = EnumErrCode.Success;
                        rs.ErrDesc = "Lấy DSSV thành công";
                        rs.Data = qr.ToList();
                    }
                    else
                    {
                        //không có dữ liệu thỏa mãn
                        rs.ErrCode = EnumErrCode.Empty;
                        rs.ErrDesc = "DSSV rỗng";
                        rs.Data = null;
                    }

                    //rs.ErrCode = EnumErrCode.InputEmpty;
                    //rs.ErrDesc = "Vui lòng nhập đầy đủ giá trị và tiêu chí cần tìm kiếm";
                    //rs.Data = null;
                }

            }
            catch (Exception ex)
            {
                rs.ErrCode = EnumErrCode.Error;
                rs.ErrDesc = "Có lỗi xảy ra trong quá trình lấy về DSSV. Chi tiết lỗi: " + ex.Message;
                rs.Data = null;
            }

            return JsonConvert.SerializeObject(rs);
        }
    }
}
