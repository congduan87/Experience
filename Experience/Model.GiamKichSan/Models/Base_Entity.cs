using System.ComponentModel.DataAnnotations;

namespace Model.GiamKichSan.Models
{
    public class Base_Entity
    {
        [StringLength(450)]
        public string IDCreate { get; set; }
        [StringLength(20)]
        public string DateCreate { get; set; }
        [StringLength(450)]
        public string IDUpdate { get; set; }
        [StringLength(20)]
        public string DateUpdate { get; set; }
    }
}
