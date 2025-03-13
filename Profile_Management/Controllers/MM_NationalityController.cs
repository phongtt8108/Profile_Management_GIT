using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using Profile_Management.Models;
using Profile_Management.Models.EF;

namespace Profile_Management.Controllers
{
    public class MM_NationalityController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: MM_Nationality
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
           
            return View();
        }

       
        // GET: MM_Nationality/Create
        public ActionResult Create()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var model = new NationalityViewModel
            {
                NewNationality = new Nationality(),
                Nationalities = db.nationalities.ToList()
            };

            return View(model);
        }

        // POST: MM_Nationality/Create
        [HttpPost]
        public ActionResult Create(NationalityViewModel model)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToList();

                foreach (var error in errors)
                {
                    System.Diagnostics.Debug.WriteLine(error);
                }

                // Trả lại form nếu có lỗi
                model.Nationalities = db.nationalities.ToList();
                return View(model);
            }

            db.nationalities.Add(model.NewNationality);
            db.SaveChanges();

            return RedirectToAction("Create");
        }

        [HttpPost]
        public JsonResult UpdateNationality(int id, string nationName)
        {
            var nationality = db.nationalities.Find(id);
            if (nationality != null)
            {
                nationality.Nation_Name = nationName;
                db.SaveChanges();
                return Json(new { success = true, message = "保存完了!" });
            }
            return Json(new { success = false, message = "データはなし、エーラー！" });
        }

        [HttpPost]
        public JsonResult DeleteNationality(int id)
        {
            var nationality = db.nationalities.Find(id);
            if (nationality != null)
            {
                db.nationalities.Remove(nationality);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }




    }
}
