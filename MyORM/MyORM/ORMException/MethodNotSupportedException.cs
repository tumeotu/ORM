using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.ORMException
{
	public class MethodNotSupportedException: Exception
	{
        /// <summary>
        /// The default message.
        /// </summary>
        public const string DefaultMessage = "The mothod is not support";


        /// <summary>
        /// Initializes a new instance of the <see cref="MethodNotSupportedException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MethodNotSupportedException(string message) : base(message)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodNotSupportedException"/> class.
        /// </summary>
        public MethodNotSupportedException() : base(DefaultMessage)
        {

        }
    }
}
