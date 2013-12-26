using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace EtisalatTest
{
    public class DB
    {
        private readonly HttpContext Context;

        public DB(HttpContext context)
        {
            Context = context;
        }

        private static readonly object _VisitsFilePathLocker = new object();
        private volatile static string _VisitsFilePath;
        private  string VisitsFilePath
        {
            get
            {
                if(_VisitsFilePath == null)
                {
                    lock (_VisitsFilePathLocker)
                        if (_VisitsFilePath == null)
                            return _VisitsFilePath = Context.Server.MapPath("~/visits.json");
                }
                return _VisitsFilePath;
            }
        }

        private static readonly  object _VisitsLocker = new object();
        private volatile static List<Visit> _Visits;
        private List<Visit> Visits
        {
            get
            {
                if(_Visits == null)
                    lock(_VisitsLocker)
                        if (_Visits == null)
                        {
                            var serializer = new JavaScriptSerializer();
                            using (var fs = new FileStream(VisitsFilePath, FileMode.Open, FileAccess.Read))
                            {
                                using (var sr = new StreamReader(fs))
                                {
                                    _Visits = serializer.Deserialize<Visit[]>(sr.ReadToEnd()).ToList();
                                }
                            }
                        }
                return _Visits;
            }
        }


        static readonly object _CommitLocker = new object();
        private void Commit()
        {
            lock(_CommitLocker)
            {
                var serializer = new JavaScriptSerializer();
                using (var fs = new FileStream(VisitsFilePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    using (TextWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(serializer.Serialize(Visits));
                        sw.Close();
                    }
                    fs.Close();
                }
            }
        }


        public void AddVisit(Visit visit)
        {
            Visits.Add(visit);
            Commit();
        }
    }
}