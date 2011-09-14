using System;
using System.Collections.Specialized;

using MuonLab.Commons.DI;
using MuonLab.Commons.StructureMap;
using MuonLab.Testing;
using MuonLab.Web.Mvc.ModelBinding;
using NUnit.Framework;
using StructureMap;

namespace MuonLab.Web.Mvc.Tests.ModelBinding
{
	public class when_binding_nested_simple_properties : Specification
	{
		private NameValueCollection post;
		private MiddleClass target;

		protected override void Given()
		{
			DependencyResolver.SetCurrentResolver(new StructureMapAdapter());

			this.post = new NameValueCollection
			       	{
			       		{":Inner:boolus", "true,FALSE"},
			       		{":Inner:stringus", "stringy"},
			       		{":Inner:dateus", "2001/01/01"},
			       		{":Inner:intus", "20"},
			       		{":Inner:doublus", "50.5"}
			       	};

			this.target = new MiddleClass();
		}

		protected override void When()
		{
			Binder.Bind(target, new[] { post });
		}

		[Test]
		public void all_properties_should_be_correctly_bound()
		{
			target.Inner.boolus.ShouldEqual(true);
			target.Inner.stringus.ShouldEqual("stringy");
			target.Inner.dateus.ShouldEqual(new DateTime(2001, 1, 1));
			target.Inner.intus.ShouldEqual(20);
			target.Inner.doublus.ShouldEqual(50.5);
		}
	}
}