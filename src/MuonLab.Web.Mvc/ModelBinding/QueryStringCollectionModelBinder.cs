using System;
using System.Web.Mvc;

namespace MuonLab.Web.Mvc.ModelBinding
{
    public class QueryStringCollectionModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (controllerContext == null)
                throw new ArgumentNullException("controllerContext");

            return new QueryStringCollection(controllerContext.HttpContext.Request.QueryString);
        }
    }
}