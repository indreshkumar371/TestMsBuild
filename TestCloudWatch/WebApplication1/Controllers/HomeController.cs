using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AWS.Logger;
using AWS.Logger.SeriLog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;

        public  HomeController()
            {

            }

        public IActionResult Index()
        {
          
            //Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console()
            //  .WriteTo.File("logs\\myapp.txt", rollingInterval: RollingInterval.Day)
            //  .CreateLogger();


            AWSLoggerConfig configuration = new AWSLoggerConfig("/singleton/dev/services");
            configuration.Region = "us-east-1";// "us-west-2";
            //configuration.LogGroup = "/singleton/dev/services";
            configuration.LogStreamNameSuffix= Guid.NewGuid().ToString();
            configuration.Profile=


            configuration.LibraryLogFileName= "aws-logger-errors.txt";
            var logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.AWSSeriLog(configuration).CreateLogger();



            int a = 10, b = 0;
            int c=0;
            try
            {
                c = a / b;
            }
            catch (Exception ex)
            {
                logger.Error(ex,"Hi Test");
                logger.Fatal(ex, "Testing exception");
                logger.Write(Serilog.Events.LogEventLevel.Debug, "Testing exception");
                
            }
         
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
