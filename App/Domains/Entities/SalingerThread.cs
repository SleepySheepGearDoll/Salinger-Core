using System;
using System.Linq;

namespace Salinger.Core.Domains.Entities
{
    /// <inherit />
    public class SalingerThread : ISalingerThread
    {
        /// <inherit />
        public string Id {get; set;}

        /// <inherit />
        public ulong TaskId {get; set;}

        /// <inherit />
        public IQueryable<IMessage> Messages {get; set;}

        /// <inherit />
        public DateTime CreatedTime {get; set;}

        /// <inherit />
        public string Creator {get; set;}

        /// <inherit />
        public DateTime UpdatedTime {get; set;}

        /// <inherit />
        public string Updator {get; set;}
    }
}
