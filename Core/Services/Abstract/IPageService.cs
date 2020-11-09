using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Abstract
{
    public  interface IPageService
    {
        IEnumerable<PageDto> GetPages();

        PageDto GetPageByName(string Name);
        List<PageContentDto> GetPageById(int id);

        void InsertNewPage(string Name, Array Columns, int LayoutID, int MenuId, string[] Class);
        void UpdatePage(string Name, List<string> Columns, int LayoutID, int MenuId, List<string> Class, string oldPageName);
        void DeletePage(string Name);
        int getMenuId(int pageId);
    }
}
