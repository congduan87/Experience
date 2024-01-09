using API.GiamKichSan.Common;
using Microsoft.AspNetCore.Mvc;
using Model.GiamKichSan.Common.SQL;
using Model.GiamKichSan.Data.MyIPs;
using Model.GiamKichSan.Models;
using Model.GiamKichSan.Models.MyIPs;
using System.Collections.Generic;
using System.Linq;

namespace API.GiamKichSan.Controllers.MyIPs
{
    [ApiController]
    [Route("MyIPs/[controller]")]
    public class HomeAddressController : ControllerBase
    {
        private HomeAddressRepository controllerRepository { get; set; }
        public HomeAddressController(BaseSQLConnection baseSQLConnection)
        {
            controllerRepository = new HomeAddressRepository(baseSQLConnection);
        }

        [HttpGet]
        public ResObject<HomeAddress_Entity> GetAll()
        {
            return controllerRepository.GetAll();
        }
        [HttpGet("{id}")]
        public ResObject<HomeAddress_Entity> GetByID([FromRoute] int id)
        {
            return controllerRepository.GetByID(id);
        }
        [HttpPost]
        public ResObject<HomeAddress_Entity> Insert(HomeAddress_Entity item)
        {
            var list = controllerRepository.GetAll();

            if (list.isValidate() && list.listObj != null)
            {
                var HomeAddresses = list.listObj as List<HomeAddress_Entity>;
                if (HomeAddresses != null && HomeAddresses.Count > 0)
                {
                    foreach (HomeAddress_Entity homeAddress in HomeAddresses)
                    {
                        homeAddress.IsActive = false;
                        controllerRepository.Edit(homeAddress);
                    }

                    if (HomeAddresses.Any(x => x.IPAddress.Equals(item.IPAddress)))
                    {
                        var homeAddress = HomeAddresses.First(x => x.IPAddress.Equals(item.IPAddress));
                        homeAddress.IsActive = true;
                        return controllerRepository.Edit(homeAddress);
                    }
                }
            }
            return controllerRepository.Insert(item);
        }
        [HttpPut("{id}")]
        public ResObject<HomeAddress_Entity> Edit([FromRoute] int id, [FromBody] HomeAddress_Entity item)
        {
            return controllerRepository.Edit(item);
        }
        [HttpDelete]
        public ResObject<bool> Delete([FromRoute] int id)
        {
            return controllerRepository.Delete(id);
        }
    }
}
