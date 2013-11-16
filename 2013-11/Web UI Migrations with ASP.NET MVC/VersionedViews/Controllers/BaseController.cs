using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VersionedViews.Controllers
{
    /// <summary>
    /// Our Base Controller Class that we'll use as base of all of our Controllers.
    /// We'll define common behavior here.
    /// </summary>
    public abstract class BaseController : Controller
    {
        public virtual int UIVersion
        {
            get { return 1; }
        }
    }
}
