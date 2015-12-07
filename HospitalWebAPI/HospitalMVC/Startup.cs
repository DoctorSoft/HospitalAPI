using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HospitalMVC.Startup))]
namespace HospitalMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
