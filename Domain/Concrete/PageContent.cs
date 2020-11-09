using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Concrete
{
   public class PageContent:BaseEntity
    {
        public string Content { get; set; }
        public int? PageId { get; set; }
        public string Class { get; set; }
        public virtual Page Page { get; set; }

    }
}
