namespace EtisalatTest
{
    public class Header
    {
        public string Name { get; set; }
        public string[] Values { get; set; }

        public Header(string name, string[] values)
        {
            Name = name;
            Values = values;
        }

        public Header(){}
    }
}