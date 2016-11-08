using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookBank.Startup))]
namespace BookBank
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
