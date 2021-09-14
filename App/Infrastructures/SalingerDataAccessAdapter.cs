using System;
using System.Collections.Generic;
using System.Linq;
using Salinger.Core.Applications.Data;
using Salinger.Core.Domains.Entities;
using Salinger.Core.Domains.Actions;
using Salinger.Core.Infrastructures.Parsers;
using Salinger.Core.Infrastructures.Exchanges;

namespace Salinger.Core.Infrastructures
{
    public class SalingerDataAccessAdapter : ISalingerDataAccessAdapter
    {
        private readonly IExchangeWebServiceContext ewsContext;

        private readonly string dbConnectionString;

        public SalingerDataAccessAdapter(
            IExchangeWebServiceContext ewsContext,
            string dbConnectionString
        )
        {
            this.ewsContext = ewsContext;
            this.dbConnectionString = dbConnectionString;
        }

        public IQueryable<ISalingerThread> GetSalingerThreads(ISearchAction searchAction)
        {
            return null;
        }

        public IEnumerable<ISearchActionLog> GetLastSearchActionLog(ISearchAction searchAction)
        {
            return null;
        }

        public ISearchAction NewSearchAction(MailSearchAction.SearchProperty property)
        {
            return FilterFactory.NewSearchAction(this.ewsContext, property);
        }
    }
}