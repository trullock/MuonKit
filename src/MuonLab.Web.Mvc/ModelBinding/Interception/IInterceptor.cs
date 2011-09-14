using System.Reflection;

namespace MuonLab.Web.Mvc.ModelBinding.Interception
{
	public interface IInterceptor<T>
	{
		object InterceptProperty(PropertyInfo property, object value);
	}
}