﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Owin.Builder;

namespace Profile_Management
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var owinApp = new AppBuilder();
            Startup startup = new Startup();
            startup.Configuration(owinApp);
            
        }
        //protected void Session_Start(object sender, EventArgs e)
        //{
        //    Session["Role"] = null; // Khởi tạo Session để tránh null
        //}
    }
}
