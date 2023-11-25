using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.GiamKichSan.Models.Blogs
{
    [Table("Blogs_Blog")]
    public class Blog_Entity : BaseCUD_Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        [StringLength(20)]
        public string DateShow { get; set; }
        [StringLength(200)]
        public string Title { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        public int Version { get; set; }
        [StringLength(200)]
        public string ImageAvatar { get; set; }
    }
}