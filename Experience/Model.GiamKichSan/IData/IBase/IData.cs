using Model.GiamKichSan.Models;
using System;

namespace Model.GiamKichSan.IData.IBase
{
    public interface IData<T, TM>
    {
        ResObject<T> Insert(T item);
        ResObject<T> Edit(T item);
        ResObject<bool> Delete(int ID);
        ResObject<T> GetAll(Func<TM, bool> func = null);
        ResObject<T> GetByID(int ID);
    }
}
