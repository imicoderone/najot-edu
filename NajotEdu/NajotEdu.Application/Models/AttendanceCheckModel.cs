using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NajotEdu.Application.Models
{
    public class AttendanceCheckModel
    {
        public int StudentId { get; set; }
        public bool HasParticipated { get; set; }
    }
}
