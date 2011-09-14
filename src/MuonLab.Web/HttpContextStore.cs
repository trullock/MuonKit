using System.Web;

namespace MuonLab.Web
{
    public interface IHttpContextStore
    {
        T Get<T>(string key);
        void Set<T>(T obj, string key);
    }

    public class HttpContextStore : IHttpContextStore
    {
        public T Get<T>(string key)
        {
            if (HttpContext.Current != null)
            {
                var item = HttpContext.Current.Items[key];
                if (item != null && typeof (T).IsAssignableFrom(item.GetType()))
                    return (T) item;
            }

            return default(T);
        }

        public void Set<T>(T obj, string key)
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Items[key] = obj;
            }
        }
    }
}