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
using Salinger.Core.Applications.Actions;
using Salinger.Core.Domains.Actions;

namespace Salinger.Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int millisecondsDelay = 10000;
            SalingerSearcher seacher = (SalingerSearcher)new Object();
            ISearchAction searchAction = (ISearchAction)new Object();
            SalingerScheduler scheduler = new SalingerScheduler(2);
            List<Task> tasks = new List<Task>();
            TaskFactory factory = new TaskFactory(scheduler);
            CancellationTokenSource ctSource = new CancellationTokenSource();
            IPollingSearchAction polling = new PollingSearchAction(millisecondsDelay, seacher, searchAction);
            Task task = factory.StartNew(() => {
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
