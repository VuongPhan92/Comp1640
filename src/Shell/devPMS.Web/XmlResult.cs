using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace devPMS.Web
{
    public class XmlResult<T> : ActionResult
    {
        public T Data { private get; set; }
        public string FileName { private get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            HttpContextBase httpContextBase = context.HttpContext;
            httpContextBase.Response.Buffer = true;
            httpContextBase.Response.Clear();

            if (string.IsNullOrEmpty(FileName))
            {
                FileName = DateTime.Now.ToString("ddmmyyyyhhss");
            }

            httpContextBase.Response.AddHeader("content-disposition", "attachement; filename =" + FileName + ".xml");
            httpContextBase.Response.ContentType = "text/xml";

            using (StringWriter writer = new StringWriter())
            {
                XmlSerializer xml = new XmlSerializer(typeof(T));
                xml.Serialize(writer, Data);
                httpContextBase.Response.Write(writer);
            }
        }
    }
}