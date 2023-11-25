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
    public class CategoryController : ControllerBase
    {
        private CategoryRepository itemRepository { get; set; }
        public CategoryController()
        {
            itemRepository = new CategoryRepository(SessionGlobal.DefaultConnectString);
        }

        [HttpGet]
        [Route("GetAll")]
        public ResObject<Category_Entity> GetAll()
        {
            return itemRepository.GetAll();
        }

        [HttpGet]
        [Route("GetByID")]
        public ResObject<Category_Entity> GetByID(long ID)
        {
            return itemRepository.GetByID(ID);
        }

        [HttpPost]
        [Route("Insert")]
        public ResObject<Category_Entity> Insert(Category_Insert item)
        {
            Category_Entity obj = new Category_Entity();

            obj.IDParent = item.IDParent;
            obj.Name = item.Name;
            obj.IDCreate = SessionGlobal.IDUserLogin;
            return itemRepository.Insert(obj);
        }

        [HttpPut]
        [Route("Edit")]
        public ResObject<Category_Entity> Edit(Category_Edit item)
        {
            var resOutput = new ResObject<Category_Entity>() { obj = new Category_Entity() };
            if (string.IsNullOrWhiteSpace(item.Name))
            {
                resOutput.codeError = "01";
                resOutput.strError = "Tên danh mục chưa được nhập";
                return resOutput;
            }
            resOutput.obj.IDParent = item.IDParent;
            resOutput.obj.Name = item.Name;
            resOutput.obj.ID = item.ID;
            resOutput.obj.IDUpdate = SessionGlobal.IDUserLogin;

            return itemRepository.Edit(resOutput.obj);
        }

        [HttpPut]
        [Route("Active")]
        public ResObject<bool> Active(long id)
        {
            Category_Entity obj = new Category_Entity();
            obj.ID = id;
            obj.IDUpdate = SessionGlobal.IDUserLogin;
            return itemRepository.Active(obj);
        }

        [HttpPut]
        [Route("DeActive")]
        public ResObject<bool> DeActive(long id)
        {
            Category_Entity obj = new Category_Entity();
            obj.ID = id;
            obj.IDUpdate = SessionGlobal.IDUserLogin;
            return itemRepository.DeActive(obj);
        }

        [HttpDelete]
        [Route("Delete")]
        public ResObject<bool> Delete(int id)
        {
            Category_Entity obj = new Category_Entity();
            obj.ID = id;
            obj.IDUpdate = SessionGlobal.IDUserLogin;
            return itemRepository.Delete(obj);
        }

        [HttpDelete]
        [Route("UnDelete")]
        public ResObject<bool> UnDelete(int id)
        {
            Category_Entity obj = new Category_Entity();
            obj.ID = id;
            obj.IDUpdate = SessionGlobal.IDUserLogin;
            return itemRepository.UnDelete(obj);
        }
    }
}
