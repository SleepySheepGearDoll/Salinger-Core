using System;
using System.Collections.Generic;
using System.Linq;
using Salinger.Core.Applications.Data;
using Salinger.Core.Domains.Entities;
using Salinger.Core.Domains.Actions;

namespace Salinger.Core.Infrastructures
{
    /// <summary>
    /// Application Layer
    /// ------------------
    /// ^ ISalingerThread and IMessage
    /// This class
    /// 
    /// ------------------
    /// Infrastructure Layer
    /// </summary>
    public interface ISalingerDataAccessAdapter
    {

        IQueryable<ISalingerThread> GetSalingerThreads(ISearchAction searchAction);

        IEnumerable<ISearchActionLog> GetLastSearchActionLog(ISearchAction searchAction);

        ISearchAction NewSearchAction(MailSearchAction.SearchProperty property);
    }
}