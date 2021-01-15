using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.SQLBuilder
{
	public class IGroup<TKey, Telement>
	{
		public TKey Key;
		public Telement Value;

		public int Count() { return 0; }
	}
}
