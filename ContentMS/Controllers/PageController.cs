using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ContentMS.Models;
using Core.Services.Abstract;
using Domain.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContentMS.Controllers
{
    public class PageController : Controller
    {
        private readonly IPageService _pageService;
        private readonly ILayoutService _layoutService;
        private readonly IMenuService _menuService;
        //private readonly IHostingEnvironment _env;
        public PageController(IPageService pageService, ILayoutService layoutService, IMenuService menuService/* IHostingEnvironment env*/)
        {
            _pageService = pageService;
            _layoutService = layoutService;
            _menuService = menuService;
            //_env = env;
        }
        public IActionResult Index()
        {

            var model = _pageService.GetPages();

            return View(model);
            
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = _layoutService.GetLayouts();
            ViewBag.menulist = _menuService.GetMenus();
            return View(model);
        }

        [HttpGet]
        public IActionResult Update(string pageName)
        {
            using (CmsContext context = new CmsContext())
            {
                LayoutModel model = new LayoutModel();

                model.Layouts = context.Layouts.Where(x => x.IsDeleted == false).ToList();
                model.Pages = context.Pages.Include("PageContents").Where(z => z.Name == pageName).ToList();
                ViewBag.page = context.Pages.Include("PageContents").SingleOrDefault(x => x.Name == pageName);
                var page = context.Pages.FirstOrDefault(y => y.Name == pageName);
                ViewBag.layout = context.Layouts.Include("LayoutItems").SingleOrDefault(x => x.Id == page.LayoutId);



                return View(model);
            }

        }

        [HttpGet]
        public IActionResult Delete(string pageName)
        {
            using (CmsContext ctx = new CmsContext())
            {
                var page = ctx.Pages.Where(x => x.Name == pageName).FirstOrDefault();
                page.IsDeleted = true;
                return ctx.SaveChanges() > 0 ? RedirectToAction("Index") : null;
            }
        }

        [HttpGet]
        public JsonResult LayoutCagir(int cagırdıgım)
        {
            CmsContext context = new CmsContext();
            var bilgiler = context.LayoutItems.Where(x => x.LayoutId == cagırdıgım).Select(a => a.Class).ToList();


            return Json(bilgiler);
        }


        [HttpPost]
        
        public IActionResult Add(string Name, string[] txtArealar, int layoutId, int menuId, string[] Classes)
        {
            _pageService.InsertNewPage(Name, txtArealar, layoutId, menuId, Classes);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(string Name, List<string> txtArealar, int layoutId, int menuId, List<string> Classes, string oldPageName)
        {
            _pageService.UpdatePage(Name, txtArealar, layoutId, menuId, Classes,oldPageName);
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult UploadImage(IFormFile upload)
        //{
        //    if (upload.Length <= 0) return null;

        //    var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();


        //    var path = Path.Combine(
        //        _env.WebRootPath, "upload/img",
        //        fileName);

        //    using (var stream = new FileStream(path, FileMode.Create))
        //    {
        //        upload.CopyTo(stream);

        //    }

        //    var url = $"{"/upload/img/"}{fileName}";

        //    return Json(new { uploaded = true, url });
        //}

    }
}
