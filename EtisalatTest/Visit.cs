using System;

namespace EtisalatTest
{
    public class Visit
    {
        public string Visitor { get; set; }
        public bool Wifi { get; set; }
        public DateTime Date { get; set; }
        public string IP { get; set; }
        public Header[] Headers { get; set; }

        public Visit(string visitor, bool wifi, DateTime date, string ip, Header[] headers)
        {
            Visitor = visitor;
            Wifi = wifi;
            Date = date;
            IP = ip;
            Headers = headers;
        }

        public Visit(){}
    }
}