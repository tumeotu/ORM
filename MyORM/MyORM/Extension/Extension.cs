
using MyORM.SQLBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM
{
	public static class ExtensionTemp
	{
		public static SqlBuilder<T> CreateConnection<T>(this MyORM.Extension.IConfig config) where T : class, new()
		{
			return new MyORM.SQLBuilder.SqlString<T>();
		}
	}
}
