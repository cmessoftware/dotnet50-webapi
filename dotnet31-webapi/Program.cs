using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace cmes_webapi
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
        
            Service.SaveSettings();
            var host = CreateHostBuilder(args).Build();
         
            using (var scope = host.Services.CreateScope())
            {
              
                //try
                //{
                //    //var context = services.GetRequiredService<DBContext>();
          
                //}
                //catch (Exception ex)
                //{
                //    var logger = services.GetRequiredService<ILogger<Program>>();
                //    logger.LogError(ex, "An error occurred creating the DB.");
                //}
            }

            //Service.logger = host.Services.GetRequiredService<ILogger<Program>>();
                 
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               })
              .ConfigureLogging(log => log.SetMinimumLevel(LogLevel.Information))
              .ConfigureLogging(builder =>
                builder.ClearProviders());
                //.AddSqlLogger(configuration =>
                //{
                //    configuration.LogLevels.Add(
                //        LogLevel.Warning, ConsoleColor.DarkMagenta);
                //    configuration.LogLevels.Add(
                //        LogLevel.Error, ConsoleColor.Red);
                //}));




        //public static IWebHost BuildWebHost(string[] args) => WebHost.CreateDefaultBuilder(args)
        //                        .UseStartup<Startup>()
        //                        .UseContentRoot(Directory.GetCurrentDirectory())
        //                        .UseUrls(GlobalSettings.APPLICATIONURL)
        //                        .UseIISIntegration()
        //                        .ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Information))
        //                        .Build();

    }
}
