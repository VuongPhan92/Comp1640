using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace devPMS.Web.Controllers
{
    public class ScriptsController : Controller
    {
        // for more information: http://blog.pmunin.com/2013/04/dynamic-javascript-css-in-aspnet-mvc.html
        [ActionName("ProjectTable.js")]
        public ActionResult Dynamic(string ShortCutNav)
        {
            return this.JavaScriptFromView(model: ShortCutNav);
        }

        [ActionName("SubmitProjectFV.js")]
        public ActionResult Dynamic2()
        {
            return this.JavaScriptFromView();
        }

        [ActionName("ProjectDetailFV.js")]
        public ActionResult Dynamic3()
        {
            return this.JavaScriptFromView();
        }

        public ActionResult Index()
        {
            return Content("Scripts folder");
        }

        protected override void HandleUnknownAction(string actionName)
        {
            var res = this.JavaScriptFromView();
            res.ExecuteResult(ControllerContext);
        }
    }
}