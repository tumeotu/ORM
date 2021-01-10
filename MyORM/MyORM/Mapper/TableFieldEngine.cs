using System;
using System.Collections.Generic;
using System.Data;

namespace MyORM.Mapper
{
    class TableFieldEngine : MappingEngine
    {
        public override void Execute<T>(DataRow record, T result, string propertyName, HashSet<string> fieldsName, string resultAlias)
        {
            FlexibleObject<T> targetObject = new FlexibleObject<T>(result);
            foreach (var field in fieldsName)
            {
                string columnName = resultAlias + "." + field;
                var targetValue = Convert.ChangeType(record[columnName], targetObject.getType(propertyName));
                targetObject.set(propertyName, targetValue);
            }
        }
    }
}