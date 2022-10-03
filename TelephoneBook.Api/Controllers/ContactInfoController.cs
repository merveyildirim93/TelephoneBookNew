using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TelephoneBook.Business.ContactInfoBusiness;
using TelephoneBook.DataAccess.Entity;
using TelephoneBook.Helper.Response;

namespace TelephoneDirectory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInfoController : ControllerBase
    {
        IContactInfoBusiness contactInfoBusiness;
        public ContactInfoController(IContactInfoBusiness contactInfoBusiness)
        {
            this.contactInfoBusiness = contactInfoBusiness;
        }

        [HttpGet]
        [Route("ContactInfoList")]
        public List<ContactInfo> ContactInfoList(Guid personId)
        {
            return contactInfoBusiness.ContactInfoList(personId);
        }

        [HttpPost]
        [Route("CreateContactInfo")]
        public PrimitiveResponse CreateContactInfo(ContactInfo model)
        {
            return contactInfoBusiness.CreateContactInfo(model);
        }

        [HttpPost]
        [Route("EditContactInfo")]
        public PrimitiveResponse EditContactInfo(ContactInfo model)
        {
            return contactInfoBusiness.EditContactInfo(model);
        }

        [HttpPost]
        [Route("DeleteContactInfo")]
        public PrimitiveResponse DeleteContactInfo(Guid id)
        {
            return contactInfoBusiness.DeleteContactInfo(id);
        }

        [HttpGet]
        [Route("GetContactInfoForEdit")]
        public ContactInfo GetContactInfoForEdit(Guid id)
        {
            return contactInfoBusiness.GetContactInfoForEdit(id);
        }

    }
}
