using Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentMS.Models
{
    public class LayoutModel
    {
        public virtual ICollection<Layout> Layouts { get; set; }
        public virtual ICollection<Page> Pages { get; set; }

    }
}
