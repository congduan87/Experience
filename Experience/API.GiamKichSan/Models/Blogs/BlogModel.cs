using Model.GiamKichSan.Models.Blogs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.GiamKichSan.Models.Blogs
{
    public class BlogModel
    {
        public Blog_Entity blog { get; set; }
        public List<BlogDetail_Entity> blogDetails { get; set; }
        public List<BlogCategory_Entity> blogCategories { get; set; }
        public List<BlogTag_Entity> blogTags { get; set; }
        public List<BlogComment_Entity> blogComments { get; set; }
    }
    public class Blog_Insert
    {
        [Display(Name = "Ngày đăng")]
        public DateTime DateShow { get; set; }
        [StringLength(200)]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }
        [Display(Name = "Danh mục")]
        public long[] IDCategory { get; set; }
        [Display(Name = "Tag")]
        public long[] IDTag { get; set; }
        [StringLength(2000)]
        [Display(Name = "Tóm tắt")]
        public string Description { get; set; }
        [StringLength(200)]
        [Display(Name = "Ảnh đại diện")]
        public string ImageAvatar { get; set; }
        [Display(Name = "Nội dung")]
        public string BlogDetail { get; set; }
    }
    public class Blog_Edit : Blog_Insert
    {
        [Display(Name = "ID")]
        public long ID { get; set; }
    }
}
