using System;
using MuonLab.Testing;
using MuonLab.Web.Xhtml.Components;
using MuonLab.Web.Xhtml.Configuration;
using Rhino.Mocks;

namespace MuonLab.Web.Xhtml.Tests.Configuration.FormConfigurationSpecifications.Initialize
{
    public class when_initializing_IComponent : Specification
    {
        private TestFormConfiguration configuration;
        private IComponent component;

        protected override void Given()
        {
            configuration = new TestFormConfiguration();
            component = Stub<IComponent>();
        }

        protected override void When()
        {
            configuration.Initialize(component);
        }

        [Then]
        public void the_component_should_have_the_config_ran_on_it()
        {
            component.AssertWasCalled(c => c.WithId("test"));
        }

        private class TestFormConfiguration : FormConfiguration
        {
            public TestFormConfiguration()
            {
                Configure<IComponent>(c => c.WithId("test"));
            }
        }
    }
}