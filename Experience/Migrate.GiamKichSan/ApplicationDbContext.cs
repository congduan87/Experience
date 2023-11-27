using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Migrate.GiamKichSan
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //options.UseSqlServer(
            //       Configuration.GetConnectionString("DefaultConnection")));
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            // Bỏ tiền tố AspNet của các bảng: mặc định các bảng trong IdentityDbContext có
            // tên với tiền tố AspNet như: AspNetUserRoles, AspNetUser ...
            // Đoạn mã sau chạy khi khởi tạo DbContext, tạo database sẽ loại bỏ tiền tố đó
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
        }

        //Acounts
        public DbSet<Model.GiamKichSan.Models.Accounts.Account_Entity> acounts_Accounts { get; set; }
        public DbSet<Model.GiamKichSan.Models.Accounts.DocumentUpload_Entity> acounts_DocumentUploads { get; set; }


        //MyIPs
        public DbSet<Model.GiamKichSan.Models.MyIPs.HomeAddress_Entity> myIPs_HomeAddresss { get; set; }


        //Blogs
        public DbSet<Model.GiamKichSan.Models.Blogs.Blog_Entity> blogs_Blogs { get; set; }
        public DbSet<Model.GiamKichSan.Models.Blogs.BlogCategory_Entity> blogs_BlogCategories { get; set; }
        public DbSet<Model.GiamKichSan.Models.Blogs.BlogComment_Entity> blogs_BlogComments { get; set; }
        public DbSet<Model.GiamKichSan.Models.Blogs.BlogDetail_Entity> blogs_BlogDetails { get; set; }
        public DbSet<Model.GiamKichSan.Models.Blogs.BlogTag_Entity> blogs_BlogTags { get; set; }
        public DbSet<Model.GiamKichSan.Models.Blogs.Category_Entity> blogs_Categories { get; set; }
        public DbSet<Model.GiamKichSan.Models.Blogs.Tag_Entity> blogs_Tags { get; set; }


        //MyWeddings
        public DbSet<Model.GiamKichSan.Models.MyWeddings.Wedding_Entity> myWeddings_Weddings { get; set; }
        public DbSet<Model.GiamKichSan.Models.MyWeddings.WeddingCouple_Entity> myWeddings_WeddingCouples { get; set; }
        public DbSet<Model.GiamKichSan.Models.MyWeddings.Suggestion_Entity> myWeddings_Suggestions { get; set; }
        public DbSet<Model.GiamKichSan.Models.MyWeddings.Blog_Entity> myWeddings_Blogs { get; set; }
        public DbSet<Model.GiamKichSan.Models.MyWeddings.Event_Entity> myWeddings_Events { get; set; }
        public DbSet<Model.GiamKichSan.Models.MyWeddings.FileUpload_Entity> myWeddings_FileUploads { get; set; }
        public DbSet<Model.GiamKichSan.Models.MyWeddings.WeddingVideo_Entity> myWeddings_WeddingVideos { get; set; }
        public DbSet<Model.GiamKichSan.Models.MyWeddings.Category_Entity> myWeddings_Categories { get; set; }
        public DbSet<Model.GiamKichSan.Models.MyWeddings.GuestConnect_Entity> myWeddings_GuestConnects { get; set; }
    }
}
