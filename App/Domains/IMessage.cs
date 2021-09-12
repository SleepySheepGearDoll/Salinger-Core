namespace Salinger.Core.Domains
{
    /// <summary>
    /// Message interface class.
    /// </summary>
    internal interface IMessage
    {

        /// <summary>
        /// From.
        /// </summary>
        /// <value>
        /// Active Directory account name.
        /// </value>
        string From {get; set;}

        /// <summary>
        /// To.
        /// </summary>
        /// <value>
        /// Active Directory account name.
        /// </value>
        string To {get; set;}

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        string Body {get; set;}

    }
}

