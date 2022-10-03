using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using TelephoneBook.DataAccess.Entity;
using TelephoneBook.Helper.Response;
using TelephoneBook.Helper.RestHelper;
using TelephoneBook.Web.Common;

namespace TelephoneBook.Web.Controllers
{
    public class ContactInfoController : Controller
    {
        private readonly IOptions<ConfigModel> _config;
        public ContactInfoController(IOptions<ConfigModel> _config)
        {
            this._config = _config;
        }
        public IActionResult Index(Guid personId)
        {
            var list = RestHelper.Get<List<ContactInfo>>(_config.Value.ApiBaseUrl + "ContactInfo/ContactInfoList?personId=" + personId);
            return View(list);
        }

        [HttpPost]
        public PrimitiveResponse CreateContactInfo(ContactInfo model)
        {
            return RestHelper.Post<PrimitiveResponse, ContactInfo>(_config.Value.ApiBaseUrl + "ContactInfo/CreateContactInfo", model);
        }

        public PrimitiveResponse DeleteContactInfo(Guid id)
        {
            return RestHelper.Get<PrimitiveResponse>(_config.Value.ApiBaseUrl + "ContactInfo/DeleteContactInfo?id=" + id);
        }

    }
}
