﻿using Microsoft.EntityFrameworkCore;
using NajotEdu.Application.Abstractions;
using NajotEdu.Application.Models;
using NajotEdu.Domain.Entities;

namespace NajotEdu.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IApplicationDbContext _context;

        public StudentService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StudentViewModel> GetByIdAsync(int id)
        {
            var entity = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);

            return new StudentViewModel()
            {
                Id = entity.Id,
                Fullname = entity.Fullname,
                BirthDate = entity.BirthDate,
                PhoneNumber = entity.PhoneNumber
            };
        }

        public async Task<List<StudentViewModel>> GetAllAsync()
        {
            return await _context.Students
                .Select(x => new StudentViewModel()
                {
                    Id = x.Id,
                    Fullname = x.Fullname,
                    BirthDate = x.BirthDate,
                    PhoneNumber = x.PhoneNumber
                })
                .ToListAsync();
        }

        public async Task CreateAsync(CreateStudentModel model)
        {
            var entity = new Student()
            {
                Fullname = model.Fullname,
                BirthDate = model.BirthDate,
                PhoneNumber = model.PhoneNumber,
                CreatedDateTime = DateTime.UtcNow
            };

            _context.Students.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateStudentModel model)
        {
            var entity = await _context.Students.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity == null)
            {
                throw new Exception("Not found");
            }

            entity.Fullname = model.Fullname ?? entity.Fullname;
            entity.BirthDate = model.BirthDate ?? entity.BirthDate;
            entity.PhoneNumber = model.PhoneNumber ?? entity.PhoneNumber;

            _context.Students.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                throw new Exception("Not found");
            }

            _context.Students.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
