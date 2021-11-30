using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace WebAPI
{
    public class NamespaceConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var ControllerNamespace = controller.ControllerType.Namespace;
            if (ControllerNamespace is null)
            {
                controller.ApiExplorer.GroupName = "v1";
            }
            else
            {
                var ApiVersion = ControllerNamespace.Split(".").Last().ToLower(Thread.CurrentThread.CurrentCulture);
                if (!ApiVersion.StartsWith("v", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    ApiVersion = "v2";
                }

                controller.ApiExplorer.GroupName = ApiVersion;
            }
        }
    }
}
