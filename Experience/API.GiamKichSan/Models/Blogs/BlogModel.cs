using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace API.GiamKichSan.Models.Blogs
{
    public class BlogModel
    {
    }
    public class Blog_Insert
    {
        [Display(Name = "Ngày đăng")]
        public DateTime DateShow { get; set; }
        [StringLength(200)]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }
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
