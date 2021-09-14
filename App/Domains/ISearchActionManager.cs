using Salinger.Core.Domains.Actions;

namespace Salinger.Core.Domains
{
    public interface ISearchActionManager
    {
        ISearchAction GetPollingSearchAction();
    }
}