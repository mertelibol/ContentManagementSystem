using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Concrete
{
  public  class LayoutItem:BaseEntity
    {
        public string Class { get; set; }
        public int LayoutId { get; set; }
        public virtual Layout Layout { get; set; }

    }
}
