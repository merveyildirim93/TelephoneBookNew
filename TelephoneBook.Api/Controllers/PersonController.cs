using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TelephoneBook.Business.PersonBusiness;
using TelephoneBook.DataAccess.Entity;
using TelephoneBook.Helper.Response;

namespace TelephoneBook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        IPersonBusiness personBusiness;
        public PersonController(IPersonBusiness personBusiness)
        {
            this.personBusiness = personBusiness;
        }

        [HttpGet]
        [Route("PersonList")]
        public List<Person> PersonList()
        {
            return personBusiness.PersonList();
        }

        [HttpPost]
        [Route("CreatePerson")]
        public PrimitiveResponse CreatePerson(Person model)
        {
            return personBusiness.CreatePerson(model);
        }

        [HttpPost]
        [Route("EditPerson")]
        public PrimitiveResponse EditPerson(Person model)
        {
            return personBusiness.EditPerson(model);
        }

        [HttpGet]
        [Route("DeletePerson")]
        public PrimitiveResponse DeletePerson(Guid id)
        {
            return personBusiness.DeletePerson(id);
        }

        [HttpGet]
        [Route("GetPersonForEdit")]
        public Person GetPersonForEdit(Guid id)
        {
            return personBusiness.GetPersonForEdit(id);
        }

    }
}
