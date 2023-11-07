using MyWedding.Data;
using MyWedding.Models;
using System.Collections.Generic;

namespace MyWedding.Common
{
    public static class WeddingInfoExtension
    {
        public static FileUploadModel Change(this FileUpload x)
        {
            return new FileUploadModel()
            {
                ID = x.ID,
                Name = x.Name,
                IDWedding = x.IDWedding,
                DateUpload = x.DateUpload,
                Path = x.Path,
                IsHidden = x.IsHidden,
                OrderIndex = x.OrderIndex,
                FullPath = Helper.GetPathImage(x.Path)
            };
        }
        public static WeddingCoupleModel Change(this List<WeddingCouple> list)
        {
            WeddingCoupleModel weddingCouple = new WeddingCoupleModel();
            foreach (var item in list)
            {
                weddingCouple.IDWedding = item.IDWedding;
                if (item.IsMale)
                {
                    weddingCouple.MaleName = item.FullName;
                    weddingCouple.MaleDescription = item.Description;
                    weddingCouple.MaleIDBankCard = item.IDBankCard;
                    weddingCouple.MaleNameBank = item.NameBank;
                    weddingCouple.MaleAddressBank = item.AddressBank;
                    weddingCouple.MaleImageBank = item.ImageBank;
                    weddingCouple.FatherMaleName = item.FatherName;
                    weddingCouple.MotherMaleName = item.MotherName;
                    weddingCouple.MaleFacebookUrl = item.FacebookUrl;
                    weddingCouple.MaleImage = item.Image;
                }
                else
                {
                    weddingCouple.FeMaleName = item.FullName;
                    weddingCouple.FeMaleDescription = item.Description;
                    weddingCouple.FeMaleIDBankCard = item.IDBankCard;
                    weddingCouple.FeMaleNameBank = item.NameBank;
                    weddingCouple.FeMaleAddressBank = item.AddressBank;
                    weddingCouple.FeMaleImageBank = item.ImageBank;
                    weddingCouple.FatherFeMaleName = item.FatherName;
                    weddingCouple.MotherFeMaleName = item.MotherName;
                    weddingCouple.FeMaleFacebookUrl = item.FacebookUrl;
                    weddingCouple.FeMaleImage = item.Image;
                }
            }
            return weddingCouple;
        }
        public static List<WeddingCouple> Change(this WeddingCoupleModel item, List<WeddingCouple> listDb)
        {
            var maleWedding = listDb.Find(x => x.IsMale) ?? new Data.WeddingCouple() { IDWedding = item.IDWedding, IsMale = true };
            var feMaleWedding = listDb.Find(x => !x.IsMale) ?? new Data.WeddingCouple() { IDWedding = item.IDWedding, IsMale = false };

            maleWedding.FullName = item.MaleName;
            maleWedding.Description = item.MaleDescription;
            maleWedding.IDBankCard = item.MaleIDBankCard;
            maleWedding.NameBank = item.MaleNameBank;
            maleWedding.ImageBank = item.MaleImageBank;
            maleWedding.AddressBank = item.MaleAddressBank;
            maleWedding.FatherName = item.FatherMaleName;
            maleWedding.MotherName = item.MotherMaleName;
            maleWedding.FacebookUrl = item.MaleFacebookUrl;
            maleWedding.Image = item.MaleImage;

            feMaleWedding.FullName = item.FeMaleName;
            feMaleWedding.Description = item.FeMaleDescription;
            feMaleWedding.IDBankCard = item.FeMaleIDBankCard;
            feMaleWedding.NameBank = item.FeMaleNameBank;
            feMaleWedding.ImageBank = item.FeMaleImageBank;
            feMaleWedding.AddressBank = item.FeMaleAddressBank;
            feMaleWedding.FatherName = item.FatherFeMaleName;
            feMaleWedding.MotherName = item.MotherFeMaleName;
            feMaleWedding.FacebookUrl = item.FeMaleFacebookUrl;
            feMaleWedding.Image = item.FeMaleImage;

            return new List<WeddingCouple>() { maleWedding, feMaleWedding };
        }
    }
}
