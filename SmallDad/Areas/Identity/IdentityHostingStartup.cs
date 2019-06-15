using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(SmallDad.Areas.Identity.IdentityHostingStartup))]
namespace SmallDad.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}