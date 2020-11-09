using Common.DTOs;
using Core.Services.Abstract;
using Domain.Concrete;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Services.Concrete
{
    public class PageService:IPageService
    {
        public IEnumerable<PageDto> GetPages()
        {
            IEnumerable<PageDto> pagelist;
            using (BaseRepository<Page> _repo = new BaseRepository<Page>())
            {
                pagelist= _repo.Query<Page>().Where(x => x.IsDeleted == false).Select(p => new PageDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    LayoutName = p.Layout.Name,
                }).ToList();
            }
            return pagelist;
        }

        public PageDto GetPageByName(string Name)
        {
            PageDto page;
            using (BaseRepository<Page> _repo = new BaseRepository<Page>())
            {
                page= _repo.Query<Page>().Where(x => x.Name == Name).Select(p => new PageDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    LayoutName = p.Layout.Name,
                    PageContents = p.PageContents.Select(x => new PageContentDto
                    {
                        Id = x.Id,
                        Class = x.Class,
                        PageId = x.PageId,
                        Content = x.Content
                    })
                }).FirstOrDefault();
            }
            return page;
        }

        public List<PageContentDto> GetPageById(int id)
        {
            using (BaseRepository<Page> _repo = new BaseRepository<Page>())
            {
                var sonuc = _repo.Query<Page>().Where(c => c.Id == id).FirstOrDefault();

                using (BaseRepository<PageContent> repository = new BaseRepository<PageContent>())
                {
                    List<PageContentDto> icerik = new List<PageContentDto>();
                    var veriler = repository.Query<PageContent>().Where(x => x.PageId == sonuc.Id).ToList();

                    foreach (var pageContent in veriler)
                    {
                        PageContentDto dto = new PageContentDto
                        {
                            Id = pageContent.Id,
                            Class = pageContent.Class,
                            Content = pageContent.Content,
                            PageId = pageContent.Id
                        };

                        icerik.Add(dto);
                    }

                    return icerik;
                }


            }
        }

        public void InsertNewPage(string Name, Array Contents, int LayoutID, int MenuId, string[] Classes)
        {
            using (BaseRepository<Page> _repo = new BaseRepository<Page>())
            {
                Page page = new Page
                {
                    Name = Name,
                    CreatedAt = DateTime.Now,
                    LayoutId = LayoutID,
                    IsDeleted = false,
                   
                    MenuId = MenuId
                };
                _repo.Add(page);
            }

            using (BaseRepository<PageContent> _repo = new BaseRepository<PageContent>())
            {
                var sonEklenen = _repo.Query<Page>().Where(x => x.Name == Name).FirstOrDefault();

                var i = 0;
                foreach (var icerik in Contents)
                {
                    var content = new PageContent
                    {
                        Content = icerik.ToString(),
                        CreatedAt = DateTime.Now,
                        IsDeleted = false,
                        Class = Classes[i],
                        PageId = sonEklenen.Id
                    };

                    _repo.Add(content);
                    i++;
                }
            }
        }

        public void UpdatePage(string Name, List<string> Contents, int LayoutID, int MenuId, List<string> Class, string oldPageName)
        {
            Page page2 = new Page();
            using (BaseRepository<Page> _repo = new BaseRepository<Page>())
            {

                var page = _repo.Query<Page>().FirstOrDefault(c => c.Name == oldPageName);
                page.Name = Name;
                page.LayoutId = LayoutID;
                page.UpdatedAt = DateTime.Now;
                _repo.Update(page);

                using (BaseRepository<PageContent> _repos = new BaseRepository<PageContent>())
                {
                    int i = 0;
                    var pagecontents = _repos.Query<PageContent>().Where(x => x.PageId == page.Id).ToList();
                   
                    foreach (var icerik in Contents)
                    {
                        var con = new PageContent
                        {
                            Content = icerik.ToString(),
                            CreatedAt = DateTime.Now,
                            IsDeleted = false,
                            Class = Class.ToString(),
                            PageId = page.Id
                        };

                        _repos.Add(con);
                        i++;
                    }

                    foreach (var oldcontent in pagecontents)
                    {
                        _repos.DeleteLayout(oldcontent);
                    }

                }
            }

        }

        public int getMenuId(int pageId)
        {
            using (BaseRepository<Page> _repos = new BaseRepository<Page>())
            {
                Page page = _repos.Query<Page>().Where(x => x.Id == pageId).FirstOrDefault();
                return (int)page.MenuId;
            }

        }

        public void DeletePage(string Name)
        {
            throw new NotImplementedException();
        }

    }
}
