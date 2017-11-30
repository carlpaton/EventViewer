using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LogViewWebApplication.Startup))]
namespace LogViewWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
