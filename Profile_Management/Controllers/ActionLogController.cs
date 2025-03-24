using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Profile_Management.Models;

namespace Profile_Management.Controllers
{
    public class ActionLogController : Controller
    {
        // GET: ActionLog
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var actionlog = db.actionLogs.OrderByDescending(a => a.ActionLogID).ToList();
            return View(actionlog);
        }
    }
}