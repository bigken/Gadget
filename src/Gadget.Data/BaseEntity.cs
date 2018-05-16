using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Gadget.Data
{
    public abstract class BaseEntity
    {
        [Key]
        public long Id { get; set; }

        public DateTime CreatedDateTimeUtc { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedDateTimeUtc { get; set; }

        public string UpdatedBy { get; set; }
    }
}
