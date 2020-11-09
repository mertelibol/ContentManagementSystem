using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Abstract
{
    public interface ILayoutService
    {
        IEnumerable<LayoutDto> GetLayouts();

        LayoutDto GetLayoutByName(string Name);

        void InsertNewLayout(string Name, List<string> Kolonlar);
        void UpdateLayout(string oldName, string Name, List<string> Columns);
        void DeleteLayout(string Name);

    }
}
