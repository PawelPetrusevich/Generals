using System;

namespace General.DAL.Common.Models
{
    public class Entity
    {
        public string Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}