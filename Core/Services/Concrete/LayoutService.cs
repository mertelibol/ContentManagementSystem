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
    public class LayoutService : ILayoutService
    {
        public IEnumerable<LayoutDto> GetLayouts()
        {
            IEnumerable<LayoutDto> layoutlist;
            using (BaseRepository<Layout> _repo = new BaseRepository<Layout>())
            {
                layoutlist= _repo.Query<Layout>().Where(x => x.IsDeleted == false).Select(p => new LayoutDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    IsDeleted = p.IsDeleted,
                    Items = p.LayoutItems.Select(x => new LayoutItemDto()
                    {
                        Id = x.Id,
                        Class = x.Class,
                    })
                }).ToList();
            }
            return layoutlist;
        }


        public LayoutDto GetLayoutByName(string Name)
        {
            LayoutDto layout;
            using (BaseRepository<Layout> _repo = new BaseRepository<Layout>())
            {
                layout= _repo.Query<Layout>().Where(p => p.Name == Name).Select(p => new LayoutDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Items = p.LayoutItems.Where(x => !x.IsDeleted).Select(x => new LayoutItemDto
                    {
                        Id = x.Id,
                        Class = x.Class

                    })
                }).FirstOrDefault();
            }
            return layout;
        }

        public void UpdateLayout(string oldName, string Name, List<string> columns)
        {
            Layout layout = new Layout();
            using (BaseRepository<Layout> _repo = new BaseRepository<Layout>())
            {
                //Layout Güncelleme İşlemi
                var layoutBilgiler = _repo.Query<Layout>().FirstOrDefault(c => c.Name == oldName);
                if (Name != oldName)
                {
                    layoutBilgiler.Name = Name;
                    _repo.Update(layoutBilgiler);
                }

                using (BaseRepository<LayoutItem> _repository = new BaseRepository<LayoutItem>())
                {
                    //Layout İtemlerini silme işlemi
                    var eskilayoutKolonlar = _repository.Query<LayoutItem>().Where(x => x.LayoutId == layoutBilgiler.Id)
                        .ToList();
                    foreach (var oldColumn in eskilayoutKolonlar)
                    {
                        _repository.DeleteLayout(oldColumn);
                    }
                    //yenilayout ekleme işlemi
                    foreach (var kolonBilgisi in columns)
                    {
                        var post = new LayoutItem
                        { Class = kolonBilgisi.ToString(),
                            LayoutId = layoutBilgiler.Id, 
                            UpdatedAt = DateTime.Now,
                            CreatedAt = DateTime.Now, 
                            IsDeleted = false };
                        _repository.Add(post);
                    }

                }

            }
        }

        public void DeleteLayout(string Name)
        {
            using (BaseRepository<Layout> _repo = new BaseRepository<Layout>())
            {
                var layout = _repo.Query<Layout>().Where(c => c.Name == Name).FirstOrDefault();
                _repo.Delete(layout);
            }
        }

        public void InsertNewLayout(string Name, List<string> Kolonlar)
        {
            Layout layout = new Layout();
            using (BaseRepository<Layout> _repo = new BaseRepository<Layout>())
            {
                Layout pl = new Layout
                {
                    Name = Name,
                    CreatedAt = DateTime.Now,
                    IsDeleted = false
                };
                _repo.Add(pl);
            }

            using (BaseRepository<LayoutItem> _repo = new BaseRepository<LayoutItem>())
            {
                layout = _repo.Query<Layout>().Where(c => c.Name == Name).First();


                foreach (var kolonBilgisi in Kolonlar)
                {
                    var post = new LayoutItem { Class = kolonBilgisi.ToString(), LayoutId = layout.Id, };
                    _repo.Add(post);
                }
            }
        }
    }
}
