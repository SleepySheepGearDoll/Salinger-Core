using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Salinger.Core.Applications;

namespace Salinger.Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SalingerScheduler scheduler = new SalingerScheduler(2);
            List<Task> tasks = new List<Task>();
            TaskFactory factory = new TaskFactory(scheduler);
            CancellationTokenSource ctSource = new CancellationTokenSource();
            Task task = factory.StartNew(() => {
                IPollingSearchAction polling = new PollingSearchAction();
                polling.Run();
            });
            CreateHostBuilder(args).Build().Run();
            ctSource.Dispose();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
