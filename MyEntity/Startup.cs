using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyEntity.Startup))]
namespace MyEntity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
