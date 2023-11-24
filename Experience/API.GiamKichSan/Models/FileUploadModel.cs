using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace API.GiamKichSan.Models
{
    public class FileUploadModel_Insert
    {
        public long IDParent { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile file { get; set; }
    }
    public class FileUploadModel_Edit : FileUploadModel_Insert
    {
        public long ID { get; set; }
    }
}
