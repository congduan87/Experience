using API.GiamKichSan.Common;
using API.GiamKichSan.Models.Blogs;
using Microsoft.AspNetCore.Mvc;
using Model.GiamKichSan.Common.SQL;
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
        public BlogController(BaseSQLConnection baseSQLConnection)
        {
            itemRepository = new BlogRepository(baseSQLConnection);
        }

        [HttpGet]
        [Route("GetAll")]
        public ResObject<Blog_Entity> GetAll()
        {
            return itemRepository.GetAll();
        }

        [HttpGet]
        [Route("GetAllByIDCategory")]
        public ResObject<Blog_Entity> GetAllByIDCategory(long IDCatetory)
        {
            return itemRepository.GetAllByIDCategory(IDCatetory);
        }

        [HttpGet]
        [Route("GetByID")]
        public ResObject<BlogModel> GetByID(long ID)
        {
            ResObject<BlogModel> output = new ResObject<BlogModel>() { obj = new BlogModel()};
            output.obj.blog = itemRepository.GetByID(ID).obj;
            output.obj.blogCategories = itemRepository.blogCategoryRepository.GetAllByIDBlog(ID).listObj;
            output.obj.blogTags = itemRepository.blogTagRepository.GetAllByIDBlog(ID).listObj;
            output.obj.blogDetails = itemRepository.blogDetailRepository.GetAll(x=>x.IDBlog.Equals(ID)).listObj;
            output.obj.blogComments = new List<BlogComment_Entity>();

            return output;
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
            var resCategory = new List<BlogCategory_Entity>();
            var resTag = new List<BlogTag_Entity>();
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

            if(item.IDCategory != null)
            {
                for (int orderIndex = 0; orderIndex < item.IDCategory.Length; orderIndex++)
                {
                    if(item.IDCategory[orderIndex] != 0)
                    {
                        resCategory.Add(new BlogCategory_Entity()
                        {
                            IDCreate = SessionGlobal.IDUserLogin,
                            IDCategory = item.IDCategory[orderIndex]
                        });
                    }  
                }
            }

            if (item.IDTag != null)
            {
                for (int orderIndex = 0; orderIndex < item.IDTag.Length; orderIndex++)
                {
                    if (item.IDTag[orderIndex] != 0)
                    {
                        resTag.Add(new BlogTag_Entity()
                        {
                            IDCreate = SessionGlobal.IDUserLogin,
                            IDTag = item.IDTag[orderIndex]
                        });
                    }
                }
            }

            return itemRepository.Insert(resOutput, resDeltail, resCategory, resTag);
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
            var resCategory = new List<BlogCategory_Entity>();
            var resTag = new List<BlogTag_Entity>();
            var orderNumber = 1;
            while (item.BlogDetail.Length > 0)
            {
                resDeltail.Add(new BlogDetail_Entity
                {
                    OrderNumber = orderNumber,
                    IDCreate = SessionGlobal.IDUserLogin,
                    Description = item.BlogDetail.Length > 4000 ? item.BlogDetail.Substring(0, 4000) : item.BlogDetail
                });
                orderNumber++;
                item.BlogDetail = item.BlogDetail.Substring(resDeltail[resDeltail.Count - 1].Description.Length);
            }

            if (item.IDCategory != null)
            {
                for (int orderIndex = 0; orderIndex < item.IDCategory.Length; orderIndex++)
                {
                    if (item.IDCategory[orderIndex] != 0)
                    {
                        resCategory.Add(new BlogCategory_Entity()
                        {
                            IDCreate = SessionGlobal.IDUserLogin,
                            IDCategory = item.IDCategory[orderIndex]
                        });
                    }
                }
            }

            if (item.IDTag != null)
            {
                for (int orderIndex = 0; orderIndex < item.IDTag.Length; orderIndex++)
                {
                    if (item.IDTag[orderIndex] != 0)
                    {
                        resTag.Add(new BlogTag_Entity()
                        {
                            IDCreate = SessionGlobal.IDUserLogin,
                            IDTag = item.IDTag[orderIndex]
                        });
                    }
                }
            }

            return itemRepository.Edit(resOutput, resDeltail, resCategory, resTag);
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
