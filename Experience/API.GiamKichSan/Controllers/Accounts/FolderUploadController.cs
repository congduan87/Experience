using API.GiamKichSan.Common;
using API.GiamKichSan.Models;
using Microsoft.AspNetCore.Mvc;
using Model.GiamKichSan.Data.Accounts;
using Model.GiamKichSan.Models;
using Model.GiamKichSan.Models.Accounts;
using System;

namespace API.GiamKichSan.Controllers.Accounts
{
    [ApiController]
    [Route("Accounts/[controller]")]
    public class FolderUploadController : ControllerBase
    {
        private DocumentUploadRepository controllerRepository { get; set; }
        public FolderUploadController()
        {
            controllerRepository = new DocumentUploadRepository(SessionGlobal.DefaultConnectString);
        }

        [HttpGet]
        [Route("GetAll")]
        public ResObject<DocumentUpload_Entity> GetAll(string IDAccount)
        {
            return controllerRepository.GetAll(x => x.Type.Equals(CustEnum.TypeUpload.Folder.ToString()) && x.IDCreate.Equals(IDAccount));
        }

        [HttpGet]
        [Route("GetByIsUse")]
        public ResObject<DocumentUpload_Entity> GetByIsUse(string IDAccount, string IsUse)
        {
            return controllerRepository.GetAll(x => x.Type.Equals(CustEnum.TypeUpload.Folder.ToString()) && x.IDCreate.Equals(IDAccount) && x.IsActive.Equals(IsUse) && x.IsDelete.Equals("N"));
        }

        [HttpGet()]
        [Route("GetByID")]
        public ResObject<DocumentUpload_Entity> GetByID(string IDAccount, int id)
        {
            return controllerRepository.GetByID(id, IDAccount);
        }

        [HttpPost]
        [Route("Insert")]
        public ResObject<DocumentUpload_Entity> Insert(FolderUploadModel_Insert obj)
        {
            var item = new DocumentUpload_Entity();
            item.Name = obj.Name;
            item.IDParent = obj.IDParent;
            item.Type = CustEnum.TypeUpload.Folder.ToString();
            item.IDCreate = SessionGlobal.IDUserLogin;
            item.DateCreate = DateTime.Now.ToString(CommonFormat.FormatDateTime);
            item.IsActive = "Y";
            item.IsDelete = "N";
            if (obj.IDParent == 0)
                item.LevelChild = 1;
            else
                item.LevelChild = controllerRepository.GetLevelChildByID(obj.IDParent, SessionGlobal.IDUserLogin);

            return controllerRepository.Insert(item);
        }

        [HttpPut]
        [Route("Edit")]
        public ResObject<DocumentUpload_Entity> Edit(FolderUploadModel_Edit obj)
        {
            var res = controllerRepository.GetByID(obj.ID, SessionGlobal.IDUserLogin);
            if (res.isValidate() && res.obj != null && res.obj is DocumentUpload_Entity)
            {
                var folder = res.obj as DocumentUpload_Entity;
                folder.Name = obj.Name;
                folder.IDParent = obj.IDParent;
                if (obj.IDParent == 0)
                    folder.LevelChild = 1;
                else
                    folder.LevelChild = controllerRepository.GetLevelChildByID(obj.IDParent, SessionGlobal.IDUserLogin);

                return controllerRepository.Edit(folder);
            }
            return res;
        }

        [HttpPut()]
        [Route("Active")]
        public ResObject<DocumentUpload_Entity> Active(long id, string isUse)
        {
            var res = controllerRepository.GetByID(id, SessionGlobal.IDUserLogin);
            if (res.isValidate() && res.obj != null)
            {
                var item = res.obj as DocumentUpload_Entity;
                item.IsActive = isUse;
                return controllerRepository.Edit(item);
            }
            return res;
        }

        [HttpDelete]
        [Route("Delete")]
        public ResObject<DocumentUpload_Entity> Delete(int id)
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
