using Model.GiamKichSan.Common;
using Model.GiamKichSan.Common.SQL;
using Model.GiamKichSan.Models;
using Model.GiamKichSan.Models.Blogs;
using System;
using System.Linq.Expressions;

namespace Model.GiamKichSan.Data.Blogs
{
    public class BlogTagRepository : SqlBaseRepository<BlogTag_Entity>
    {
        public BlogTagRepository(BaseSQLConnection baseSQLConnection) : base(baseSQLConnection)
        {
        }
        public ResObject<BlogTag_Entity> GetAllByIDBlog(long IDBlog)
        {
            return base.GetByFilter<BlogTag_Entity>(x => x.IDBlog.Equals(IDBlog), nameof(BlogTag_Entity.ID));
        }
        public ResObject<BlogTag_Entity> GetByID(long ID)
        {
            return base.GetById<BlogTag_Entity>(x => x.ID.Equals(ID));
        }
        public ResObject<BlogTag_Entity> Update(BlogTag_Entity item)
        {
            item.DateCreate = DateTime.Now.ToString(CustEnum.FormatDateTime);
            var resItem = base.GetById<BlogTag_Entity>(x => x.IDTag.Equals(item.IDTag) && x.IDBlog.Equals(item.IDBlog));
            if (resItem != null && resItem.isValidate() && (resItem.obj == null || resItem.obj.ID == 0))
            {
                return base.Insert(item);
            }
            return resItem;
        }
    }
}
