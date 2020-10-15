using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InternetBankingRESTfulServiceApi.Models
{
    public class Version
    {
        public int id { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy mm, dd}")]
        public DateTime? Date { get; set; }
    }
}
