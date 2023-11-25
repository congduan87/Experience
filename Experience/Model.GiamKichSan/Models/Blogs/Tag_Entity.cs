using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.GiamKichSan.Models.Blogs
{
    [Table("Blogs_Tag")]
    public class Tag_Entity : BaseCUD_Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        [StringLength(30)]
        public string Name { get; set; }
    }
}
