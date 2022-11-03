using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Models.DBModels
{
   public class MessegeMail
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Topic { get; set; }
        public string Message { get; set; }
        public string? HowMess { get; set; }
        public string? WhomMess { get; set; }
    }
}
