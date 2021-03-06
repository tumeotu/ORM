﻿using System;
using System.Collections.Generic;
using System.Data;

namespace MyORM.Mapper
{
    class TableFieldEngine : MappingEngine
    {
        public override void Execute<T>(DataRow record, T result, string propertyName, string columnName, string resultAlias)
        {
            FlexibleObject<T> targetObject = new FlexibleObject<T>(result);
            string queryColumnName = resultAlias + "." + columnName;
            var targetValue = record.IsNull(queryColumnName) ? null : record[queryColumnName];
            targetObject.set(propertyName, targetValue);
        }
    }
}