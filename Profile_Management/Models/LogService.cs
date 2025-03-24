using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Profile_Management.Models
{
	
  public class LogService
    {
        private readonly ApplicationDbContext _context;

        public LogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(string actionLogType, string actionLogDescription, int actionLogUser, string actionLogIP, string actionLogDevice)
        {
            _context.actionLogs.Add(new EF.ActionLog
            {
                ActionLogType = actionLogType,
                ActionLogDescription = actionLogDescription,
                ActionLogDate = DateTime.Now,
                ActionLogUser = actionLogUser,
                ActionLogAccountLog = actionLogUser.ToString(), 
                ActionLogIP = actionLogIP,
                ActionLogDevice = actionLogDevice
            });
            _context.SaveChanges();
        }


    }
}