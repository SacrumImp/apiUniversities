using System;

namespace hhru_api.Models
{
    class Universities
    {
        public University[] items { get; set; }

        public University getFirst()
        {
            return this.items[0];
        }

    }

    public class University
    {
        public String id { get; set; }

        public String acronym { get; set; }

        public String text { get; set; }

        public String synonyms { get; set; }

        public Area area { get; set; }
    }

    public class Area
    {
        public String id { get; set; }
        public String name { get; set; }

    }

}