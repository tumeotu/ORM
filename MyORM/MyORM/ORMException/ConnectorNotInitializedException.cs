using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.ORMException
{
    /// <summary>
    /// Generic exception thrown from within the orm connector class.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class ConnectorNotInitializedException : Exception
    {

        /// <summary>
        /// The default message.
        /// </summary>
        public const string DefaultMessage = "The connector has not been initialized.";


        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectorNotInitializedException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ConnectorNotInitializedException(string message) : base(message)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectorNotInitializedException"/> class.
        /// </summary>
        public ConnectorNotInitializedException() : base(DefaultMessage)
        {

        }
    }
}
