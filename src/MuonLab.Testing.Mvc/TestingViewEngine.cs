using System;
using System.IO;
using System.Web.Mvc;
using Rhino.Mocks;

namespace MuonLab.Testing.Mvc
{
    public class TestingViewEngine : WebFormViewEngine
    {
        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Here be dragons!
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="viewName"></param>
        /// <param name="masterName"></param>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            string webProjectPath;
			
            // hack of death!!!!
            if(Environment.CurrentDirectory.ToUpper().EndsWith("DEBUG"))
            {
                var assembly = controllerContext.Controller.GetType().Assembly;

                webProjectPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, Path.Combine("../../../", assembly.GetName().Name)));
            }
            else
                webProjectPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../"));

            for (var i = 0; i < this.ViewLocationFormats.Length; i++ )
            {
                string location = this.ViewLocationFormats[i];

                var virtualViewPath = string.Format(location, viewName, controllerContext.RouteData.Values["controller"]);

                // crudely trim leading ~/
                virtualViewPath = virtualViewPath.Substring(2);

                var fullPath = Path.Combine(webProjectPath, virtualViewPath);

                this.ViewLocationFormats[i] = fullPath;

                if (File.Exists(fullPath))
                    return new ViewEngineResult(MockRepository.GenerateStub<IView>(), this);
            }

            return new ViewEngineResult(this.ViewLocationFormats);
        }

        public override void ReleaseView(ControllerContext controllerContext, IView view)
        {
            throw new NotImplementedException();
        }
    }
}