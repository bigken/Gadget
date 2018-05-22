using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gadget.Api.Models
{
    public class ArticalModel
    {
        public long ArticalId { get; set; }

        public string Title { get; set; }

        public DateTime PublishedDateTime { get; set; }

        public string ContextFilePath { get; set; }

        public int Rating { get; set; }

        public long AuthorId { get; set; }

        public string AuthorName { get; set; }
        
        public string AuthorAvatar { get; set; }
    }
}
