using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using MvcJson.Net.Models;

namespace MvcJson.Net.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index(bool preferred = false)
        {
            Func<Customer, bool> exp = null;
            if (preferred)
            {
                exp = c => c.Preferred;
            }

            var custs = Customer.GetAll(exp);

            if (Request.IsAjaxRequest())
            {
                return Json(custs);
            }
            return View(custs);
        }

        /// <summary>
        /// Creates a System.Web.Mvc.JsonResult object that serializes the specified object to JavaScript Object Notation (JSON).
        /// </summary>
        /// <param name="data">The JavaScript object graph to serialize.</param>
        /// <param name="contentType">The content type (MIME type).</param>
        /// <param name="contentEncoding">The content encoding.</param>
        /// <param name="behavior">The JSON request behavior</param>
        /// <returns>
        /// The JSON result object that serializes the specified object to JSON format.
        /// The result object that is prepared by this method is written to the response
        /// by the ASP.NET MVC framework when the object is executed.
        /// </returns>
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            // No need to override other overloads since they all eventually call this one.
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior // Technically no longer needed since we handle adding a container.
            };
        }
    }
}
