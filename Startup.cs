using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bowling.Startup))]
namespace Bowling
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
