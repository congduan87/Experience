using System.ComponentModel.DataAnnotations;

namespace API.GiamKichSan.Models.Blogs
{
    public class TagModel
    {
    }
    public class Tag_Insert
    {
        [StringLength(30)]
        [Required]
        [Display(Name = "Tên tag")]
        public string Name { get; set; }
    }
    public class Tag_Edit : Tag_Insert
    {
        [Display(Name = "ID")]
        public long ID { get; set; }
    }
}
