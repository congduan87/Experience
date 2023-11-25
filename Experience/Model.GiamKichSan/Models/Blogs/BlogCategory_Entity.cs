using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.GiamKichSan.Models.Blogs
{
    [Table("Blogs_BlogCategory")]
    public class BlogCategory_Entity : Base_Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public long IDBlog { get; set; }
        public long IDCategory { get; set; }
    }
}