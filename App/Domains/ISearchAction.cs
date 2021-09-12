using System;
using System.Linq;
using System.Threading.Tasks;

namespace Salinger.Core.Domains
{
    /// <summary>
    /// Search action interface class.
    /// </summary>
    internal interface ISearchAction
    {
        /// <summary>
        /// Start a search action. this method is async method.
        /// </summary>
        /// <returns>
        /// ISalingerThread data.
        /// </returns>
        Task<IQueryable<ISalingerThread>> Run();
    }
}