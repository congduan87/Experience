using MyWedding.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyWedding.Models
{
    public class WeddingCoupleModel
    {
        public Int64 IDWedding { get; set; }
        [MaxLength(500)]
        [Display(Name = "Tên chú rể")]
        public string MaleName { get; set; }
        [MaxLength(2000)]
        [Display(Name = "Tính cách")]
        public string MaleDescription { get; set; }
        [MaxLength(500)]
        [Display(Name = "Link Facebook")]
        public string MaleFacebookUrl { get; set; }
        [MaxLength(500)]
        [Display(Name = "Hình ảnh chú rể")]
        public string MaleImage { get; set; }
        [Display(Name = "Số tài khoản")]
        public string MaleIDBankCard { get; set; }
        [MaxLength(500)]
        [Display(Name = "Tên ngân hàng")]
        public string MaleNameBank { get; set; }
        [MaxLength(500)]
        [Display(Name = "Chi nhánh")]
        public string MaleAddressBank { get; set; }
        [MaxLength(500)]
        [Display(Name = "Mã QR")]
        public string MaleImageBank { get; set; }
        [MaxLength(500)]
        [Display(Name = "Tên bố chú rể")]
        public string FatherMaleName { get; set; }
        [MaxLength(500)]
        [Display(Name = "Tên mẹ chú rể")]
        public string MotherMaleName { get; set; }

        [MaxLength(500)]
        [Display(Name = "Tên cô dâu")]
        public string FeMaleName { get; set; }
        [MaxLength(2000)]
        [Display(Name = "Tính cách")]
        public string FeMaleDescription { get; set; }
        [MaxLength(500)]
        [Display(Name = "Link Facebook")]
        public string FeMaleFacebookUrl { get; set; }
        [MaxLength(500)]
        [Display(Name = "Hình ảnh cô dâu")]
        public string FeMaleImage { get; set; }
        [Display(Name = "Số tài khoản")]
        public string FeMaleIDBankCard { get; set; }
        [MaxLength(500)]
        [Display(Name = "Tên ngân hàng")]
        public string FeMaleNameBank { get; set; }
        [MaxLength(500)]
        [Display(Name = "Chi nhánh")]
        public string FeMaleAddressBank { get; set; }
        [MaxLength(500)]
        [Display(Name = "Mã QR")]
        public string FeMaleImageBank { get; set; }
        [MaxLength(500)]
        [Display(Name = "Tên bố cô dâu")]
        public string FatherFeMaleName { get; set; }
        [MaxLength(500)]
        [Display(Name = "Tên mẹ cô dâu")]
        public string MotherFeMaleName { get; set; }

        public WeddingCoupleModel()
        {
            IDWedding = VariableGlobal.IDWedding;
        }
    }
}
