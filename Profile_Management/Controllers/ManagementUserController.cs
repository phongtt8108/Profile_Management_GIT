using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Profile_Management.Models;
using PagedList.Mvc;
using PagedList;

namespace Profile_Management.Controllers
{
    public class ManagementUserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ManagementUser
        public ActionResult Index(/*string dateOfBirth,*/string searchString, int page = 1)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            int pageSize = 10;

            var users = db.user_TBLs.AsQueryable();

            //if (!string.IsNullOrEmpty(dateOfBirth) && DateTime.TryParse(dateOfBirth, out DateTime dob))
            //{
            //    users = users.Where(u => u.DateOfBirth == dob);
            //}
            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(u =>
                    u.FullName.Contains(searchString) ||
                    u.Gender.Contains(searchString) ||
                    u.Job.Contains(searchString) ||
                    u.MaritalStatus.Contains(searchString));
                   
            }
            if (!users.Any())
            {
                ViewBag.NoResults = "該当のデータがありません"; 
            }
            users = users.OrderByDescending(u => u.UserID);
            ViewBag.CurrentFilter = searchString;
            return View(users.ToPagedList(page, pageSize));
        }
        public ActionResult Create_Profile()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var nationalities = db.nationalities.ToList();
            //{
            //    Value = n.Nation_ID.ToString(),
            //    Text = n.Nation_Name
            //}).ToList();
            //ViewBag.Nationalities = nationalities;
            ViewBag.Nationalities = new SelectList(nationalities, "Nation_ID", "Nation_Name");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Profile(User_TBL user)
        {
            if (!string.IsNullOrEmpty(user.ProfilePicture))
            {
                string tempPath = Server.MapPath(user.ProfilePicture);
                string finalPath = Server.MapPath("~/UploadedFiles/" + Path.GetFileName(user.ProfilePicture));

                if (System.IO.File.Exists(tempPath))
                {
                    // 算定保存
                    System.IO.File.Move(tempPath, finalPath);
                    user.ProfilePicture = "/UploadedFiles/" + Path.GetFileName(user.ProfilePicture);
                }
            }

            if (ModelState.IsValid)
            {
                
                db.user_TBLs.Add(user);
                db.SaveChanges();
                db.actionLogs.Add(new Models.EF.ActionLog
                {
                    ActionLogType = "Create",
                    ActionLogDescription = "User created",
                    ActionLogDate = DateTime.Now,
                    ActionLogUser = user.UserID,
                    ActionLogAccountLog = (string)Session["Email"],
                    ActionLogIP = Request.UserHostAddress,
                    ActionLogDevice = Request.Browser.Browser
                });
                db.SaveChanges();
                return RedirectToAction("Index", "ManagementUser");

            }
            return RedirectToAction("Create_Profile", user);
        }
        public ActionResult Detail(int id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var userdetail = db.user_TBLs.Find(id);
            var imagePath = userdetail.ProfilePicture;

            if (userdetail == null)
            {
                return RedirectToAction("Index", "ManagementUser");
            }
            ViewBag.ImagePath = imagePath;
            return View(userdetail);
            
        }
        public ActionResult Edit(int id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var user = db.user_TBLs.Find(id);
            var imagePath = user.ProfilePicture;

            if (user == null)
            {
                return RedirectToAction("Index", "ManagementUser");
            }
            var nationalities = db.nationalities.Select(n => new SelectListItem
            {
                Value = n.Nation_ID.ToString(),
                Text = n.Nation_Name
            }).ToList();
            ViewBag.Nationalities = nationalities;
            ViewBag.FormattedDate = user.DateOfBirth?.ToString("yyyy-MM-dd");

            ViewBag.ImagePath = imagePath;
            return View(user);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User_TBL user)
        {
            if (!string.IsNullOrEmpty(user.ProfilePicture))
            {
                string tempPath = Server.MapPath(user.ProfilePicture);
                string finalPath = Server.MapPath("~/UploadedFiles/" + Path.GetFileName(user.ProfilePicture));

                if (System.IO.File.Exists(tempPath))
                {
                    // 算定保存
                    System.IO.File.Move(tempPath, finalPath);
                    user.ProfilePicture = "/UploadedFiles/" + Path.GetFileName(user.ProfilePicture);
                }
            }

            if (ModelState.IsValid)
            {
                var existingUser = db.user_TBLs.Find(user.UserID);
                if (existingUser != null)
                {
                    existingUser.FullName = user.FullName;
                    existingUser.DateOfBirth = user.DateOfBirth;
                    existingUser.Gender = user.Gender;
                    existingUser.NationalID = user.NationalID;
                    existingUser.Nation_ID = user.Nation_ID;
                    existingUser.MaritalStatus = user.MaritalStatus;
                    existingUser.PhoneNumber = user.PhoneNumber;
                    existingUser.Address = user.Address;
                    existingUser.Job = user.Job;
                    existingUser.Company = user.Company;
                    existingUser.Position = user.Position;
                    existingUser.ProfilePicture = user.ProfilePicture;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Edit", new {id= user.UserID});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_User(int id)
        {
            var user = db.user_TBLs.Find(id);
            if(user != null)
            {
                db.user_TBLs.Remove(user);
                db.SaveChanges();
            }    

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult UploadTempImage(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string uploadPath = Server.MapPath("~/UploadedFiles/Temp");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                string fileExt = Path.GetExtension(file.FileName);
                string fileName = Guid.NewGuid().ToString() + fileExt;
                string filePath = Path.Combine(uploadPath, fileName);

                file.SaveAs(filePath);

                // 暫定保存のURLを返す
                return Json(new { success = true, filePath = "/UploadedFiles/Temp/" + fileName });
            }
            return Json(new { success = false });
        }

        public ActionResult Deatail_PageFunc(int page = 1)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            const int pageSize = 10;
            var users = db.user_TBLs.OrderByDescending(p => p.UserID).ToList();
            var pageUsers = users.ToPagedList(page, pageSize);
            return View("Index",pageUsers);
        }
    }
}