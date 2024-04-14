﻿using MailClient.DB.Content;
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
        public string? Topic { get; set; }
        public string? MailMess { get; set; }
        public string? IdHow { get; set; }
        public string? IdWhom { get; set; }
        public virtual ICollection<FileMessage>? Files { get; set; }
    }
}
