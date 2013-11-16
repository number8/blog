using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VersionedViews.Controllers;

namespace VersionedViews.ViewEngines
{
    public class UIVersionedRazorViewEngine : RazorViewEngine
    {
        public UIVersionedRazorViewEngine()
            : base()
        {
            AreaViewLocationFormats = new[] {
                "~/Areas/{2}/Views/{1}/{0}.v%UIVersion%.cshtml",
                "~/Areas/{2}/Views/{1}/{0}.v%UIVersion%.vbhtml",
                "~/Areas/{2}/Views/Shared/{0}.v%UIVersion%.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.v%UIVersion%.vbhtml",
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/{1}/{0}.vbhtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.vbhtml"
            };

            AreaMasterLocationFormats = new[] {
                "~/Areas/{2}/Views/{1}/{0}.v%UIVersion%.cshtml",
                "~/Areas/{2}/Views/{1}/{0}.v%UIVersion%.vbhtml",
                "~/Areas/{2}/Views/Shared/{0}.v%UIVersion%.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.v%UIVersion%.vbhtml",
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/{1}/{0}.vbhtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.vbhtml"
            };

            AreaPartialViewLocationFormats = new[] {
                "~/Areas/{2}/Views/{1}/{0}.v%UIVersion%.cshtml",
                "~/Areas/{2}/Views/{1}/{0}.v%UIVersion%.vbhtml",
                "~/Areas/{2}/Views/Shared/{0}.v%UIVersion%.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.v%UIVersion%.vbhtml",
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/{1}/{0}.vbhtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.vbhtml"
            };

            ViewLocationFormats = new[] {
                "~/Views/{1}/{0}.v%UIVersion%.cshtml",
                "~/Views/{1}/{0}.v%UIVersion%.vbhtml",
                "~/Views/Shared/{0}.v%UIVersion%.cshtml",
                "~/Views/Shared/{0}.v%UIVersion%.vbhtml",
                "~/Views/{1}/{0}.cshtml",
                "~/Views/{1}/{0}.vbhtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Views/Shared/{0}.vbhtml"
            };

            MasterLocationFormats = new[] {
                "~/Views/{1}/{0}.v%UIVersion%.cshtml",
                "~/Views/{1}/{0}.v%UIVersion%.vbhtml",
                "~/Views/Shared/{0}.v%UIVersion%.cshtml",
                "~/Views/Shared/{0}.v%UIVersion%.vbhtml",
                "~/Views/{1}/{0}.cshtml",
                "~/Views/{1}/{0}.vbhtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Views/Shared/{0}.vbhtml"
            };

            PartialViewLocationFormats = new[] {
                "~/Views/{1}/{0}.v%UIVersion%.cshtml",
                "~/Views/{1}/{0}.v%UIVersion%.vbhtml",
                "~/Views/Shared/{0}.v%UIVersion%.cshtml",
                "~/Views/Shared/{0}.v%UIVersion%.vbhtml",
                "~/Views/{1}/{0}.cshtml",
                "~/Views/{1}/{0}.vbhtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Views/Shared/{0}.vbhtml"
            };
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return base.CreatePartialView(controllerContext, ContextualizePath(controllerContext, partialPath));
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            var nameSpace = controllerContext.Controller.GetType().Namespace;
            return base.CreateView(controllerContext, ContextualizePath(controllerContext, viewPath), ContextualizePath(controllerContext, masterPath));
        }

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            var nameSpace = controllerContext.Controller.GetType().Namespace;
            return base.FileExists(controllerContext, ContextualizePath(controllerContext, virtualPath));
        }

        protected string ContextualizePath(ControllerContext controllerContext, string path)
        {
            var UIVersion = 1;
            if (controllerContext.Controller is BaseController)
            {
                UIVersion = (controllerContext.Controller as BaseController).UIVersion;
            }
            return path.Replace("%UIVersion%", UIVersion.ToString());
        }
    }
}