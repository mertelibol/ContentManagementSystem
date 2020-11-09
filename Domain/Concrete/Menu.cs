using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Concrete
{
   public  class Menu:BaseEntity
    {
        [StringLength(100)]
      
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int OrderNo { get; set; }
        public string Icon { get; set; }
        public virtual Menu ParentMenu { get; set; }
        public virtual List<Menu> Menus { get; set; }
        public virtual ICollection<Page> Page { get; set; }

    }
}
