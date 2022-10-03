using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.DataAccess.Entity;
using TelephoneBook.Helper.Response;

namespace TelephoneBook.Business.ContactInfoBusiness
{
    public interface IContactInfoBusiness
    {
        PrimitiveResponse CreateContactInfo(ContactInfo model);
        PrimitiveResponse EditContactInfo(ContactInfo model);
        PrimitiveResponse DeleteContactInfo(Guid id);
        ContactInfo GetContactInfoForEdit(Guid id);
        List<ContactInfo> ContactInfoList(Guid personId);
    }
}
