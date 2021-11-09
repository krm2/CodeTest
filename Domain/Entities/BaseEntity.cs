using System;

namespace Domain.Entities
{
    public class BaseEntity
    {
        public DateTime CreatedDt { get; set; }
        public DateTime UpdatedDt { get; set; }
        public string CreatedBy { get; set; }
    }
}
