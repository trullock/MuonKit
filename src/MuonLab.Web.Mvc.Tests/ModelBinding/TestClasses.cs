using System;
using System.Collections.Generic;

namespace MuonLab.Web.Mvc.Tests.ModelBinding
{
	public class MiddleClass
	{
		public InnerClass Inner { get; set; }

		public MiddleClass()
		{
			this.Inner = new InnerClass();
		}
	}

	public class OuterClass
	{
		public MiddleClass Middle { get; set; }

		public OuterClass()
		{
			this.Middle = new MiddleClass();
		}
	}

	public class InnerClass
	{
		public bool boolus { get; set; }
		public string stringus { get; set; }
		public DateTime dateus { get; set; }
		public int intus { get; set; }
		public double doublus { get; set; }
		public IEnumerable<bool> enumerable { get; set; }
		public IEnumerable<string> strings { get; set; }
	}
}