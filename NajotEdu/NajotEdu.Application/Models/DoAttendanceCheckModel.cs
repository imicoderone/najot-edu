using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NajotEdu.Application.Models
{
    public class DoAttendanceCheckModel
    {
        public int LessonId { get; set; }
        public List<AttendanceCheckModel> Checks { get; set; }
    }
}
