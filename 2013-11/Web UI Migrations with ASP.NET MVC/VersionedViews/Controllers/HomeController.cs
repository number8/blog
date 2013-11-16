using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VersionedViews.Controllers
{
    public class HomeController : BaseController
    {
        public override int UIVersion
        {
            get 
            {
                var UIVersion = 1;
                Int32.TryParse(this.ControllerContext.HttpContext.Request.QueryString["UIVersion"], out UIVersion);
                return UIVersion;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
