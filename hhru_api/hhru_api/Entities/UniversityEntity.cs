using System;

namespace hhru_api.Entities
{
    public class UniversityEntity
    {
        public int id { get; set; }
        public String acronym { get; set; }
        public String text { get; set; }
        public String synonyms { get; set; }
        public int idArea { get; set; }

        public UniversityEntity(int id, String acronym, String text, String synonyms, int idArea)
        {
            this.id = id;
            this.acronym = acronym;
            this.text = text;
            this.synonyms = synonyms;
            this.idArea = idArea;
        }

    }
}