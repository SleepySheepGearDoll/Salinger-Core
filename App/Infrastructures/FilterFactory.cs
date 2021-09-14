using Microsoft.Exchange.WebServices.Data;
using Salinger.Core.Domains.Actions;
using Salinger.Core.Infrastructures.Exchanges;
using Salinger.Core.Infrastructures.Filters;
using Salinger.Core.Infrastructures.Parsers;

namespace Salinger.Core.Infrastructures
{
    internal class FilterFactory
    {
        internal static ISearchAction NewSearchAction(IExchangeWebServiceContext ewsContext, MailSearchAction.SearchProperty property)
        {
            return new MailFilter(ewsContext, new SalingerMessageParser(), property);
        }
    }
}