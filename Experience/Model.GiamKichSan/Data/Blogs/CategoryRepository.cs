using Model.GiamKichSan.Common;
using Model.GiamKichSan.Common.SQL;
using Model.GiamKichSan.Models;
using Model.GiamKichSan.Models.Blogs;
using System;
using System.Linq.Expressions;

namespace Model.GiamKichSan.Data.Blogs
{
    public class CategoryRepository : SqlBaseRepository<Category_Entity>
    {
        public CategoryRepository(BaseSQLConnection baseSQLConnection) : base(baseSQLConnection)
        {
        }

        public ResObject<bool> Delete(Category_Entity item)
        {
            var resOutput = new ResObject<bool>();
            var resItem = base.GetById<Category_Entity>(x => x.ID.Equals(item.ID));
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
        public ResObject<bool> UnDelete(Category_Entity item)
        {
            var resOutput = new ResObject<bool>();
            var resItem = base.GetById<Category_Entity>(x => x.ID.Equals(item.ID));
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
        public ResObject<bool> Active(Category_Entity item)
        {
            var resOutput = new ResObject<bool>();
            var resItem = base.GetById<Category_Entity>(x => x.ID.Equals(item.ID));
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
        public ResObject<bool> DeActive(Category_Entity item)
        {
            var resOutput = new ResObject<bool>();
            var resItem = base.GetById<Category_Entity>(x => x.ID.Equals(item.ID));
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
        public ResObject<Category_Entity> Edit(Category_Entity item)
        {
            var resOutput = new ResObject<Category_Entity>();
            var resItem = base.GetById<Category_Entity>(x => x.ID.Equals(item.ID));
            if (resItem != null && resItem.isValidate() && resItem.obj != null && resItem.obj.ID > 0)
            {
                resItem.obj.DateUpdate = DateTime.Now.ToString(CustEnum.FormatDateTime);
                resItem.obj.IDParent = item.IDParent;
                resItem.obj.Name = item.Name;
                resItem.obj.IDUpdate = item.IDUpdate;
                resOutput = base.Update(resItem.obj, x => x.ID.Equals(resItem.obj.ID));
            }
            else
            {
                resOutput.codeError = "01";
                resOutput.strError = "Không tìm thấy bản ghi để chỉnh sửa";
            }
            return resOutput;
        }
        public ResObject<Category_Entity> GetAll(Expression<Func<Category_Entity, bool>> func = null)
        {
            return base.GetByFilter<Category_Entity>(func, nameof(Category_Entity.Name), int.MaxValue);
        }
        public ResObject<Category_Entity> GetByID(long ID)
        {
            return base.GetById<Category_Entity>(x => x.ID.Equals(ID));
        }
        public ResObject<Category_Entity> Insert(Category_Entity item)
        {
            item.DateCreate = DateTime.Now.ToString(CustEnum.FormatDateTime);
            item.IsActive = CustEnum.IsActive;
            item.IsDelete = CustEnum.IsNotDelete;
            return base.Insert(item);
        }
    }
}
