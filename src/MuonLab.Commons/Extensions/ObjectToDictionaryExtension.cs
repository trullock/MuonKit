using System.Collections.Generic;
using System.ComponentModel;

namespace MuonLab.Commons.Extensions
{
	public static class ObjectToDictionaryExtension
	{
		/// <summary>
		/// Creates a dictionary of property/value pairs from the properties of the object
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static IDictionary<string, object> ToDictionary(this object self)
		{
			var properties = new Dictionary<string, object>();

			foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(self))
			{
				object obj2 = descriptor.GetValue(self);
				properties.Add(descriptor.Name, obj2);
			}

			return properties;
		}
	}
}