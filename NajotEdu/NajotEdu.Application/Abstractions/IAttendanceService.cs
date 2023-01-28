using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NajotEdu.Application.Models;

namespace NajotEdu.Application.Abstractions
{
    public interface IAttendanceService
    {
        Task CheckAsync(DoAttendanceCheckModel model);
    }
}
