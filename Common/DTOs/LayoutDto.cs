using Common.DTOs.AbstractDTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs
{
   public class LayoutDto:BaseDto
    {
        

        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public IEnumerable<LayoutItemDto> Items { get; set; }
    }

    public class LayoutItemDto : BaseDto
    {
        public int LayoutId { get; set; }
        public string Class { get; set; }

    }


}

