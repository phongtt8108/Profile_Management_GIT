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
using Profile_Management.Common;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing.Printing;
using System.Web.UI;

namespace Profile_Management.Controllers
{
    public class ManagementUserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ManagementUser
        public ActionResult Index(int page = 1, string searchString = "")
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            const int pageSize = 10;
            var users = db.user_TBLs.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                if (int.TryParse(searchString, out int userID))
                {
                    users = db.user_TBLs.Where(u => u.UserID == userID);
                }
                else
                {
                    users = users.Where(u =>
                        u.UserID.ToString().Contains(searchString) ||
                        u.FullName.Contains(searchString) ||
                        u.Gender.Contains(searchString) ||
                        u.Job.Contains(searchString) ||
                        u.MaritalStatus.Contains(searchString) ||
                        u.Position.Contains(searchString));
                }
            }
            ViewBag.CountUser = users.Count();
            var pagedUsers = users
                .OrderByDescending(u => u.UserID)
                .ToPagedList(page, pageSize);
            ViewBag.CurrentFilter = searchString;
            return View(pagedUsers);
        }

        public ActionResult SearchFunction(string searchString = "", int page = 1)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            const int pageSize = 10;
            var users = db.user_TBLs.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                if (int.TryParse(searchString, out int userID))
                {
                    users = db.user_TBLs.Where(u => u.UserID == userID);
                }
                else
                {
                    users = users.Where(u =>
                        u.UserID.ToString().Contains(searchString) ||
                        u.FullName.Contains(searchString) ||
                        u.Gender.Contains(searchString) ||
                        u.Job.Contains(searchString) ||
                          u.MaritalStatus.Contains(searchString) ||
                        u.Position.Contains(searchString));
                }
            }
            ViewBag.CountUser = users.Count();
            var pagedUsers = users.OrderByDescending(u => u.UserID).ToPagedList(page, pageSize);
            if (!users.Any())
            {
                ViewBag.NoResults = "該当のデータがありません";
            }
            ViewBag.CurrentFilter = searchString;
            return View("Index", pagedUsers);
        }
        public ActionResult Create_Profile()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var nationalities = db.nationalities.ToList();

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
                ActionLogMM.ActionLogSV("Create", user.FullName, user.UserID);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = user.UserID });
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
            var user = db.user_TBLs
                 .Include(u => u.Nationality)
                 .FirstOrDefault(u => u.UserID == id);
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
            ViewBag.Nationalities = new SelectList(nationalities, "Value", "Text", user.Nationality);
            ViewBag.FormattedDate = user.DateOfBirth?.ToString("yyyy-MM-dd");
            ViewBag.ImagePath = imagePath;
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User_TBL user)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
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
            else
            {
                var existingUser1 = db.user_TBLs.Find(user.UserID);
                if (existingUser1 != null)
                {
                    user.ProfilePicture = existingUser1.ProfilePicture;
                }
            }
            //ModelState.Remove("Nation_ID");
            if (ModelState.IsValid)
            {
                var existingUser = db.user_TBLs.Find(user.UserID);
                var oldData = ActionLogMM.EditBeboreData(user.UserID);
                if (existingUser != null)
                {
                    bool isDataChanged = false;
                    var properties = typeof(User_TBL).GetProperties();
                    StringBuilder changesLog = new StringBuilder();

                    foreach (var property in properties)
                    {
                        var oldValue = property.GetValue(existingUser);
                        var newValue = property.GetValue(user);
                        if (property.Name == "Nationality")
                        {
                            if (existingUser.Nationality?.Nation_ID == user.Nation_ID)
                            {
                                continue;
                            }
                        }
                        System.Diagnostics.Debug.WriteLine($"Property: {property.Name} | Old: {oldValue} | New: {newValue}");
                        if (!Equals(oldValue, newValue) && !(newValue == null && oldValue == null))
                        {
                            isDataChanged = true;
                            if (oldValue == null)
                            {
                                changesLog.AppendLine($"{property.Name}: {newValue}");
                            }
                            else if (newValue == null)
                            {
                                changesLog.AppendLine($"{property.Name}: {oldValue} => null");
                            }
                            else
                            {
                                changesLog.AppendLine($"{property.Name}: {oldValue} => {newValue}");
                            }
                            property.SetValue(existingUser, newValue);
                        }
                    }
                    if (isDataChanged)
                    {
                        ActionLogMM.ActionLogSV("Edit", changesLog.ToString(), user.UserID);
                        //try
                        //{
                        db.SaveChanges();
                        //}
                        //catch (DbEntityValidationException ex)
                        //{
                        //    foreach (var validationErrors in ex.EntityValidationErrors)
                        //    {
                        //        foreach (var validationError in validationErrors.ValidationErrors)
                        //        {
                        //            System.Diagnostics.Debug.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                        //        }
                        //    }
                        //    throw;
                        //}

                    }
                }
            }
            return RedirectToAction("Edit", new { id = user.UserID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_User(int id)
        {
            var user = db.user_TBLs.Find(id);
            if (user != null)
            {
                ActionLogMM.ActionLogSV("Delete", user.FullName, user.UserID);
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
            var users = db.user_TBLs.OrderByDescending(p => p.UserID);
            ViewBag.CountUser = users.Count();
            var pageUsers = users.ToPagedList(page, pageSize);
            return View("Index", pageUsers);
        }

    }
}