using System;
using System.Collections.Generic;

namespace Model.GiamKichSan.Models
{
    public class ResObject<T>
    {
        private const string _validate = "00";
        public String codeError { get; set; }
        public String strError { get; set; }
        public T obj { get; set; }
        public List<T> listObj { get; set; }
        public T[] arrObj { get; set; }

        public ResObject()
        {
            codeError = _validate;
            //strError = "Không xử lý được đối tượng";
        }

        public bool isValidate()
        {
            return codeError.Equals(_validate);
        }
    }
}
