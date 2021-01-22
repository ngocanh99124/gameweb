using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameWeb_XL.Models;
using System.Web.Security;
using GameWeb_XL.Controllers.DbModules;
using Newtonsoft.Json.Linq;

namespace GameWeb_XL.Controllers
{
    public class PlayController : Controller
    {
        // GET: Play
        public static GameSQLEntities database = new GameSQLEntities();
        public static List<NguoiChoi> invite = new List<NguoiChoi>();
        public TwoModel newT = new TwoModel();

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult pickAVT()
        {
            invite = new List<NguoiChoi>();
           
            return View();
        }

        [Authorize]
        [HttpPost]

        public ActionResult setCookie(int avt)
        {
            HttpCookie avtCookie = new HttpCookie("avt", avt.ToString());
            avtCookie.Expires.AddDays(10);
            HttpContext.Response.SetCookie(avtCookie);
            return Json(new { success = true });

        }
        [Authorize]
        public ActionResult waiting()
        {
            
            var id = database.NguoiChois.Find(Request.Cookies["userID"].Value);
            if (id.Ten.Equals("ADMIN"))
            {
                var model = database.Database.SqlQuery<NguoiChoi>("selectAllFr").ToList();
                newT.Require = model;
                newT.Invite = invite;
                return View(newT);
            }
            var model1 = database.BanBes.Where(s => s.Ten1 == id.Ten);
            var model2 = database.BanBes.Where(s => s.Ten2 == id.Ten);
            List<NguoiChoi> NC = new List<NguoiChoi>();
            foreach (var item in model1)
            {
                NC.AddRange(database.NguoiChois.Where(s => s.Ten == item.Ten2).ToList());
            }
            foreach (var item in model2)
            {
                NC.AddRange(database.NguoiChois.Where(s => s.Ten == item.Ten1).ToList());
            }
            newT.Require = NC;
            newT.Invite = invite;
            return View(newT);
        }

        [Authorize]
        [HttpPost]
        public ActionResult accept(string mess)
        {
            var json = JObject.Parse(mess);
            var status = int.Parse((string)json["status"]);
            if (status == 1)
            {
                var friend = (string)json["friend"];
                var id = database.NguoiChois.Find(friend);
                invite.Add(id);
                newT.Invite = invite;
                return PartialView(newT);
            }
            else
            {
                return Json(new
                {
                    RedirectUrl = Url.Action("ready", "Play")
                });
            }
        }

        [Authorize]
        public ActionResult ready()
        {
            return View();
        }
    }
}