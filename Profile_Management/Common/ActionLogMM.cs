﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Profile_Management.Controllers;
using Profile_Management.Models;
using Profile_Management.Models.EF;
using UAParser;

namespace Profile_Management.Common
{

    public class ActionLogMM : ApiController
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        public static void ActionLogSV(string actionLogType, string actionLogDescription,int actionLogUser)
        {
            var actionLog = new ActionLog
            {
                ActionLogType = actionLogType,
                ActionLogDescription = actionLogDescription,
                ActionLogDate = DateTime.Now,
                ActionLogUser = actionLogUser,
                ActionLogAccountLog = (string)HttpContext.Current.Session["Email"],
                ActionLogIP = HttpContext.Current.Request.UserHostAddress,
                ActionLogDevice = GetDeviceInfo(HttpContext.Current.Request.UserAgent)
            };
            db.actionLogs.Add(actionLog);
            db.SaveChanges();
        }
        private static string GetDeviceInfo(string userAgent)
        {
            // Use UAParser to get device info
            var uaParser = Parser.GetDefault();
            var clientInfo = uaParser.Parse(userAgent);

            string device = "Unknown Device";

            if (clientInfo.Device.Family != "Other")
            {
                device = clientInfo.Device.Family;
            }
            else if (clientInfo.OS.Family != "Other")
            {
                device = clientInfo.OS.Family; 
            }

            return device;
        }
        public static string EditBeboreData(int userid)
        {
            var user = db.user_TBLs.Find(userid);
            var oldData = new
            {
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
                Address = user.Address,
                Company = user.Company,
                Job = user.Job,
                NationalID = user.NationalID,
                Nation_ID = user.Nation_ID,
                MaritalStatus = user.MaritalStatus
            };

            return $"Data before edit: FullName={oldData.FullName}, PhoneNumber={oldData.PhoneNumber}, " +
                $"DateOfBirth={oldData.DateOfBirth}, Address={oldData.Address}, " +
                $"Company={oldData.Company}, Job={oldData.Job}, NationalID={oldData.NationalID}, " +
                $"Nation_ID={oldData.Nation_ID}, MaritalStatus={oldData.MaritalStatus}";

        }
    }
}