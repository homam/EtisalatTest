using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace EtisalatTest
{
    public class WriteOnlyDB
    {
        private readonly HttpContext Context;

        public WriteOnlyDB(HttpContext context)
        {
            Context = context;
        }

        private static readonly object _VisitsFilePathLocker = new object();
        private volatile static string _VisitsFilePath;
        private string VisitsFilePath
        {
            get
            {
                if (_VisitsFilePath == null)
                {
                    lock (_VisitsFilePathLocker)
                        if (_VisitsFilePath == null)
                        {
                            var path = Context.Server.MapPath("~/wvisits.json");
                            if(!File.Exists(path))
                                using (var fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                                {
                                    fs.Close();
                                }
                            return _VisitsFilePath = Context.Server.MapPath("~/wvisits.json");
                        }
                }
                return _VisitsFilePath;
            }
        }


        static readonly object _CommitLocker = new object();
        private void Commit(Visit visit)
        {
            lock (_CommitLocker)
            {
                var serializer = new JavaScriptSerializer();
                using (var fs = new FileStream(VisitsFilePath, FileMode.Append, FileAccess.Write))
                {
                    using (TextWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(serializer.Serialize(visit));
                        sw.Close();
                    }
                    fs.Close();
                }
            }
        }


        public void AddVisit(Visit visit)
        {
            Commit(visit);
        }
    }
}