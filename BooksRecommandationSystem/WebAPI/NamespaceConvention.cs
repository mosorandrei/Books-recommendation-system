using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace WebAPI
{
    public class NamespaceConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel Controller)
        {
            var ControllerNamespace = Controller.ControllerType.Namespace;
            if(ControllerNamespace is null)
            {
                Controller.ApiExplorer.GroupName = "v1";
            } else
            {
                var ApiVersion = ControllerNamespace.Split(".").Last().ToLower();
                if (!ApiVersion.StartsWith("v"))
                {
                    ApiVersion = "v2";
                }

                Controller.ApiExplorer.GroupName = ApiVersion;
            }
        }
    }
}

