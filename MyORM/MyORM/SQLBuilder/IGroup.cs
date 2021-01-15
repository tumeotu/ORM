using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.SQLBuilder
{
	public class IGroup<T>
	{
		public T Value;
		public double Count() { return 0; }
		public double AVG(object clause) { return 0; }
		public double MIN(object clause) { return 0; }
		public double MAX(object clause) { return 0; }
		public double SUM(object clause) { return 0; }
	}
}
