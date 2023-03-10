using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace LifetimeDemonstration.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseDefaultServiceProvider(options =>
                {
                    options.ValidateScopes = true;
                })
                .UseStartup<Startup>();
    }
}
