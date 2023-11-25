using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.GiamKichSan.Models.Blogs
{
    [Table("Blogs_Category")]
    public class Category_Entity : BaseCUD_Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public long IDParent { get; set; }
        [StringLength(300)]
        public string Name { get; set; }
    }
}