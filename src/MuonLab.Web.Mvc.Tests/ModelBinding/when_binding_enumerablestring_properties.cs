using System;
using System.Collections.Specialized;
using System.Linq;

using MuonLab.Commons.DI;
using MuonLab.Commons.StructureMap;
using MuonLab.Testing;
using MuonLab.Web.Mvc.ModelBinding;
using NUnit.Framework;
using StructureMap;

namespace MuonLab.Web.Mvc.Tests.ModelBinding
{
	public class when_binding_enumerablestring_properties : Specification
	{
		private NameValueCollection post;
		private InnerClass target;

		protected override void Given()
		{
			DependencyResolver.SetCurrentResolver(new StructureMapAdapter());

			this.post = new NameValueCollection
			       	{
			       		{":strings", "one\ntwo\nthree"},
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
			var strings = this.target.strings.ToArray();

			strings[0].ShouldEqual("one");
			strings[1].ShouldEqual("two");
			strings[2].ShouldEqual("three");
		}
	}
}