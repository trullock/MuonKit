using System.Web.Mvc;

namespace MuonLab.Web.Mvc.ModelBinding
{
    public class HttpFileCollectionBaseModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return controllerContext.HttpContext.Request.Files;
        }
    }
}