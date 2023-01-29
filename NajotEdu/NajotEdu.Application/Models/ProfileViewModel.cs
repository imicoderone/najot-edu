using NajotEdu.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NajotEdu.Application.Models
{
    public class ProfileViewModel
    {
        public string UserName { get; set; }
        public string Fullname { get; set; }
        public string? PhotoPath { get; set; }

        public ICollection<GroupViewModel> Groups { get; set; }
    }
}
