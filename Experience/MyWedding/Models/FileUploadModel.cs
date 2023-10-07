using MyWedding.Common;
using MyWedding.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWedding.Models
{
    public class FileUploadModel: FileUpload
    {
        [Display(Name = "Tên đường dẫn ảnh")]
        public string FullPath { get; set; }
        public FileUploadModel()
        {
            IDWedding = VariableGlobal.IDWedding;
        }
    }
}
