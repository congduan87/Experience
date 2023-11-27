using Model.GiamKichSan.Common.SQL;
using Model.GiamKichSan.IData.IBase;
using Model.GiamKichSan.Models;
using Model.GiamKichSan.Models.Accounts;
using System;
using System.Linq.Expressions;

namespace Model.GiamKichSan.Data.Accounts
{
    public class DocumentUploadRepository : SqlBaseRepository<DocumentUpload_Entity>
    {
        public DocumentUploadRepository(string connectionString) : base(connectionString)
        {
        }

        public ResObject<bool> Delete(long ID, string IDAccount)
        {
            return base.Delete(x => x.ID.Equals(ID) && x.IDCreate.Equals(IDAccount));
        }

        public ResObject<DocumentUpload_Entity> Edit(DocumentUpload_Entity item)
        {
            return base.Update(item, x => x.ID.Equals(item.ID));
        }

        public ResObject<DocumentUpload_Entity> GetAll(Expression<Func<DocumentUpload_Entity, bool>> func = null)
        {
            return base.GetByFilter<DocumentUpload_Entity>(func, nameof(DocumentUpload_Entity.ID), int.MaxValue);
        }

        public ResObject<DocumentUpload_Entity> GetByID(long ID, string IDAccount)
        {
            return base.GetById<DocumentUpload_Entity>(x => x.ID.Equals(ID) && x.IDCreate.Equals(IDAccount));
        }

        public byte GetLevelChildByID(long ID, string IDAccount)
        {
            var output = base.GetById<DocumentUpload_Entity>(x => x.ID.Equals(ID) && x.IDCreate.Equals(IDAccount));
            if (output != null && output.isValidate() && output.obj != null)
                return Convert.ToByte(((output.obj as DocumentUpload_Entity)?.LevelChild ?? 0) + 1);
            else
                return (byte)1;
        }

        public ResObject<DocumentUpload_Entity> Insert(DocumentUpload_Entity item)
        {
            return base.Insert(item);
        }
    }
}
