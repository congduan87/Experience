using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.GiamKichSan.Models.Blogs
{
    public class CategoryModel
    {
    }
    public class Category_Insert
    {
        [Display(Name = "Mã danh mục cha")]
        public long IDParent { get; set; }
        [StringLength(300)]
        [Required]
        [Display(Name="Tên danh mục")]
        public string Name { get; set; }
    }
    public class Category_Edit: Category_Insert
    {
        [Display(Name = "ID danh mục")]
        public long ID { get; set; }
    }
}
