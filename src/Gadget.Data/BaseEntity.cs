namespace Gadget.Data
{
    using System;

    public abstract class BaseEntity
    {
        public long Id { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedDateTime { get; set; }

        public string UpdatedBy { get; set; }
    }
}
