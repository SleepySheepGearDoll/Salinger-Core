using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Salinger.Core.Applications
{
    public class PollingSearchAction : IPollingSearchAction
    {
        public PollingSearchAction()
        {

        }

        public async void Run()
        {
            while (true)
            {
                Console.WriteLine("Polling Search Action!!");
                await Task.Delay(10000);
            }
        }
    }
}