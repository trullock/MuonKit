namespace MuonLab.Web.Mvc.ModelBinding.Interception
{
	public class DefaultInterceptor<T> : Interceptor<T>
	{
		protected override void Configure()
		{
			InterceptAll<string>()
				.With(s => s == null ? null : s.Trim());
		}
	}
}