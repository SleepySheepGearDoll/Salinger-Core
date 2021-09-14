namespace Salinger.Core.Domains.Entities
{
    public class Message : IMessage
    {
        /// <inherit />
        public string From {get; set;}

        /// <inherit />
        public string To {get; set;}

        /// <inherit />
        public string Body {get; set;}
    }
}