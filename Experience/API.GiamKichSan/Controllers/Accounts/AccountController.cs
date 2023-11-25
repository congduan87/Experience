using API.GiamKichSan.Common;
using Microsoft.AspNetCore.Mvc;
using Model.GiamKichSan.Data.Accounts;
using Model.GiamKichSan.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.GiamKichSan.Controllers.Accounts
{
    [ApiController]
    [Route("Accounts/[controller]")]
    public class AccountController : ControllerBase
    {
        private AccountRepository controllerRepository { get; set; }
        public AccountController()
        {
            controllerRepository = new AccountRepository(SessionGlobal.DefaultConnectString);
        }

        [HttpGet]
        public ResObject GetAll()
        {
            return controllerRepository.GetAll();
        }
        [HttpGet("{id}")]
        public ResObject GetByID([FromRoute] int id)
        {
            return controllerRepository.GetByID(id);
        }
        [HttpPost]
        public ResObject Insert([FromBody] Account_Entity item)
        {
            return controllerRepository.Insert(item);
        }
        [HttpPut("{id}")]
        public ResObject Edit([FromRoute] int id, [FromBody] Account_Entity item)
        {
            return controllerRepository.Edit(item);
        }
        [HttpDelete]
        public ResObject Delete([FromRoute] int id)
        {
            return controllerRepository.Delete(id);
        }
    }
}
