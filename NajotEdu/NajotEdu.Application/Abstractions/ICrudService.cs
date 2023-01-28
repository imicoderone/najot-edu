using NajotEdu.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NajotEdu.Application.Abstractions
{
    public interface ICrudService<T, V, C, U>
    {
        Task<V> GetByIdAsync(T id);
        Task<List<V>> GetAllAsync();
        Task CreateAsync(C model);
        Task UpdateAsync(U model);
        Task DeleteAsync(T id);
    }
}
