using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameWeb_XL.Models;
using System.Web.Security;
using GameWeb_XL.Controllers.DbModules;
using System.Data.SqlClient;

namespace GameWeb_XL.Controllers
{
    public class newController : Controller
    {
        public static GameSQLEntities database = new GameSQLEntities();

        // GET: new
        [HttpPost]
        public ActionResult createID(string name, string pass)
        {
            var model = new NguoiChoi();
            model.Ten = name;
            model.MatKhau = pass;
            var check = DbModules.DbNguoiChoi.get(name);
            if (check == null)
                return View("Signup", 1);
            if (!check.MatKhau.Equals(pass))
                return View("login"); 
            return View();
        }

        [HttpPost]

        public ActionResult HuyKetBan(string Ten1, string Ten2)
        {
            object[] sqlparam = new SqlParameter[]
          {
                new SqlParameter("@Ten1", Ten1),
                new SqlParameter("@Ten2", Ten2)

          };
            var a = 0;
            if (Ten1 == null || Ten2 == null)
                a = 0;
            else
                a = database.Database.ExecuteSqlCommand("HuyKB @Ten1, @Ten2", sqlparam);
            if (a != 0 && ModelState.IsValid)
            {
                return RedirectToAction("DanhSach / " + Ten1, "Friends");
            }
            else
                ModelState.AddModelError("", "huy ket ban khong thanh cong");
            return View("DanhSach / " + Ten1, "Friends");
        }

        [HttpPost]

        public ActionResult changepass(string id, string val, string confirm)
        {
            var a = DbModules.DbNguoiChoi.get(id);
            if (!val.Equals(confirm))
            {
                ModelState.AddModelError("", "Mat khau khong trung nhau");
            }
            else
            if (a == null)
            {
                ModelState.AddModelError("","doi mat khau loi");
            }
            else
            {
                var s1 = a.MatKhau;
                if (s1.Equals(val))
                {
                    ModelState.AddModelError("", "mat khau moi phai khac mat khau cu");
                }
                else
                {
                    object[] sqlparam = new SqlParameter[]
                 {
                    new SqlParameter("@Ten", id),
                    new SqlParameter("@MatKhau", val),
                   
                };
                database.Database.ExecuteSqlCommand("update NguoiChoi where Ten = @Ten set MatKhau = @MatKhau", sqlparam);
                    return RedirectToAction("Details/" + id, "Detail");
                }
                
            }
            return RedirectToAction("ChangePass", "login");
        }
    }
}