using Model.GiamKichSan.Common;
using Model.GiamKichSan.Common.SQL;
using Model.GiamKichSan.Models;
using Model.GiamKichSan.Models.Blogs;
using System;
using System.Linq.Expressions;

namespace Model.GiamKichSan.Data.Blogs
{
    public class BlogDetailRepository : SqlBaseRepository<BlogDetail_Entity>
    {
        public BlogDetailRepository(BaseSQLConnection baseSQLConnection) : base(baseSQLConnection)
        {
        }
        public ResObject<BlogDetail_Entity> GetAll(Expression<Func<BlogDetail_Entity, bool>> func = null)
        {
            return base.GetByFilter<BlogDetail_Entity>(func, nameof(BlogDetail_Entity.ID), int.MaxValue);
        }
        public ResObject<BlogDetail_Entity> GetByID(long ID)
        {
            return base.GetById<BlogDetail_Entity>(x => x.ID.Equals(ID));
        }
        public ResObject<BlogDetail_Entity> Insert(BlogDetail_Entity item)
        {
            item.DateCreate = DateTime.Now.ToString(CustEnum.FormatDateTime);
            item.DateUpdate = DateTime.Now.ToString(CustEnum.FormatDateTime);
            return base.Insert(item);
        }
    }
}
