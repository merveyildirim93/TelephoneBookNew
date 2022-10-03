using System;
using System.ComponentModel.DataAnnotations;

namespace TelephoneBook.DataAccess.Entity
{
    public class ContactInfo
    {
        [Key]
        public Guid Id { get; set; }
        public Guid FkPerson { get; set; }
        public string Phone { get; set; }   
        public string Mail { get; set; }   
        public string Location { get; set; }
        public bool Deleted { get; set; }
    }
}
