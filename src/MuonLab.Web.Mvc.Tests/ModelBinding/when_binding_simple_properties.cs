using System;
using System.Collections.Specialized;

using MuonLab.Commons.DI;
using MuonLab.Commons.StructureMap;
using MuonLab.Testing;
using MuonLab.Web.Mvc.ModelBinding;
using NUnit.Framework;

namespace MuonLab.Web.Mvc.Tests.ModelBinding
{
	public class when_binding_simple_properties : Specification
	{
		private NameValueCollection post;
		private InnerClass target;

		protected override void Given()
		{
			DependencyResolver.SetCurrentResolver(new StructureMapAdapter());

			this.post = new NameValueCollection
			       	{
			       		{":boolus", "true,FALSE"},
			       		{":stringus", "stringy"},
			       		{":dateus", "2001/01/01"},
			       		{":intus", "20"},
			       		{":doublus", "50.5"}
			       	};

			this.target = new InnerClass();
		}

		protected override void When()
		{
			Binder.Bind(target, new[] { post });
		}

		[Test]
		public void all_properties_should_be_correctly_bound()
		{
			target.boolus.ShouldEqual(true);
			target.stringus.ShouldEqual("stringy");
			target.dateus.ShouldEqual(new DateTime(2001, 1, 1));
			target.intus.ShouldEqual(20);
			target.doublus.ShouldEqual(50.5);
		}
	}
}