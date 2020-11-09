using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTOs;
using Core.Services.Abstract;
using Domain.Context;
using Microsoft.AspNetCore.Mvc;

namespace ContentMS.Controllers
{
    public class PreviewController : Controller
    {

        private readonly IPageService _pageService;
        private readonly ILayoutService _layoutService;
        private readonly IMenuService _menuService;
        public PreviewController(IPageService pageService, ILayoutService layoutService, IMenuService menuService)
        {
            _pageService = pageService;
            _layoutService = layoutService;
            _menuService = menuService;
        }
        public IActionResult Index(int pageId)
        {
            List<PageContentDto> model = _pageService.GetPageById(pageId);
            //int menuıd = _pageService.getMenuId(pageId);
            //using (CmsContext context = new CmsContext())
            //{
            //    ViewBag.menu = context.Menus.Where(x => x.Id == menuıd).FirstOrDefault();
            //}
            return View(model);
        }
    }
}
