using Model.GiamKichSan.Common.SQL;
using Model.GiamKichSan.IData.IBase;
using Model.GiamKichSan.Models;
using Model.GiamKichSan.Models.MyIPs;
using System;

namespace Model.GiamKichSan.Data.MyIPs
{
    public class HomeAddressRepository : SqlBaseRepository<HomeAddress_Entity>, IData<HomeAddress_Entity, HomeAddress_Entity>
    {
        public HomeAddressRepository(BaseSQLConnection baseSQLConnection) : base(baseSQLConnection)
        {
        }

        public ResObject<bool> Delete(int ID)
        {
            return base.Delete(x => x.ID.Equals(ID));
        }

        public ResObject<HomeAddress_Entity> Edit(HomeAddress_Entity item)
        {
            return base.Update(new { IPAddress = item.IPAddress, DateCreate = item.DateCreate, IsActive = item.IsActive }, x => x.ID.Equals(item.ID));
        }

        public ResObject<HomeAddress_Entity> GetAll(Func<HomeAddress_Entity, bool> func = null)
        {
            return base.GetAll<HomeAddress_Entity>(nameof(HomeAddress_Entity.IsActive), int.MaxValue);
        }

        public ResObject<HomeAddress_Entity> GetByID(int ID)
        {
            return base.GetById<HomeAddress_Entity>(x => x.ID.Equals(ID));
        }

        public ResObject<HomeAddress_Entity> Insert(HomeAddress_Entity item)
        {
            return base.Insert(new { IPAddress = item.IPAddress, DateCreate = item.DateCreate, IsActive = item.IsActive });
        }
    }
}
