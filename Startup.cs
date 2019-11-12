using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(E_Hutech.Startup))]
namespace E_Hutech
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
