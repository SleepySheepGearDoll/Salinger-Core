using System;
using System.Collections.Generic;
using System.Linq;

namespace Salinger.Core.Applications.Data
{
    /// <summary>
    /// Search action log interface class.
    /// </summary>
    public interface ISearchActionLog
    {
        /// <summary>
        /// Search action log id.
        /// </summary>
        /// <value>id value.</value>
        string Id {get; set;}

        /// <summary>
        /// Salinger thread id.
        /// </summary>
        /// <value></value>
        string SalingerThreadId {get; set;}

        /// <summary>
        /// Email subject.
        /// </summary>
        /// <value></value>
        string Subject {get; set;}

        /// <summary>
        /// Mailing list.
        /// </summary>
        /// <value></value>
        IEnumerable<string> MailingList {get; set;}
        
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        string Creator {get; set;}

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        DateTime CreatedTime {get; set;}

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        string Updator {get; set;}

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        DateTime UpdatedTime {get; set;}
    }
}