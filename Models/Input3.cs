using System.Collections.Generic;

namespace Models
{
    public class Input3
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public List<Package> Packages { get; set; }
    }

    public class Package
    {
        public int Value { get; set; }
    }
}
