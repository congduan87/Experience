using API.GiamKichSan.Common;
using API.GiamKichSan.Models.Blogs;
using Microsoft.AspNetCore.Mvc;
using Model.GiamKichSan.Data.Blogs;
using Model.GiamKichSan.Models;
using Model.GiamKichSan.Models.Blogs;

namespace API.GiamKichSan.Controllers.Blogs
{
    [ApiController]
    [Route("Blogs/[controller]")]
    public class TagController : ControllerBase
    {
        private TagRepository itemRepository { get; set; }
        public TagController()
        {
            itemRepository = new TagRepository(SessionGlobal.DefaultConnectString);
        }

        [HttpGet]
        [Route("GetAll")]
        public ResObject<Tag_Entity> GetAll()
        {
            return itemRepository.GetAll();
        }

        [HttpGet]
        [Route("GetByID")]
        public ResObject<Tag_Entity> GetByID(long ID)
        {
            return itemRepository.GetByID(ID);
        }

        [HttpPost]
        [Route("Insert")]
        public ResObject<Tag_Entity> Insert(Tag_Insert item)
        {
            var resOutput = new ResObject<Tag_Entity>() { obj = new Tag_Entity() };
            if (string.IsNullOrWhiteSpace(item.Name))
            {
                resOutput.codeError = "01";
                resOutput.strError = "Tên tag chưa được nhập";
                return resOutput;
            }

            resOutput.obj.Name = item.Name;
            resOutput.obj.IDCreate = SessionGlobal.IDUserLogin;
            resOutput = itemRepository.Insert(resOutput.obj);

            return resOutput;
        }

        [HttpPut]
        [Route("Edit")]
        public ResObject<Tag_Entity> Edit(Tag_Edit item)
        {
            var resOutput = new ResObject<Tag_Entity>() { obj = new Tag_Entity() };
            if (string.IsNullOrWhiteSpace(item.Name))
            {
                resOutput.codeError = "01";
                resOutput.strError = "Tên tag chưa được nhập";
                return resOutput;
            }
            resOutput.obj.Name = item.Name;
            resOutput.obj.ID = item.ID;
            resOutput.obj.IDUpdate = SessionGlobal.IDUserLogin;

            return itemRepository.Edit(resOutput.obj);
        }

        [HttpPut]
        [Route("Active")]
        public ResObject<bool> Active(long id)
        {
            Tag_Entity obj = new Tag_Entity();
            obj.ID = id;
            obj.IDUpdate = SessionGlobal.IDUserLogin;
            return itemRepository.Active(obj);
        }

        [HttpPut]
        [Route("DeActive")]
        public ResObject<bool> DeActive(long id)
        {
            Tag_Entity obj = new Tag_Entity();
            obj.ID = id;
            obj.IDUpdate = SessionGlobal.IDUserLogin;
            return itemRepository.DeActive(obj);
        }

        [HttpDelete]
        [Route("Delete")]
        public ResObject<bool> Delete(int id)
        {
            Tag_Entity obj = new Tag_Entity();
            obj.ID = id;
            obj.IDUpdate = SessionGlobal.IDUserLogin;
            return itemRepository.Delete(obj);
        }

        [HttpDelete]
        [Route("UnDelete")]
        public ResObject<bool> UnDelete(int id)
        {
            Tag_Entity obj = new Tag_Entity();
            obj.ID = id;
            obj.IDUpdate = SessionGlobal.IDUserLogin;
            return itemRepository.UnDelete(obj);
        }
    }
}
