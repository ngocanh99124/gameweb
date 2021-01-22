using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using GameWeb_XL.Controllers.DbModules; 

namespace GameWeb_XL.Controllers
{
    public class DetailController : Controller
    {
        // GET: Detail
        [Authorize]
        public ActionResult Index(string id)
        {
            var model = DbNguoiChoi.get(id);
            return View(model);
        }

        // GET: Detail/Details/5
        [Authorize]
        public ActionResult Details(string id)
        {
            var model = DbNguoiChoi.get(id);
            return View(model);
        }

        // GET: Detail/Create
        [Authorize]
        [HttpPost]
        public ActionResult Upload_Img(HttpPostedFileBase imageFile)
        {
            var model = DbNguoiChoi.get(User.Identity.Name);
            if (imageFile.ContentLength > 0)
            {

                string fileName = imageFile.FileName;
                string src = "/Content/images/";
                string path = Path.Combine(src, fileName);
                //imageFile.SaveAs(fileName);
                if (path != null)
                {
                    DbNguoiChoi.updateImage(Request.Cookies["userID"].Value, path);
                    return RedirectToAction("Details/" + Request.Cookies["userID"].Value, "Detail");
                }

            }

            return View("Details/" + Request.Cookies["userID"].Value, model);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Detail/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Detail/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Detail/Edit/5
        [Authorize]
        [HttpPost]
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

        // GET: Detail/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Detail/Delete/5
        [Authorize]
        [HttpPost]
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
    }
}
