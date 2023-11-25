using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.GiamKichSan.Models.Blogs
{
    [Table("Blogs_BlogComment")]
    public class BlogComment_Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public long IDBlog { get; set; }
        [StringLength(50)]
        public string ReaderEmail { get; set; }
        [StringLength(50)]
        public string ReaderName { get; set; }
        [StringLength(4000)]
        public string ReaderComment { get; set; }
        [StringLength(20)]
        public string ReaderDate { get; set; }
    }
}