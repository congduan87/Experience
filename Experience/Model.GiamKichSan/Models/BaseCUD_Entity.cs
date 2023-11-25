using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.GiamKichSan.Models
{
    public class BaseCUD_Entity: Base_Entity
    {
        [StringLength(1)]
        public string IsActive { get; set; }
        [StringLength(1)]
        public string IsDelete { get; set; }
    }
}
