using System;
using Microsoft.Exchange.WebServices.Data;

namespace Salinger.Core.Infrastructures.Exchanges
{
    public class ExchangeWebServiceContext : IExchangeWebServiceContext
    {
        private int maxCountOfItems;

        private ExchangeService service;

        public ExchangeWebServiceContext(
            string uri,
            int maxCountOfItems,
            string account,
            string password
        )
        {
            this.service = new ExchangeService(ExchangeVersion.Exchange2010);
            this.service.Url = new Uri(uri);
            this.service.Credentials = new WebCredentials(account, password);
            this.maxCountOfItems = maxCountOfItems;
        }

        public int GetMaxCountOfItems()
        {
            return this.maxCountOfItems;
        }

        public ExchangeService GetExchangeService()
        {
            return this.service;
        }
    }
}