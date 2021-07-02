using System;

namespace EcommerceVT.Model
{
    public class File
    {
        public File(string name, string url, string format)
        {
            Name = name;
            Url = url;
            Format = format;
        }

        public string Name { get; private set; }
        public string Url { get; private set; }
        public string Format { get; private set; }
    }
}
