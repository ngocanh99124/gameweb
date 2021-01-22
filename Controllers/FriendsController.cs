using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameWeb_XL.Models
{
    public class FriendsController : Controller
    {
        public GameSQLEntities context = new GameSQLEntities();
        // GET: Friends
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        //action hủy kb
        public ActionResult HuyKB(string Ten1, string Ten2)
        {

            var result = new AccountModel().HuyKB(Ten1, Ten2);
            if (result != 0 && ModelState.IsValid)
            {

                return RedirectToAction("DanhSach/" + User.Identity.Name, "Friends");
            }
            else
            {
                ModelState.AddModelError("", "Error?");
            }
            return View();
        }

        //nhung ban be chua kb
        [Authorize]
        public ActionResult ToanBoDanhSach(string Ten1, string Ten2)
        {

            var result = new AccountModel().AddFriend(Ten1, Ten2);
            if (result != 0 && ModelState.IsValid)
            {

                return RedirectToAction("DanhSach/" + User.Identity.Name, "Friends");
            }
            else
            {
                ModelState.AddModelError("", "Error?");
            }
            return View();
        }

        [Authorize]
        public ActionResult AcceptFriend(string Ten1, string Ten2)
        {

            var result = new AccountModel().AcceptFriend(Ten1, Ten2);
            if (result != 0 && ModelState.IsValid)
            {

                return RedirectToAction("DanhSach/" + User.Identity.Name, "Friends");
            }
            else
            {
                ModelState.AddModelError("", "Error?");
            }
            return View();
        }
        [Authorize]
        public ActionResult HuyYeuCau(string Ten1, string Ten2)
        {
            var result = new AccountModel().HuyYeuCau(Ten1, Ten2);
            if (result != 0 && ModelState.IsValid)
            {

                return RedirectToAction("DanhSach/" + User.Identity.Name, "Friends");
            }
            else
            {
                ModelState.AddModelError("", "Error?");
            }
            return View();
        }


        [Authorize]
        public ActionResult DanhSach(string id, string search = "")
        {
            //ViewData["Ten"] = User.Identity.Name;
            id = User.Identity.Name;
            object[] sqlparam = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };
            var model = context.Database.SqlQuery<NguoiChoi>("SelectBan @id", sqlparam).ToList();
            if (search != "")
            {
                var model2 = model.FindAll(s => s.Ten.Contains(search));
                return View(model2);
            }
            return View(model);

        }
        [Authorize(Users = "ADMIN")]
        public ActionResult AllFr()
        {
            var model = context.Database.SqlQuery<NguoiChoi>("selectAllFr").ToList();

            return View(model);
        }
        [ChildActionOnly, Authorize]
        public ActionResult ToanBoDanhSach1(string Ten1, string search = "")
        {
            Ten1 = User.Identity.Name;
            object[] sqlparam = new SqlParameter[]
            {
                new SqlParameter("@Ten1", Ten1)
            };
            var model = context.Database.SqlQuery<NguoiChoi>("SelectNotBB @Ten1", sqlparam).ToList();
            if (search != "")
            {
                var model2 = model.FindAll(s => s.Ten.Contains(search));
                return View(model2);
            }
            return View(model);
        }
        [Authorize]
        [ChildActionOnly]
        public ActionResult LoiMoiKB(string TenNhan, string search = "")
        {
            TenNhan = User.Identity.Name;
            object[] sqlparam = new SqlParameter[]
            {
                new SqlParameter("@TenNhan", TenNhan)
            };
            var model = context.Database.SqlQuery<NguoiChoi>("NguoiGuiLoiMoiKB @TenNhan", sqlparam).ToList();
            if (search != "")
            {
                var model2 = model.FindAll(s => s.Ten.Contains(search));
                return View(model2);
            }
            return View(model);

        }
        [Authorize]
        [ChildActionOnly]
        public ActionResult DaGuiLoiMoi(string TenGui, string search = "")
        {
            TenGui = User.Identity.Name;
            object[] sqlparam = new SqlParameter[]
            {
                new SqlParameter("@TenGui", TenGui)
            };
            var model = context.Database.SqlQuery<NguoiChoi>("DaGuiLoiMoi @TenGui", sqlparam).ToList();
            if (search != "")
            {
                var model2 = model.FindAll(s => s.Ten.Contains(search));
                return View(model2);
            }
            return View(model);

        }
    }
}