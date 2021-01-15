using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.Database.DBHandler
{
    abstract class AbstractHandlerCheckDB : IHandlerCheckDB
    {
		#region Properties
		/// <summary>
		/// next handler in chain
		/// </summary>
		private IHandlerCheckDB _nextHandler;
        #endregion 

        #region Method
        /// <summary>
        /// set who can handle request next
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public IHandlerCheckDB SetNext(IHandlerCheckDB handler)
        {
            this._nextHandler = handler;
            return handler;
        }

        /// <summary>
        /// handle the request
        /// </summary>
        /// <param name="config"></param>
        /// <returns>IDatabase if no one can handle then return null</returns>
        public virtual IDatabase Handle(Dictionary<string, string> config)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.Handle(config);
            }
            else
            {
                return null;
            }
        }
		#endregion
	}
}
