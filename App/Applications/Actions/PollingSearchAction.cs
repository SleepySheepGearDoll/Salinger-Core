using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Salinger.Core.Domains;
using Salinger.Core.Domains.Actions;
using Salinger.Core.Domains.Entities;

namespace Salinger.Core.Applications.Actions
{
    /// <summary>
    /// 
    /// </summary>
    public class PollingSearchAction : IPollingSearchAction
    {
        /// <summary>
        /// 
        /// </summary>
        private int millisecondsDelay;

        /// <summary>
        /// 
        /// </summary>
        private readonly SalingerSearcher searcher;

        /// <summary>
        /// 
        /// </summary>
        private readonly ISearchActionManager manager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="millisecondsDelay"></param>
        public PollingSearchAction(
            int millisecondsDelay,
            SalingerSearcher searcher,
            ISearchActionManager manager
        )
        {
            if (millisecondsDelay < 10000)
            {
                throw new ArgumentException("millisecondsDelay");
            }
            this.millisecondsDelay = millisecondsDelay;
            if (searcher == null)
            {
                throw new ArgumentException("searcher");
            }
            this.searcher = searcher;
            if (manager == null)
            {
                throw new ArgumentException("manager");
            }
            this.manager = manager;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <returns></returns>
        public async void Run()
        {
            while (true)
            {
                Console.WriteLine($"[ {DateTime.Now} ] Polling Task Run");
                ISearchAction action = this.manager.GetPollingSearchAction();
                IEnumerable<ISalingerThread> threads = await this.searcher.Search(action);
                await Task.Delay(this.millisecondsDelay);
            }
        }
    }
}