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
	public class when_binding_nested_nested_simple_properties : Specification
	{
		private NameValueCollection post;
		private OuterClass target;

		protected override void Given()
		{
			DependencyResolver.SetCurrentResolver(new StructureMapAdapter());

			this.post = new NameValueCollection
			       	{
			       		{":Middle:Inner:boolus", "true,FALSE"},
			       		{":Middle:Inner:stringus", "stringy"},
			       		{":Middle:Inner:dateus", "2001/01/01"},
			       		{":Middle:Inner:intus", "20"},
			       		{":Middle:Inner:doublus", "50.5"}
			       	};

			this.target = new OuterClass();
		}

		protected override void When()
		{
			Binder.Bind(target, new[] { post });
		}

		[Test]
		public void all_properties_should_be_correctly_bound()
		{
			target.Middle.Inner.boolus.ShouldEqual(true);
			target.Middle.Inner.stringus.ShouldEqual("stringy");
			target.Middle.Inner.dateus.ShouldEqual(new DateTime(2001, 1, 1));
			target.Middle.Inner.intus.ShouldEqual(20);
			target.Middle.Inner.doublus.ShouldEqual(50.5);
		}
	}
}