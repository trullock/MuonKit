using System;
using System.Collections.Specialized;
using MuonLab.Commons.DI;
using MuonLab.Commons.StructureMap;
using MuonLab.Testing;
using MuonLab.Validation;
using MuonLab.Web.Mvc.ModelBinding;

namespace MuonLab.Web.Mvc.Tests.ModelBinding
{
	public class when_binding_a_enum_flags_property : Specification
	{
		private NameValueCollection collection;
		private TestClass resource;
		private ValidationReport report;

		public class TestClass
		{
			public Modes Modes { get; set; }
		}

		[Flags]
		public enum Modes
		{
			None = 0,
			One = 1,
			Two = 2,
			Four = 4
		}

		protected override void Given()
		{
			DependencyResolver.SetCurrentResolver(new StructureMapAdapter());

			this.collection = new NameValueCollection{{":Modes", "1,2,4"}};
			this.resource = new TestClass();
		}

		protected override void When()
		{
			this.report = Binder.Bind(this.resource, new[] { this.collection });
		}

		[Then]
		public void the_modes_should_be_bound_properly()
		{
			resource.Modes.ShouldEqual(Modes.One | Modes.Two | Modes.Four);
		}

		[Then]
		public void report_should_be_valid()
		{
			report.IsValid.ShouldBeTrue();
		}
	}
}