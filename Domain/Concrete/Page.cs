using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Concrete
{
   public class Page:BaseEntity
    {
        public string Name { get; set; }

        public string Slug { get; set; }
        public int? LayoutId { get; set; }
        public int? MenuId { get; set; }
        public virtual Layout Layout { get; set; }
        public virtual Menu Menu { get; set; }

        public virtual List<PageContent> PageContents { get; set; }

    }
}
