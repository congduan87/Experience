using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWedding.Common;
using MyWedding.Data;
using MyWedding.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace MyWedding.Pages.Admin.FileUpload
{
    public class DetailsModel : CustPageModel
    {
        [BindProperty]
        public FileUploadModel weddingFileUpload { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name = "Chọn file upload")]
        [BindProperty]
        public IFormFile[] FileUploads { get; set; }
        public DetailsModel(ApplicationDbContext context) : base(context)
        {

        }
        public IActionResult OnGet(int ID)
        {
            return IsCheckIDWedding(() =>
            {
                if (ID != 0)
                {
                    weddingFileUpload = (_context.fileUploads.Where(x => x.ID == ID).FirstOrDefault() ?? new Data.FileUpload()).Change();
                }
                else
                {
                    weddingFileUpload = new FileUploadModel();
                }
            });
        }
        public IActionResult OnPost()
        {
            if (weddingFileUpload.ID != 0 || (FileUploads != null && FileUploads.Length > 0))
            {
                try
                {
                    return IsCheckIDWedding(() =>
                    {
                        var update = weddingFileUpload as Data.FileUpload;
                        update.DateUpload = DateTime.Now.ToString("yyyy-MM-dd");
                        if (FileUploads != null && FileUploads.Length > 0)
                        {
                            if (!string.IsNullOrEmpty(weddingFileUpload.Path))
                            {
                                System.IO.File.Delete(Path.Combine(Helper.RootImage(), weddingFileUpload.Path));
                            }

                            update.Path = Guid.NewGuid().ToString() + new FileInfo(FileUploads[0].FileName).Extension;
                            var file = Path.Combine(Helper.RootImage(), update.Path);
                            using (var fileStream = new FileStream(file, FileMode.Create))
                            {
                                FileUploads[0].CopyTo(fileStream);
                            }

                            if (string.IsNullOrWhiteSpace(update.Name))
                                update.Name = FileUploads[0].FileName;
                        }

                        if (weddingFileUpload.ID != 0)
                            _context.fileUploads.Update(update);
                        else
                            _context.fileUploads.Add(update);

                        _context.SaveChanges();

                    }, "/Admin/FileUpload/Index");
                }
                catch
                {

                }
            }
            return Page();
        }
    }
}
