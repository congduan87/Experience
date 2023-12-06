using Model.GiamKichSan.Common;
using Model.GiamKichSan.Common.SQL;
using Model.GiamKichSan.Models;
using Model.GiamKichSan.Models.Blogs;
using System;
using System.Linq.Expressions;

namespace Model.GiamKichSan.Data.Blogs
{
    public class BlogCategoryRepository : SqlBaseRepository<BlogCategory_Entity>
    {
        public BlogCategoryRepository(BaseSQLConnection baseSQLConnection) : base(baseSQLConnection)
        {
        }
        public ResObject<BlogCategory_Entity> GetAllByIDBlog(long IDBlog)
        {
            return base.GetByFilter<BlogCategory_Entity>(x => x.IDBlog.Equals(IDBlog), nameof(BlogCategory_Entity.ID));
        }
        public ResObject<BlogCategory_Entity> GetByID(long ID)
        {
            return base.GetById<BlogCategory_Entity>(x => x.ID.Equals(ID));
        }
        public ResObject<BlogCategory_Entity> Update(BlogCategory_Entity item)
        {
            item.DateCreate = DateTime.Now.ToString(CustEnum.FormatDateTime);           
            var resItem = base.GetById<BlogCategory_Entity>(x => x.IDCategory.Equals(item.IDCategory) && x.IDBlog.Equals(item.IDBlog));
            if (resItem != null && resItem.isValidate() && (resItem.obj == null || resItem.obj.ID == 0))
            {
                return base.Insert(item);
            }
            return resItem;
        }
    }
}
