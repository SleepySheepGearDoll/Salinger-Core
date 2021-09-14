using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Salinger.Core.Domains.Actions;
using Salinger.Core.Domains.Entities;
using Salinger.Core.Infrastructures;

namespace Salinger.Core.Domains
{
    public class SearchActionManager : ISearchActionManager
    {
        private readonly ISalingerDataAccessAdapter adapter;

        private readonly string mailAddress;

        public SearchActionManager(ISalingerDataAccessAdapter adapter, string mailAddress)
        {
            this.adapter = adapter;
            this.mailAddress = mailAddress;
        }

        public ISearchAction GetPollingSearchAction()
        {
            var property = new MailSearchAction.SearchProperty()
            {
                Sender = this.mailAddress,
            };
            var searchAction = this.adapter.NewSearchAction(property);
            return searchAction;        
        }
    }
}