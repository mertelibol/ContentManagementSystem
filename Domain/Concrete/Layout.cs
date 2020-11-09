using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Concrete
{
    public class Layout:BaseEntity

    {
        public string Name { get; set; }

        public ICollection<Page> Pages { get; set; }
        public virtual ICollection<LayoutItem> LayoutItems { get; set; }

    }
}
