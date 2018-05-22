using System;
using System.Collections.Generic;
using System.Text;

namespace Gadget.Data.Entity
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public DateTime PublishedDateTime { get; set; }
        
        public Author Author { get; set; }
        public Article Article { get; set; }
    }
}
