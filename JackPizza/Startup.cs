using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JackPizza.Startup))]
namespace JackPizza
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
