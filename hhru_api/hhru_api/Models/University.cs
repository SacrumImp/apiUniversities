using System;

class Universities
{
    public University[] items { get; set; }

    public University getFirst()
    {
        return this.items[0];
    }

}

class University
{
    public String id { get; set; }

    public String acronym { get; set; }

    public String text { get; set; }

    public String synonyms { get; set; }

    public Area area { get; set; }
}

class Area
{
    public String id { get; set; }
    public String name { get; set; }
}