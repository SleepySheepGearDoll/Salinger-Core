using System;
using System.Collections.Generic;
using System.Linq;

namespace Salinger.Core.Applications.Data
{
    /// <inherit />
    public class SearchActionLog : ISearchActionLog
    {
        /// <inherit />
        public string Id {get; set;}

        /// <inherit />
        public string SalingerThreadId {get; set;}

        /// <inherit />
        public string Subject {get; set;}

        /// <inherit />
        public IEnumerable<string> MailingList {get; set;}

        /// <inherit />
        public string Creator {get; set;}

        /// <inherit />
        public DateTime CreatedTime {get; set;}

        /// <inherit />
        public string Updator {get; set;}

        /// <inherit />
        public DateTime UpdatedTime {get; set;}
    }
}