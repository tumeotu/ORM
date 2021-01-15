using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.ORMException
{
	public class SqlStringException: Exception
	{
        /// <summary>
        /// The default message.
        /// </summary>
        public const string DefaultMessage = "The primary key is not null";

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlStringException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public SqlStringException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlStringException"/> class.
        /// </summary>
        public SqlStringException() : base(DefaultMessage)
        {

        }
    }
}
