using System;

namespace hhru_api.Entities
{
    public class AreaEntity
    {
        public int id { get; set; }
        public String name { get; set; }

        public AreaEntity(int id, String name)
        {
            this.id = id;
            this.name = name;
        }
    }
}