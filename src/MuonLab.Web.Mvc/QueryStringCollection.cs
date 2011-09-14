using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;

namespace MuonLab.Web.Mvc
{
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class QueryStringCollection : NameValueCollection
	{
		public QueryStringCollection()
		{
		}

		public QueryStringCollection(NameValueCollection collection)
		{
			if (collection == null)
				throw new ArgumentNullException("collection");

			Add(collection);
		}

		public virtual ValueProviderResult GetValue(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentException("Value cannot be null or empty", "name");

			var values = this.GetValues(name);
			if (values == null)
				return null;

			return new ValueProviderResult(values, base[name], CultureInfo.CurrentCulture);
		}

		public IDictionary<string, ValueProviderResult> ToValueProvider()
		{
			var currentCulture = CultureInfo.CurrentCulture;
			var dictionary = new Dictionary<string, ValueProviderResult>(StringComparer.OrdinalIgnoreCase);
			
			foreach (var str in this.AllKeys)
			{
				var values = this.GetValues(str);
				var attemptedValue = base[str];
				var result = new ValueProviderResult(values, attemptedValue, currentCulture);
				
                dictionary[str] = result;
			}

			return dictionary;
		}
	}
}