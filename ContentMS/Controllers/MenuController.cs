using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTOs;
using Core.Services.Abstract;
using Core.Services.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContentMS.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly ILogger<MenuController> _logger;

        public MenuController(IMenuService menuService,ILogger<MenuController> logger )
        {
            _menuService = menuService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            IEnumerable<MenuDto> model = _menuService.GetMenus();
           

                return View(model);
            }


        [HttpGet]
        public IActionResult Add()
        {
            var model = _menuService.GetMenus();
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Add(string Name, int? parentId, string icon)
        {
            _menuService.InsertNewMenu(Name, parentId, icon);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(string MenuName)
        {
           _menuService.DeleteMenu(MenuName);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(string MenuName)
        {
            var menu = _menuService.GetMenuByName(MenuName);
           
                ViewBag.Name = menu.Name;
                ViewBag.ParentId = menu.ParentId;
                ViewBag.Icon = menu.Icon;
            

               var model = _menuService.GetMenus();

               return View(model);
        }

      

        [HttpPost]
        public IActionResult Update(string Name, int? parentId, string icon, string oldName)
        {
           _menuService.UpdateMenu(Name, parentId, icon, oldName);
            return RedirectToAction("Index");
        }
    }
}
