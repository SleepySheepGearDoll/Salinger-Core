using Microsoft.Exchange.WebServices.Data;

namespace Salinger.Core.Infrastructures.Exchanges
{
    public interface IExchangeWebServiceContext
    {
        int GetMaxCountOfItems();
        
        ExchangeService GetExchangeService();
    }
}