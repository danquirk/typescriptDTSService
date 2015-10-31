using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace typescriptDTS.Controllers
{
    public class DtsHistoryRecord
    {
        /// <summary>
        /// Filename
        /// </summary>
        public string n;
        /// <summary>
        /// Path
        /// </summary>
        public string p;
        /// <summary>
        /// History (list of dtsHash values, newest in index 0)
        /// </summary>
        public string[] h; 

    }
    public class IndexController : ApiController
    {
        private string indexPath = System.Web.HttpContext.Current.Server.MapPath("~/newsboy/");
        
        // GET: api/Index
        public DtsHistoryRecord[] Get()
        {
            using (StreamReader file = File.OpenText(indexPath + "data.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                DtsHistoryRecord[] indexData = (DtsHistoryRecord[])serializer.Deserialize(file, typeof(DtsHistoryRecord[]));
                return indexData;
            }
        }

        // GET: api/Index/a
        public Object Get(string id)
        {
            try
            {
                var indexTimestamp = File.GetLastWriteTime(indexPath);
                var serveNewIndex = id != null;
                var clientTimestamp = new DateTime(1970, 1, 1).AddSeconds(Int32.Parse(id));
                if (serveNewIndex && clientTimestamp < indexTimestamp)
                {
                    string indexText = File.ReadAllText(indexPath + "data.json");
                    return Json(indexText);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
