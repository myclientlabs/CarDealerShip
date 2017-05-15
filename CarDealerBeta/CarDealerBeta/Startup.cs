using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarDealerBeta.Startup))]
namespace CarDealerBeta
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
