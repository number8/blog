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
            get { return base.UIVersion; }
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
