using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Salinger.Core.Domains.Entities;

namespace Salinger.Core.Domains.Actions
{
    public abstract class MailSearchAction : ISearchAction
    {
        public SearchProperty property {get; protected set;}

        public abstract Task<IEnumerable<ISalingerThread>> Run();
        
        public class SearchProperty
        {
            public string[] ToRecipients {get; set;} = new string[0];

            public string[] CcRecipients {get; set;} = new string[0];

            public string Sender {get; set;} = string.Empty;

            public DateTime DateTimeReceived {get; set;} = DateTime.MinValue;

            public string Subject {get; set;}
            
            public SearchProperty()
            {

            }
        }
    }
}