using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneBook.DataAccess.Dtos.ReportDtos
{
    public class LocationReportDto
    {
        public LocationReportDto()
        {
            PersonLocationInfoLists = new List<PersonLocationInfoListDto>();
        }
        public string Location { get; set; }
        public int LocationCount { get; set; }
        public int PhoneCount { get; set; }
        public int PersonCount { get; set; }
        public List<PersonLocationInfoListDto> PersonLocationInfoLists { get; set; }
    }
}
