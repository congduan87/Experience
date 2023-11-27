using Model.GiamKichSan.Common;
using Model.GiamKichSan.Common.SQL;
using Model.GiamKichSan.Models;
using Model.GiamKichSan.Models.Blogs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Model.GiamKichSan.Data.Blogs
{
    public class BlogRepository : SqlBaseRepository<Blog_Entity>
    {
        private BlogDetailRepository blogDetailRepository { get; set; }
        public BlogRepository(string connectionString) : base(connectionString)
        {
            blogDetailRepository = new BlogDetailRepository(connectionString);
        }

        public ResObject<bool> Delete(Blog_Entity item)
        {
            var resOutput = new ResObject<bool>();
            var resItem = base.GetById<Blog_Entity>(x => x.ID.Equals(item.ID));
            if (resItem != null && resItem.isValidate() && resItem.obj != null && resItem.obj.ID > 0)
            {
                resOutput = base.UpdateWhere(new
                {
                    IsDelete = CustEnum.IsDelete,
                    DateUpdate = DateTime.Now.ToString(CustEnum.FormatDateTime),
                    IDUpdate = item.IDUpdate,
                    ID = item.ID,
                }, x => new { ID = resItem.obj.ID });
            }
            else
            {
                resOutput.codeError = "01";
                resOutput.strError = "Không tìm thấy bản ghi để xóa";
            }
            return resOutput;
        }
        public ResObject<bool> UnDelete(Blog_Entity item)
        {
            var resOutput = new ResObject<bool>();
            var resItem = base.GetById<Blog_Entity>(x => x.ID.Equals(item.ID));
            if (resItem != null && resItem.isValidate() && resItem.obj != null && resItem.obj.ID > 0)
            {
                resOutput = base.UpdateWhere(new
                {
                    IsDelete = CustEnum.IsNotDelete,
                    DateUpdate = DateTime.Now.ToString(CustEnum.FormatDateTime),
                    IDUpdate = item.IDUpdate,
                    ID = item.ID,
                }, x => new { ID = resItem.obj.ID });
            }
            else
            {
                resOutput.codeError = "01";
                resOutput.strError = "Không tìm thấy bản ghi để xóa";
            }
            return resOutput;
        }
        public ResObject<bool> Active(Blog_Entity item)
        {
            var resOutput = new ResObject<bool>();
            var resItem = base.GetById<Blog_Entity>(x => x.ID.Equals(item.ID));
            if (resItem != null && resItem.isValidate() && resItem.obj != null && resItem.obj.ID > 0)
            {
                resOutput = base.UpdateWhere(new
                {
                    IsActive = CustEnum.IsActive,
                    DateUpdate = DateTime.Now.ToString(CustEnum.FormatDateTime),
                    IDUpdate = item.IDUpdate,
                    ID = item.ID,
                }, x => new { ID = resItem.obj.ID });
            }
            else
            {
                resOutput.codeError = "01";
                resOutput.strError = "Không tìm thấy bản ghi để Active";
            }
            return resOutput;
        }
        public ResObject<bool> DeActive(Blog_Entity item)
        {
            var resOutput = new ResObject<bool>();
            var resItem = base.GetById<Blog_Entity>(x => x.ID.Equals(item.ID));
            if (resItem != null && resItem.isValidate() && resItem.obj != null && resItem.obj.ID > 0)
            {
                resOutput = base.UpdateWhere(new
                {
                    IsActive = CustEnum.IsNotActive,
                    DateUpdate = DateTime.Now.ToString(CustEnum.FormatDateTime),
                    IDUpdate = item.IDUpdate,
                    ID = item.ID,
                }, x => new { ID = resItem.obj.ID });
            }
            else
            {
                resOutput.codeError = "01";
                resOutput.strError = "Không tìm thấy bản ghi để DeActive";
            }
            return resOutput;
        }
        public ResObject<Blog_Entity> Edit(Blog_Entity item, List<BlogDetail_Entity> details)
        {
            var resOutput = new ResObject<Blog_Entity>();
            var resItem = base.GetById<Blog_Entity>(x => x.ID.Equals(item.ID));
            if (resItem != null && resItem.isValidate() && resItem.obj != null && resItem.obj.ID > 0)
            {
                item.DateCreate = DateTime.Now.ToString(CustEnum.FormatDateTime);
                item.IsActive = CustEnum.IsActive;
                item.IsDelete = CustEnum.IsNotDelete;
                item.Version = resItem.obj.Version + 1;

                resOutput = base.Insert(item);
                if (resOutput.isValidate() && resOutput.obj.ID > 0)
                {
                    var resDetail = new ResObject<BlogDetail_Entity>();
                    foreach (var item1 in details)
                    {
                        item1.IDBlog = resOutput.obj.ID;
                        item1.DateCreate = DateTime.Now.ToString(CustEnum.FormatDateTime);
                        item1.Version = resOutput.obj.Version;

                        resDetail = blogDetailRepository.Insert(item1);
                        if (!resDetail.isValidate())
                        {
                            resOutput.codeError = resDetail.codeError;
                            resOutput.strError = resDetail.strError;
                            return resOutput;
                        }
                    }
                }
                return resOutput;
            }
            else
            {
                resOutput.codeError = "01";
                resOutput.strError = "Không tìm thấy bản ghi để chỉnh sửa";
            }
            return resOutput;
        }
        public ResObject<Blog_Entity> GetAll(Expression<Func<Blog_Entity, bool>> func = null)
        {
            var strQuerry = $@"SELECT A.* FROM {base.GetTableName()} A
                INNER JOIN (SELECT ID, MAX([VERSION]) AS [VERSION] FROM {base.GetTableName()} GROUP BY ID) B
                ON A.ID = B.ID";
            base.GetDataTable(strQuerry, new object[] { }).ToList<Blog_Entity>();
            return base.GetByFilter<Blog_Entity>(func, nameof(Blog_Entity.ID), int.MaxValue);
        }
        public ResObject<Blog_Entity> GetByID(long ID)
        {
            return base.GetById<Blog_Entity>(x => x.ID.Equals(ID));
        }
        public ResObject<Blog_Entity> Insert(Blog_Entity item, List<BlogDetail_Entity> details)
        {
            item.DateCreate = DateTime.Now.ToString(CustEnum.FormatDateTime);
            item.IsActive = CustEnum.IsActive;
            item.IsDelete = CustEnum.IsNotDelete;
            item.Version = 1;
            var resOutput = base.Insert(item);
            if (resOutput.isValidate() && resOutput.obj.ID > 0)
            {
                var resDetail = new ResObject<BlogDetail_Entity>();
                foreach (var item1 in details)
                {
                    item1.IDBlog = resOutput.obj.ID;
                    item1.DateCreate = DateTime.Now.ToString(CustEnum.FormatDateTime);
                    item1.Version = resOutput.obj.Version;

                    resDetail = blogDetailRepository.Insert(item1);
                    if (!resDetail.isValidate())
                    {
                        resOutput.codeError = resDetail.codeError;
                        resOutput.strError = resDetail.strError;
                        return resOutput;
                    }
                }
            }
            return resOutput;
        }
    }
}
