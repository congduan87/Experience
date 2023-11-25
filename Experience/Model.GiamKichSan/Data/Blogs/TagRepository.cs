using Model.GiamKichSan.Common;
using Model.GiamKichSan.Common.SQL;
using Model.GiamKichSan.Models;
using Model.GiamKichSan.Models.Blogs;
using System;
using System.Linq.Expressions;

namespace Model.GiamKichSan.Data.Blogs
{
    public class TagRepository : SqlBaseRepository<Tag_Entity>
    {
        public TagRepository(string connectionString) : base(connectionString)
        {
        }

        public ResObject<bool> Delete(Tag_Entity item)
        {
            var resOutput = new ResObject<bool>();
            var resItem = base.GetById<Tag_Entity>(x => x.ID.Equals(item.ID));
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
        public ResObject<bool> UnDelete(Tag_Entity item)
        {
            var resOutput = new ResObject<bool>();
            var resItem = base.GetById<Tag_Entity>(x => x.ID.Equals(item.ID));
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
        public ResObject<bool> Active(Tag_Entity item)
        {
            var resOutput = new ResObject<bool>();
            var resItem = base.GetById<Tag_Entity>(x => x.ID.Equals(item.ID));
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
        public ResObject<bool> DeActive(Tag_Entity item)
        {
            var resOutput = new ResObject<bool>();
            var resItem = base.GetById<Tag_Entity>(x => x.ID.Equals(item.ID));
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
        public ResObject<Tag_Entity> Edit(Tag_Entity item)
        {
            var resOutput = new ResObject<Tag_Entity>();
            var resItem = base.GetById<Tag_Entity>(x => x.ID.Equals(item.ID));
            if (resItem != null && resItem.isValidate() && resItem.obj != null && resItem.obj.ID > 0)
            {
                resItem = base.GetById<Tag_Entity>(x => x.Name.Equals(item.Name) && !x.ID.Equals(item.ID));
                if (resItem != null && resItem.isValidate() && resItem.obj != null && resItem.obj.ID > 0)
                {
                    resOutput.codeError = "02";
                    resOutput.strError = "Tên trùng với một bản ghi khác";
                    return resOutput;
                }

                resItem.obj.ID = item.ID;
                resItem.obj.DateUpdate = DateTime.Now.ToString(CustEnum.FormatDateTime);
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
        public ResObject<Tag_Entity> GetAll(Expression<Func<Tag_Entity, bool>> func = null)
        {
            return base.GetByFilter<Tag_Entity>(func, nameof(Tag_Entity.Name), int.MaxValue);
        }
        public ResObject<Tag_Entity> GetByID(long ID)
        {
            return base.GetById<Tag_Entity>(x => x.ID.Equals(ID));
        }
        public ResObject<Tag_Entity> Insert(Tag_Entity item)
        {
            item.DateCreate = DateTime.Now.ToString(CustEnum.FormatDateTime);
            item.IsActive = CustEnum.IsActive;
            item.IsDelete = CustEnum.IsNotDelete;
            var resItem = base.GetById<Tag_Entity>(x => x.Name.Equals(item.Name));
            if (resItem != null && resItem.isValidate() && (resItem.obj == null || resItem.obj.ID == 0))
            {
                return base.Insert(item);
            }
            else
            {
                resItem.codeError = "01";
                resItem.strError = "Đã tồn tại tên tag";
            }
            return resItem;
        }
    }
}
