using Salinger.Core.Domains.Actions;

namespace Salinger.Core.Applications.Actions
{
    public abstract class LogSearchAction : ISearchAction
    {
        public abstract Task<IEnumerable<ISalingerThread>> Run();
        
        public class SearchPropety()
        {

        }
    }
}