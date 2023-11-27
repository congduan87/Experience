using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.GiamKichSan.Models.Accounts
{
    [Table("DocumentUpload")]
    public partial class DocumentUpload_Entity: BaseCUD_Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public long IDParent { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        public byte LevelChild { get; set; }
        [StringLength(20)]
        public string Type { get; set; }
        [StringLength(500)]
        public string Path { get; set; }
    }
}
