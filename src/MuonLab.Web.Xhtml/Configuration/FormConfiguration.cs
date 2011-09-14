using System;
using System.Collections.Generic;
using MuonLab.Web.Xhtml.Components;

namespace MuonLab.Web.Xhtml.Configuration
{
	public class FormConfiguration : IFormConfiguration
	{
		private readonly IDictionary<Type, IList<Delegate>> configurations;

		protected FormConfiguration()
		{
			this.configurations = new Dictionary<Type, IList<Delegate>>();
		}

		protected void Configure<TComponent>(Action<TComponent> configuration) where TComponent : IComponent
		{
			var type = typeof(TComponent);

			if(!this.configurations.ContainsKey(type))
				this.configurations.Add(type, new List<Delegate>());

			this.configurations[type].Add(configuration);
		}

	    public void Initialize(IComponent component)
	    {
            var configs = getMatchingConfigurations(component.GetType());

            foreach (var config in configs)
            {
                config.DynamicInvoke(component);
            }
	    }

	    private IEnumerable<Delegate> getMatchingConfigurations(Type component)
		{
			var matchedConfigs = new List<Delegate>();

			foreach(var configType in this.configurations.Keys)
			{
				if(configurationMatches(configType, component))
					matchedConfigs.AddRange(this.configurations[configType]);
			}

			return matchedConfigs;
		}

		private static bool configurationMatches(Type configType, Type ComponentType)
		{
			return configType.IsAssignableFrom(ComponentType);
		}
	}
}