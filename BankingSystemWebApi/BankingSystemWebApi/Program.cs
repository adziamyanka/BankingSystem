using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BankingSystemWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            // Directory.SetCurrentDirectory(@"C:\BankingSystemWebApi\BankingSystemWebApi");
                //  var builder = WebApplication.CreateBuilder(new WebApplicationOptions() { EnvironmentName = "Development", ApplicationName = "WebApi" });
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }

    }
}
