using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.Database.DBHandler
{
    class MySQLHandlerCheck : AbstractHandlerCheckDB
    {
        public override object Handle(object request)
        {
            if (request.ToString() == "mysql")
            {
                return $"mysql: I'll eat the {request.ToString()}.\n";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }
}
