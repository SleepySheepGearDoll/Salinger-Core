using System;
using System.Linq;
using Microsoft.Exchange.WebServices.Data;
using Salinger.Core.Domains.Entities;

namespace Salinger.Core.Infrastructures.Parsers
{
    public class SalingerMessageParser : ISalingerMessageParser
    {
        public IQueryable<ISalingerThread> Parse(FindItemsResults<Item> items)
        {
            return null;
        }
    }
}