using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.ORMException
{
	public class DatabaseConfigMalformed: Exception
	{
        /// <summary>
        /// The default message.
        /// </summary>
        public const string DefaultMessage = "The database note found. Pleadse set DatabaseType or exactly connection string";


        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseConfigMalformed"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DatabaseConfigMalformed(string message) : base(message)
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseConfigMalformed"/> class.
        /// </summary>
        public DatabaseConfigMalformed() : base(DefaultMessage)
        {

        }
    }
}
