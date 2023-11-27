using API.GiamKichSan.Common;
using API.GiamKichSan.Models.Blogs;
using Microsoft.AspNetCore.Mvc;
using Model.GiamKichSan.Data.Blogs;
using Model.GiamKichSan.Models;
using Model.GiamKichSan.Models.Blogs;
using System.Collections.Generic;

namespace API.GiamKichSan.Controllers.Blogs
{
    [ApiController]
    [Route("Blogs/[controller]")]
    public class BlogController : ControllerBase
    {
        private BlogRepository itemRepository { get; set; }
        public BlogController()
        {
            itemRepository = new BlogRepository(SessionGlobal.DefaultConnectString);
        }

        [HttpGet]
        [Route("GetAll")]
        public ResObject<Blog_Entity> GetAll()
        {
            return itemRepository.GetAll();
        }

        [HttpGet]
        [Route("GetByID")]
        public ResObject<Blog_Entity> GetByID(long ID)
        {
            return itemRepository.GetByID(ID);
        }

        [HttpPost]
        [Route("Insert")]
        public ResObject<Blog_Entity> Insert(Blog_Insert item)
        {
            var resOutput = new Blog_Entity();
            resOutput.DateShow = item.DateShow.ToString(CommonFormat.FormatDateTime);
            resOutput.Title = item.Title;
            resOutput.Description = item.Description;
            resOutput.ImageAvatar = item.ImageAvatar;
            resOutput.IDCreate = SessionGlobal.IDUserLogin;

            var resDeltail = new List<BlogDetail_Entity>();
            var orderNumber = 1;
            while (item.BlogDetail.Length > 0)
            {
                resDeltail.Add(new BlogDetail_Entity
                {
                    OrderNumber = orderNumber,
                    IDCreate = SessionGlobal.IDUserLogin,
                    Description = item.BlogDetail.Length > 4000? item.BlogDetail.Substring(0,4000): item.BlogDetail
                });
                orderNumber++;
                item.BlogDetail = item.BlogDetail.Substring(resDeltail[resDeltail.Count-1].Description.Length);
            }

            return itemRepository.Insert(resOutput, resDeltail);
        }

        [HttpPut]
        [Route("Edit")]
        public ResObject<Blog_Entity> Edit(Blog_Edit item)
        {
            var resOutput = new Blog_Entity();
            resOutput.DateShow = item.DateShow.ToString(CommonFormat.FormatDateTime);
            resOutput.Title = item.Title;
            resOutput.Description = item.Description;
            resOutput.ImageAvatar = item.ImageAvatar;
            resOutput.IDCreate = SessionGlobal.IDUserLogin;
            resOutput.ID = item.ID;

            var resDeltail = new List<BlogDetail_Entity>();
            var orderNumber = 1;
            while (item.BlogDetail.Length > 0)
            {
                resDeltail.Add(new BlogDetail_Entity
                {
                    OrderNumber = orderNumber,
                    IDCreate = SessionGlobal.IDUserLogin,
                    Description = item.BlogDetail.Substring(0, 4000)
                });
                orderNumber++;
                item.BlogDetail = item.BlogDetail.Substring(4000);
            }

            return itemRepository.Edit(resOutput, resDeltail);
        }

        [HttpPut]
        [Route("Active")]
        public ResObject<bool> Active(long id)
        {
            Blog_Entity obj = new Blog_Entity();
            obj.ID = id;
            obj.IDUpdate = SessionGlobal.IDUserLogin;
            return itemRepository.Active(obj);
        }

        [HttpPut]
        [Route("DeActive")]
        public ResObject<bool> DeActive(long id)
        {
            Blog_Entity obj = new Blog_Entity();
            obj.ID = id;
            obj.IDUpdate = SessionGlobal.IDUserLogin;
            return itemRepository.DeActive(obj);
        }

        [HttpDelete]
        [Route("Delete")]
        public ResObject<bool> Delete(int id)
        {
            Blog_Entity obj = new Blog_Entity();
            obj.ID = id;
            obj.IDUpdate = SessionGlobal.IDUserLogin;
            return itemRepository.Delete(obj);
        }

        [HttpDelete]
        [Route("UnDelete")]
        public ResObject<bool> UnDelete(int id)
        {
            Blog_Entity obj = new Blog_Entity();
            obj.ID = id;
            obj.IDUpdate = SessionGlobal.IDUserLogin;
            return itemRepository.UnDelete(obj);
        }
    }
}
