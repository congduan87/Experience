using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.GiamKichSan.Models.MyIPs
{
    [Table("MyIP_HomeAddress")]
    public partial class HomeAddress_Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [StringLength(50)]
        public string IPAddress { get; set; }

        public DateTime? DateCreate { get; set; }

        public bool? IsActive { get; set; }
    }
}
