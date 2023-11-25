using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.GiamKichSan.Models
{
    public class HomeAddress_Entity
    {
        public long ID { get; set; }
        public string IPAddress { get; set; }
        public DateTime? DateCreate { get; set; }
        public bool? IsActive { get; set; }
    }
}
