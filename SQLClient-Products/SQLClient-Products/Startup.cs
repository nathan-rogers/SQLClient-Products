using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SQLClient_Products.Startup))]
namespace SQLClient_Products
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
