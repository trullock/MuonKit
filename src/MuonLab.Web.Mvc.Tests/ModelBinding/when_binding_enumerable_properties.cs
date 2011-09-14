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
	public class when_binding_enumerable_properties : Specification
	{
		private NameValueCollection post;
		private InnerClass target;

		protected override void Given()
		{
			DependencyResolver.SetCurrentResolver(new StructureMapAdapter());

			this.post = new NameValueCollection
			       	{
			       		{":enumerable", "true,FALSE,trUE"},
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
			var bools = this.target.enumerable.ToArray();

			bools[0].ShouldEqual(true);
			bools[1].ShouldEqual(false);
			bools[2].ShouldEqual(true);
		}
	}
}