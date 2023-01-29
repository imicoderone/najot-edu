using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NajotEdu.Application.Abstractions
{
    public interface IFileService
    {
        Task<string> Upload(IFormFile formFile);
    }
}
