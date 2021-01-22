using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameWeb_XL.Models;
using System.Web.Security;
using GameWeb_XL.Controllers.DbModules;

namespace GameWeb_XL.Controllers
{

    public class LoginController : Controller
    {
        public static GameSQLEntities database = new GameSQLEntities();
        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        // GET:signup
        public ActionResult Signup()
        {
            ViewBag.Mess = "ok";
            return View();
        }

        [AllowAnonymous]
        // GET:signup
        public ActionResult login()
        {
            ViewBag.Mess = "ok";
            return View();
        }
        //login.html
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(LoginModel model)
        {

            var result = LoginModel.login(model.Ten, model.MatKhau);
            if (!DbModules.DbNguoiChoi.isExist(model.Ten))
            {
                ModelState.AddModelError("", "User name or pass word is error?");
                return View();
            }
            if (result && ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(model.Ten, false);
                HttpCookie userIDCookie = new HttpCookie("userID", model.Ten);
                userIDCookie.Expires.AddDays(10);
                HttpContext.Response.SetCookie(userIDCookie);

                return View("../Home/AuthorHome");
            }
            else
            {
                ModelState.AddModelError("", "User name or pass word is error?");
            }
            return View();
        }
        //game
        [Authorize]
        public ActionResult Game()
        {
            return View();
        }
        //ds ban be
        [Authorize]
        public ActionResult DanhSach(string id, string search = "")
        {
            ViewData["Ten"] = User.Identity.Name;
            var model1 = database.BanBes.Where(s => s.Ten1 == id);
            var model2 = database.BanBes.Where(s => s.Ten2 == id);
            List<NguoiChoi> NC = new List<NguoiChoi>();
            foreach (var item in model1)
            {
                NC.AddRange(database.NguoiChois.Where(s => s.Ten == item.Ten2).ToList());
            }
            foreach (var item in model2)
            {
                NC.AddRange(database.NguoiChois.Where(s => s.Ten == item.Ten1).ToList());
            }
            if (search != "")
            {
                var NC1 = NC.FindAll(s => s.Ten.Contains(search));
                return View(NC1);
            }
            return View(NC);

        }
        // GET: Login/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            return View();
        }

        // Post: Login/Create
        [HttpPost]
        public ActionResult Create(string username, string password1)
        {
            var model = new NguoiChoi();
            model.CapDo = 0;
            model.Ten = username;
            model.MatKhau = password1;
            model.HinhAnh = "/Content/images/download.jfif";
            ViewBag.Mess = null;
            if (DbModules.DbNguoiChoi.isExist(model.Ten))
            {
                string a = "ID existed!";
                ViewBag.Mess = a;
                var mo = 1;
                return View("Signup", mo);
            }
            else
            {
                DbModules.DbNguoiChoi.add(model);
                return View("login");
            }

        }


        [Authorize]

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        [Authorize]

        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [Authorize]

        public ActionResult CreateID()
        {
            var model = new NguoiChoi();
            model.CapDo = 0;
            model.Ten = Request.Form["username"].ToString();
            model.MatKhau = Request.Form["password1"].ToString();
            return Content(model.Ten);
        }

        // POST: Login/Delete/5
        [HttpPost]
        [Authorize]

        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult logout()
        {
            if (Request.Cookies["userID"] != null)
            {
                Response.Cookies["userID"].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("login");
        }

        [Authorize]
        [HttpGet]
        public ActionResult ChangePass()
        {
            return View();21```
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePass(LoginModel model)
        {
            model.Ten = User.Identity.Name;
            var result = new AccountModel().changePass(model.Ten, model.MatKhau, model.MatKhauConfirm, model.MatKhaucu);
            if (result && ModelState.IsValid)
            {

                return RedirectToAction("Details/" + User.Identity.Name, "Detail");
            }
            else if (string.IsNullOrEmpty(model.Ten) || string.IsNullOrEmpty(model.MatKhau) || string.IsNullOrEmpty(model.MatKhauConfirm)
                || string.IsNullOrEmpty(model.MatKhaucu))
                ModelState.AddModelError("", "Please fill all information???");
            else if (model.MatKhau != model.MatKhauConfirm)
                ModelState.AddModelError("", "Pass Word confirm is error???");
            else
            {
                ModelState.AddModelError("", "Error?");
            }
            return View();
        }
    }
}
                                                                                 