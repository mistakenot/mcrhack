using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookByText.Startup))]
namespace BookByText
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
