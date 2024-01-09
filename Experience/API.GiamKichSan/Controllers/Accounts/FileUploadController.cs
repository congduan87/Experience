using API.GiamKichSan.Common;
using API.GiamKichSan.Models;
using Microsoft.AspNetCore.Mvc;
using Model.GiamKichSan.Common.SQL;
using Model.GiamKichSan.Data.Accounts;
using Model.GiamKichSan.Models;
using Model.GiamKichSan.Models.Accounts;
using System;
using System.IO;
using System.Threading.Tasks;

namespace API.GiamKichSan.Controllers.Accounts
{
    [ApiController]
    [Route("Accounts/[controller]")]
    public class FileUploadController : ControllerBase
    {
        private DocumentUploadRepository controllerRepository { get; set; }
        public FileUploadController(BaseSQLConnection baseSQLConnection)
        {
            controllerRepository = new DocumentUploadRepository(baseSQLConnection);
        }

        [HttpGet]
        [Route("GetFilesByIDFolder")]
        public ResObject<DocumentUpload_Entity> GetFilesByIDFolder(long IDFolder)
        {
            return controllerRepository.GetAll(x => !x.Type.Equals(CustEnum.TypeUpload.Folder.ToString()) && x.IDParent.Equals(IDFolder));
        }

        [HttpGet]
        [Route("GetFilesByIDFolder/{IsUse}")]
        public ResObject<DocumentUpload_Entity> GetFilesByIDFolder(long IDFolder, string IsUse)
        {
            return controllerRepository.GetAll(x => !x.Type.Equals(CustEnum.TypeUpload.Folder.ToString()) && x.IDParent.Equals(IDFolder) && x.IsActive.Equals(IsUse));
        }

        [HttpGet("{id}")]
        public ResObject<DocumentUpload_Entity> GetByID(int id)
        {
            return controllerRepository.GetByID(id, SessionGlobal.IDUserLogin);
        }

        [HttpPost]
        [Route("Insert")]
        public async Task<ResObject<DocumentUpload_Entity>> Insert([FromForm] FileUploadModel_Insert item)
        {
            var resOutput = new ResObject<DocumentUpload_Entity>();
            if (item.file != null)
            {
                DocumentUpload_Entity obj = new DocumentUpload_Entity();
                var fileInfo = new FileInfo(item.file.FileName);
                obj.Type = fileInfo.Extension;
                obj.Path = DateTime.Now.ToString("yyyyMMdd");
                if (!Directory.Exists(Path.Combine(UploadHelper.RootImage(), obj.Path)))
                {
                    Directory.CreateDirectory(Path.Combine(UploadHelper.RootImage(), obj.Path));
                }
                obj.Path = Path.Combine(obj.Path, Guid.NewGuid().ToString() + obj.Type);

                using (var fileStream = new FileStream(Path.Combine(UploadHelper.RootImage(), obj.Path), FileMode.Create))
                {
                    await item.file.CopyToAsync(fileStream);
                }
                if (string.IsNullOrWhiteSpace(item.Name))
                {
                    item.Name = item.file.FileName;
                }

                obj.IDParent = item.IDParent;
                obj.Name = item.Name;
                obj.LevelChild = 0;
                obj.IDCreate = SessionGlobal.IDUserLogin;
                obj.DateCreate = DateTime.Now.ToString(CommonFormat.FormatDateTime);
                obj.IsActive = "Y";
                obj.IsDelete = "N";

                return controllerRepository.Insert(obj);
            }
            return resOutput;
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<ResObject<DocumentUpload_Entity>> Edit([FromForm] FileUploadModel_Edit item)
        {
            var resOutput = new ResObject<DocumentUpload_Entity>();

            resOutput = controllerRepository.GetByID(item.ID, SessionGlobal.IDUserLogin);
            if (!resOutput.isValidate() || resOutput.obj.ID == 0)
            {
                return resOutput;
            }

            if (item.file != null)
            {
                //if (!string.IsNullOrEmpty(resOutput.obj.Path))
                //{
                //    System.IO.File.Delete(Path.Combine(UploadHelper.RootImage(), resOutput.obj.Path));
                //}
                var fileInfo = new FileInfo(item.file.FileName);
                resOutput.obj.Type = fileInfo.Extension;
                resOutput.obj.Path = Path.Combine(DateTime.Now.ToString("yyyyMMdd"), Guid.NewGuid().ToString() + resOutput.obj.Type);
                using (var fileStream = new FileStream(Path.Combine(UploadHelper.RootImage(), resOutput.obj.Path), FileMode.Create))
                {
                    await item.file.CopyToAsync(fileStream);
                }
                if (string.IsNullOrWhiteSpace(item.Name))
                {
                    item.Name = item.file.FileName;
                }
            }

            resOutput.obj.IDParent = item.IDParent;
            resOutput.obj.Name = item.Name;
            resOutput.obj.LevelChild = 0;

            return controllerRepository.Edit(resOutput.obj);
        }

        [HttpPut("{IsUse}")]
        public ResObject<DocumentUpload_Entity> Active([FromBody] long id, string IsUse)
        {
            var res = controllerRepository.GetByID((int)id, SessionGlobal.IDUserLogin);
            if (res.isValidate() || res.obj == null)
            {
                var item = res.obj as DocumentUpload_Entity;
                item.IsActive = IsUse;
                return controllerRepository.Edit(item);
            }
            return res;
        }
        [HttpDelete]
        [Route("Delete")]
        public ResObject<DocumentUpload_Entity> Delete( int id)
        {
            var res = controllerRepository.GetByID(id, SessionGlobal.IDUserLogin);
            if (res.isValidate() || res.obj == null)
            {
                var item = res.obj as DocumentUpload_Entity;
                item.IsDelete = "Y";
                return controllerRepository.Edit(item);
            }
            return res;
        }
    }
}
