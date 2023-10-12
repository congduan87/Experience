using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWedding.Data
{
    [Table(name: "WeddingHappy_Wedding")]
    public class Wedding
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 ID { get; set; }
        [MaxLength(500)]
        [Display(Name = "Tên đám cưới")]
        public string Name { get; set; }
        [MaxLength(22)]
        [Display(Name = "Ngày cưới")]
        public string StartDate { get; set; }
        [MaxLength(256)]
        public string UserName { get; set; }
        [MaxLength(500)]
        [Display(Name = "Tên chú rể ghi thiệp")]
        public string WeddingMale { get; set; }
        [MaxLength(500)]
        [Display(Name = "Tên cô dâu ghi thiệp")]
        public string WeddingFaMale { get; set; }
        [MaxLength(500)]
        [Display(Name = "Trang web cưới")]
        public string Url { get; set; }
        [MaxLength(500)]
        [Display(Name = "Ảnh SEO")]
        public string Image { get; set; }
        [MaxLength(500)]
        [Display(Name = "Ảnh background Home")]
        public string ImageHome { get; set; }
        [MaxLength(500)]
        [Display(Name = "Ảnh background cặp đôi")]
        public string ImageCouple { get; set; }
        [MaxLength(500)]
        [Display(Name = "Ảnh background blog")]
        public string ImageBlog { get; set; }
        [MaxLength(500)]
        [Display(Name = "Ảnh background album cưới")]
        public string ImageAlbum { get; set; }
        [MaxLength(500)]
        [Display(Name = "Ảnh background video cưới")]
        public string ImageVideo { get; set; }
        [MaxLength(500)]
        [Display(Name = "Ảnh background sự kiện")]
        public string ImageEvent { get; set; }
        [MaxLength(500)]
        [Display(Name = "Ảnh background suggestion")]
        public string ImageSuggestion { get; set; }
        [MaxLength(500)]
        [Display(Name = "Ảnh background mừng cưới")]
        public string ImageBenefit { get; set; }
    }

    [Table(name: "WeddingHappy_WeddingCouple")]
    public class WeddingCouple
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 ID { get; set; }
        public Int64 IDWedding { get; set; }
        [MaxLength(500)]
        public string FullName { get; set; }
        [MaxLength(500)]
        public string FacebookUrl { get; set; }
        [MaxLength(500)]
        public string Image { get; set; }
        public bool IsMale { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; }
        public string IDBankCard { get; set; }
        [MaxLength(500)]
        public string NameBank { get; set; }
        [MaxLength(500)]
        public string AddressBank { get; set; }
        [MaxLength(500)]
        public string ImageBank { get; set; }
        [MaxLength(500)]
        public string FatherName { get; set; }
        [MaxLength(500)]
        public string MotherName { get; set; }
    }

    [Table(name: "WeddingHappy_Suggestion")]
    public class Suggestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 ID { get; set; }
        public Int64 IDWedding { get; set; }
        [MaxLength(22)]
        public string CreateDate { get; set; }
        [MaxLength(500)]
        public string Title { get; set; }
        [MaxLength(4000)]
        public string Content { get; set; }
        public bool IsHidden { get; set; }
    }

    [Table(name: "WeddingHappy_Blog")]
    public class Blog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 ID { get; set; }
        public Int64 IDWedding { get; set; }
        [MaxLength(22)]
        [Display(Name = "Ngày đăng")]
        public string CreateDate { get; set; }
        [MaxLength(500)]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }
        [MaxLength(4000)]
        [Display(Name = "Nội dung")]
        public string Content { get; set; }
        [MaxLength(500)]
        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }
        [Display(Name = "Ẩn")]
        public bool IsHidden { get; set; }
    }

    [Table(name: "WeddingHappy_Event")]
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 ID { get; set; }
        public Int64 IDWedding { get; set; }
        [MaxLength(22)]
        [Display(Name = "Ngày giờ tổ chức")]
        public string StartDate { get; set; }
        [MaxLength(500)]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }
        [MaxLength(500)]
        [Display(Name = "Địa chỉ bản đồ")]
        public string MapAddress { get; set; }
        [MaxLength(20)]
        [Display(Name = "Kinh độ")]
        public string Latitude { get; set; }
        [MaxLength(20)]
        [Display(Name = "Vĩ Độ")]
        public string Longitude { get; set; }
        [MaxLength(500)]
        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }
        [MaxLength(500)]
        [Display(Name = "Địa chỉ tổ chức")]
        public string Address { get; set; }
    }

    [Table(name: "WeddingHappy_FileUpload")]
    public class FileUpload
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 ID { get; set; }
        public Int64 IDWedding { get; set; }
        [MaxLength(500)]
        [Display(Name = "Tên ảnh upload")]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Path { get; set; }
        [MaxLength(22)]
        public string DateUpload { get; set; }
        [Display(Name = "Ẩn")]
        public bool IsHidden { get; set; }
    }

    [Table(name: "WeddingHappy_WeddingVideo")]
    public class WeddingVideo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 ID { get; set; }
        public Int64 IDWedding { get; set; }
        [MaxLength(500)]
        [Display(Name = "Tiêu đề video")]
        public string Title { get; set; }
        [MaxLength(2000)]
        [Display(Name = "Nội dung video")]
        public string Content { get; set; }
        [MaxLength(500)]
        [Display(Name = "Đường dẫn file Video")]
        public string Path { get; set; }
        [MaxLength(22)]
        public string DateUpload { get; set; }
        [Display(Name = "Ẩn")]
        public bool IsHidden { get; set; }
    }

    [Table(name: "WeddingHappy_Category")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 ID { get; set; }
        public Int64 IDWedding { get; set; }
        [MaxLength(500)]
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }
        [MaxLength(500)]
        [Display(Name = "Mã danh mục")]
        public string Code { get; set; }
        [MaxLength(500)]
        [Display(Name = "Đường dẫn")]
        public string Path { get; set; }
        [Display(Name = "Ẩn")]
        public bool IsHidden { get; set; }
    }


}
