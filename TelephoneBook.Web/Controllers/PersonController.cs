using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelephoneBook.DataAccess.Entity;
using TelephoneBook.Helper.Response;
using TelephoneBook.Helper.RestHelper;
using TelephoneBook.Web.Common;

namespace TelephoneDirectory.Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly IOptions<ConfigModel> _config;
        public PersonController(IOptions<ConfigModel> _config)
        {
            this._config = _config;
        }
        public IActionResult Index()
        {
            var list = RestHelper.Get<List<Person>>(_config.Value.ApiBaseUrl + "Person/PersonList");
            return View(list);
        }

        [HttpPost]
        public PrimitiveResponse CreatePerson(Person model)
        {
            return RestHelper.Post<PrimitiveResponse, Person>(_config.Value.ApiBaseUrl + "Person/CreatePerson", model);
        }

        public PrimitiveResponse DeletePerson(Guid id)
        {
            return RestHelper.Get<PrimitiveResponse>(_config.Value.ApiBaseUrl + "Person/DeletePerson?id="+ id);
        }
    }
}
