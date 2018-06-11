namespace Gadget.Data.Entity
{
    using System;
    using System.Collections.Generic;

    public class Article : BaseEntity
    {
        public string Title { get; set; }

        public DateTime PublishedDateTime { get; set; }

        public string ContextFilePath { get; set; }

        public int Rating { get; set; }

        public Author Author { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
