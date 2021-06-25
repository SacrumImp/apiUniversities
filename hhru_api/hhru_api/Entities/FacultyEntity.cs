using System;

namespace hhru_api.Entities
{
    public class FacultyEntity
    {
        public int id { get; set; }
        public int universityId { get; set; }
        public String name { get; set; }

        public FacultyEntity(int id, int universityId, String name)
        {
            this.id = id;
            this.universityId = universityId;
            this.name = name;
        }

    }
}