namespace Gadget.Data.Entity
{
    using System;

    public class Comment : BaseEntity
    {
        public string Content { get; set; }

        public DateTime PublishedDateTime { get; set; }

        public Author Author { get; set; }

        public Article Article { get; set; }
    }
}
