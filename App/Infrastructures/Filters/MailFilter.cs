using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Exchange.WebServices.Data;
using Salinger.Core.Domains.Actions;
using Salinger.Core.Domains.Entities;
using Salinger.Core.Infrastructures.Exchanges;
using Salinger.Core.Infrastructures.Parsers;

namespace Salinger.Core.Infrastructures.Filters
{
    public class MailFilter : MailSearchAction
    {
        private readonly ExchangeService service;

        private SearchFilter filter;

        private int maxCountOfItems;
        
        private PropertySet propertySet = new PropertySet();

        private ISalingerMessageParser parser;

        public MailFilter(
            IExchangeWebServiceContext ewsContext, 
            ISalingerMessageParser parser, 
            MailSearchAction.SearchProperty property)
        {
            base.property = property;
            this.service = ewsContext.GetExchangeService();
            this.parser = parser;
            this.filter = null;
            this.maxCountOfItems = ewsContext.GetMaxCountOfItems();
        }

        public override async Task<IEnumerable<ISalingerThread>> Run()
        {
            return await System.Threading.Tasks.Task.Run(() => {
                /*
                this.Bind();
                ItemView view = new ItemView(this.maxCountOfItems);
                view.PropertySet = this.propertySet;
                FindItemsResults<Item> items = this.service.FindItems(WellKnownFolderName.Inbox, this.filter, view);
                IQueryable<ISalingerThread> threads = this.parser.Parse(items);
                */
                Console.WriteLine("MailFilter.Run()");
                return new List<ISalingerThread>();
            });
        }

        private void Bind()
        {
            List<SearchFilter> filters = new List<SearchFilter>(0);

            if (this.property.ToRecipients.Length > 0)
            {
                this.propertySet.Add(EmailMessageSchema.ToRecipients);
                foreach (string recipient in this.property.ToRecipients)
                {
                    var mailAddress = new EmailAddress(recipient);
                    var f = new SearchFilter.IsEqualTo(EmailMessageSchema.ToRecipients, mailAddress);
                    filters.Add(f);
                }
            }

            if (this.property.CcRecipients.Length > 0)
            {
                this.propertySet.Add(EmailMessageSchema.CcRecipients);
                foreach (string recipient in this.property.CcRecipients)
                {
                    var mailAddress = new EmailAddress(recipient);
                    var f = new SearchFilter.IsEqualTo(EmailMessageSchema.CcRecipients, mailAddress);
                    filters.Add(f);
                }
            }

            if (string.IsNullOrEmpty(this.property.Sender) == false)
            {
                this.propertySet.Add(EmailMessageSchema.Sender);
                var mailAddress = new EmailAddress(this.property.Sender);
                var f = new SearchFilter.IsEqualTo(EmailMessageSchema.Sender, mailAddress);
                filters.Add(f);
            }

            if (this.property.DateTimeReceived == DateTime.MinValue)
            {
                this.propertySet.Add(ItemSchema.DateTimeReceived);
                var f = new SearchFilter.IsGreaterThanOrEqualTo(EmailMessageSchema.DateTimeReceived, this.property.DateTimeReceived);
                filters.Add(f);
            }

            if (string.IsNullOrEmpty(this.property.Subject) == false)
            {
                this.propertySet.Add(ItemSchema.Subject);
                var f = new SearchFilter.ContainsSubstring(EmailMessageSchema.Subject, this.property.Subject);
                filters.Add(f);
            }

            this.filter = new SearchFilter.SearchFilterCollection(
                LogicalOperator.And,
                filters
            );
        }
    }
}