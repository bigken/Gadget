using System.ComponentModel.DataAnnotations;

namespace Gadget.Data.Entity
{
    public class Author : BaseEntity
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
    }
}
