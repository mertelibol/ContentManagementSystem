using Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentMS.Models
{
    public class PageModel
    {
        public virtual IEnumerable<Page> PageDtos { get; set; }
        public virtual IEnumerable<Menu> MenuDto { get; set; }

    }
}
