using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneBook.DataAccess.Entity
{
    public class Report
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime RequestDate { get; set; }
        public int Status { get; set; } // 1-Hazırlanıyor, 2-Tamamlandı
        public string ReportName { get; set; }
    }
}
