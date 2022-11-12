using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Models.DBModels
{
    public class User
    {
        public int Id { get; set; }
        public string? Mail { get; set; }
        public string? Passowrd { get; set; }
    }
}
