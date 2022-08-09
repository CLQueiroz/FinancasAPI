using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace FinanceApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseIISIntegration()
                    ;
                    var host = new WebHostBuilder()
                      .UseKestrel()
                      .UseContentRoot(Directory.GetCurrentDirectory())
                      .UseIISIntegration()
                      .UseStartup<Startup>()
                      .Build();
                });
    }
}
