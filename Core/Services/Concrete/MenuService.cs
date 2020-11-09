using Common.DTOs;
using Core.Services.Abstract;
using Domain.Concrete;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services.Concrete
{
   public class MenuService:IMenuService
    {
        public IEnumerable<MenuDto> GetMenus()
        {
            using (BaseRepository<Menu> _repo = new BaseRepository<Menu>())
            {
                return _repo.Query<Menu>().Where(m => !m.IsDeleted).Select(m => new MenuDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    UpdatedAt = m.UpdatedAt,
                    ParentId = m.ParentId,
                    Pages = m.Page.Select(x => new PageDto()
                    {
                        Name = x.Name,
                        LayoutId = x.LayoutId
                    })
                }).ToList();
            }
        }

      

        public MenuDto GetMenuByName(string Name)
        {
            using (BaseRepository<Menu> _repo = new BaseRepository<Menu>())
            {
                return _repo.Query<Menu>().Where(p => p.Name == Name).Select(p => new MenuDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    ParentId = p.ParentId,
                    Icon = p.Icon
                }).FirstOrDefault();
            }
        }

        public void InsertNewMenu(string Name, int? ParentId, string icon)
        {
            using (BaseRepository<Menu> _repo = new BaseRepository<Menu>())
            {
                Menu newMenu = new Menu
                {
                    Name = Name,
                    Icon = icon,
                    ParentId = ParentId
                };
                _repo.Add(newMenu);
            }
        }

        public void UpdateMenu(string Name, int? ParentId, string icon, string oldName)
        {
            using (BaseRepository<Menu> _repo = new BaseRepository<Menu>())
            {
                //Layout Güncelleme İşlemi
                var menuBilgiler = _repo.Query<Menu>().FirstOrDefault(c => c.Name == oldName);
                menuBilgiler.Name = Name;
                menuBilgiler.ParentId = ParentId;
                menuBilgiler.Icon = icon;
                menuBilgiler.UpdatedAt = DateTime.Now;
                _repo.Update(menuBilgiler);
            }
        }

        public void DeleteMenu(string Name)
        {
            using (BaseRepository<Menu> _repo = new BaseRepository<Menu>())
            {
                var layout = _repo.Query<Menu>().Where(c => c.Name == Name).FirstOrDefault();
                _repo.Delete(layout);
            }
        }
    }

}

