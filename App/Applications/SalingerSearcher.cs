using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Salinger.Core.Domains.Actions;
using Salinger.Core.Domains.Entities;

namespace Salinger.Core.Applications
{
    public class SalingerSearcher
    {
        async public Task<IQueryable<ISalingerThread>> Search (ISearchAction action)
        {
            return await action.Run();
        }
    }
}