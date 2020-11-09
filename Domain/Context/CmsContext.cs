using Domain.Concrete;
using Microsoft.EntityFrameworkCore;
using Raven.Client.Documents.Operations.ETL.SQL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Context
{
   public class CmsContext:DbContext
    {
        public CmsContext()
        {
        }

        public CmsContext(DbContextOptions<CmsContext> options) : base(options)
        {


        }
        //    protected override void OnConfiguring(DbContextOptionsBuilder options)
        //=> options.UseSqlServer("DataSource=LAPTOP-56NBTJ4K");

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        //{

        //    if (optionsbuilder.IsConfigured)
        //    {
        //        //string sqlConnectionString = null;
        //        optionsbuilder.UseSqlServer("Data Source=LAPTOP-56NBTJ4K\\SQLSERVERMERT;Initial Catalog=ProjectCMSDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", builder => builder.EnableRetryOnFailure());
        //    }

        //}
        public virtual DbSet<Layout> Layouts { get; set; }
        public DbSet<LayoutItem> LayoutItems { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageContent> PageContents { get; set; }
        
        public DbSet<User> Users { get; set; }

        
        public DbSet<Menu> Menus { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-56NBTJ4K\\SQLSERVERMERT;Initial Catalog=ProjectCMSDB;Integrated Security=True;");
        }

    }
}
