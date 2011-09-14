using System;
using MuonLab.Testing;
using MuonLab.Web.Xhtml.Components;
using MuonLab.Web.Xhtml.Configuration;
using Rhino.Mocks;

namespace MuonLab.Web.Xhtml.Tests.Configuration.FormConfigurationSpecifications.Initialize
{
    public class when_initializing_IComponentT : Specification
    {
        private TestFormConfiguration configuration;
        private IComponent<object> component;

        protected override void Given()
        {
            configuration = new TestFormConfiguration();
            component = Stub<IComponent<object>>();
        }

        protected override void When()
        {
            configuration.Initialize(component);
        }

        [Then]
        public void the_component_should_have_the_icomponent_config_ran_on_it()
        {
            component.AssertWasCalled(c => c.WithId("test"));
        }

        [Then]
        public void the_component_should_have_the_icomponentT_config_ran_on_it()
        {
            component.AssertWasCalled(c => c.WithName("test"));
        }

        [Then]
        public void the_component_should_not_have_the_icomponentT2_config_ran_on_it()
        {
            component.AssertWasNotCalled(c => c.WithName("fish"));
        }

        private class TestFormConfiguration : FormConfiguration
        {
            public TestFormConfiguration()
            {
                Configure<IComponent>(c => c.WithId("test"));
                Configure<IComponent<object>>(c => c.WithName("test"));
                Configure<IComponent<DateTime>>(c => c.WithName("fish"));
            }
        }
    }
}