using Model.GiamKichSan.Common.SQL;
using Model.GiamKichSan.IData.IBase;
using Model.GiamKichSan.Models;
using Model.GiamKichSan.Models.Accounts;
using System;

namespace Model.GiamKichSan.Data.Accounts
{
    public class AccountRepository : SqlBaseRepository<Account_Entity>, IData<Account_Entity, Account_Entity>
    {
        public AccountRepository(string connectionString) : base(connectionString)
        {

        }

        public ResObject<bool> Delete(int ID)
        {
            return base.Delete(x => x.Id.Equals(ID));
        }

        public ResObject<Account_Entity> Edit(Account_Entity item)
        {
            return base.Update(new
            {
                item.UserName,
                item.PasswordHash,
                item.Email,
                item.LastName,
                item.FirstName,
                item.PhoneNumber,
                item.SecurityStamp,
                item.PhoneNumberConfirmed,
                item.TwoFactorEnabled,
                item.LockoutEndDateUtc,
                item.LockoutEnabled,
                item.AccessFailedCount
            }, x => x.Id.Equals(item.Id));
        }

        public ResObject<Account_Entity> GetAll(Func<Account_Entity, bool> func = null)
        {
            return base.GetAll<Account_Entity>(nameof(Account_Entity.UserName));
        }

        public ResObject<Account_Entity> GetByID(int ID)
        {
            return base.GetById<Account_Entity>(x => x.Id.Equals(ID));
        }

        public ResObject<Account_Entity> Insert(Account_Entity item)
        {
            return base.Insert(new
            {
                item.UserName,
                item.PasswordHash,
                item.Email,
                item.LastName,
                item.FirstName,
                item.PhoneNumber,
                item.SecurityStamp,
                item.PhoneNumberConfirmed,
                item.TwoFactorEnabled,
                item.LockoutEndDateUtc,
                item.LockoutEnabled,
                item.AccessFailedCount
            });
        }

        ResObject<bool> IData<Account_Entity, Account_Entity>.Delete(int ID)
        {
            throw new NotImplementedException();
        }
    }
}
