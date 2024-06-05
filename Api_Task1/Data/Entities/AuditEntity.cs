﻿namespace Api_Task1.Data.Entities
{
    public class AuditEntity:BaseEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ModifiedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
