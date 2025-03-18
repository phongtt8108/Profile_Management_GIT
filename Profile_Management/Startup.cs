using Microsoft.Owin;
using Owin;
using Profile_Management.Models;

[assembly: OwinStartupAttribute(typeof(Profile_Management.Startup))]
namespace Profile_Management
{
    
    public partial class Startup
    {
        
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
        }
       
    }
}
