using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Salinger.Core.Domains;

namespace Salinger.Core.Applications
{
    internal class SalingerSearcher
    {
        async internal Task<IQueryable<ISalingerThread>> Search (ISearchAction action)
        {
            return await action.Run();
        }
    }
}