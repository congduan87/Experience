using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.GiamKichSan.Models.Blogs
{
    [Table("Blogs_BlogDetail")]
    public class BlogDetail_Entity : Base_Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }        
        public long IDBlog { get; set; }
        public int Version { get; set; }
        public int OrderNumber { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
    }
}