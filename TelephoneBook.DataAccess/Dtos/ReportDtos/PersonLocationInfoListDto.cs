using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneBook.DataAccess.Dtos.ReportDtos
{
    public class PersonLocationInfoListDto
    {
        public string Location { get; set; }
        public int LocationCount { get; set; }
        public string Phone { get; set; }
        public string Person { get; set; }
    }
}
