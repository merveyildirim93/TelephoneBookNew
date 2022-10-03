using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.DataAccess.Entity;
using TelephoneBook.Helper.Response;

namespace TelephoneBook.Business.PersonBusiness
{
    public interface IPersonBusiness
    {
        PrimitiveResponse CreatePerson(Person model);
        PrimitiveResponse EditPerson(Person model);
        PrimitiveResponse DeletePerson(Guid id);
        Person GetPersonForEdit(Guid id);
        List<Person> PersonList();
    }
}
