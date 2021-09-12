using System;
using System.Linq;

namespace Salinger.Core.Domains.Entities
{
    /// <summary>
    /// Salinger Thread interface class.
    /// </summary>
    public interface ISalingerThread
    {
        /// <summary>
        /// Thread Id.
        /// </summary>
        /// <value></value>
        string Id {get; set;}

        /// <summary>
        /// TaskId.
        /// </summary>
        /// <value></value>
        ulong TaskId {get; set;}

        /// <summary>
        /// Messages
        /// </summary>
        /// <value></value>
        IQueryable<IMessage> Messages {get; set;}

        /// <summary>
        /// Created time.
        /// </summary>
        /// <value></value>
        DateTime CreatedTime {get; set;}

        /// <summary>
        /// Creator.
        /// </summary>
        /// <value>
        /// Active Directory account name.
        /// </value>
        string Creator {get; set;}

        /// <summary>
        /// Last updated time.
        /// </summary>
        /// <value></value>
        DateTime UpdatedTime {get; set;}

        /// <summary>
        /// Last updator.
        /// </summary>
        /// <value>
        /// Active Directory account name.
        /// </value>
        string Updator {get; set;}
    }
}
