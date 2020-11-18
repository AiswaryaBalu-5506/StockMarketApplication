using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationMicroService.Models
{
    public class EmailSettingsModel
    {
        public string smtpUserName { get; set; }
        public string smtpPassword { get; set; }
    }
}
