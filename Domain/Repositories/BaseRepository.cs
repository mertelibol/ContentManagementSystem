using Domain.Abstract;
using Domain.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Repositories
{
    public class BaseRepository<T>:IDisposable  where T : class, IBaseEntity
    {

        public CmsContext context = null;
        private DbSet<T> _dbSet;

        public BaseRepository()
        {
            context = new CmsContext();
            _dbSet = context.Set<T>();
        }



        public virtual bool Add(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            entity.IsDeleted = false;
            _dbSet.Add(entity);

            return context.SaveChanges() > 0;
        }

        public virtual T Find(int Id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == Id);
        }

        public virtual bool Delete(T entity)//soft delete!!! (update için kullanmıyoruz çünkü tüm kolonları siliyoruz)
        {
            var record = Find(entity.Id);

            record.IsDeleted = true;
            return context.SaveChanges() > 0;
            

        }

        public virtual bool DeleteLayout(T entity)//hard delete!!!! (layoutun tüm itemlerini siliyoruz)
        {


             
        
            //var record = Find(entity.Id);

            _dbSet.Remove(entity);
            return context.SaveChanges() > 0;

        



    }




        public virtual bool Update(T entity)
        {

            _dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;

            return context.SaveChanges() > 0;


          
        }

        public virtual IQueryable<IBaseEntity> ListAll()
        {
            return _dbSet;
        }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }

        public IQueryable<E> Query<E>() where E : class
        {
            return context.Set<E>();
        }
    }
   

}

