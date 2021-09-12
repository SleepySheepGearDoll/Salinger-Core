using System;
using System.Linq;
using System.Threading.Tasks;
using Salinger.Core.Domains.Entities;

namespace Salinger.Core.Domains.Actions
{
    /// <summary>
    /// Search action interface class.
    /// </summary>
    public interface ISearchAction
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