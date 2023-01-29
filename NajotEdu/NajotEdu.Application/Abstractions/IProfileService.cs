using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NajotEdu.Application.Models;

namespace NajotEdu.Application.Abstractions
{
    public interface IProfileService
    {
        Task SetPhoto(IFormFile formFile);
        Task<ProfileViewModel> GetProfile();
    }
}
