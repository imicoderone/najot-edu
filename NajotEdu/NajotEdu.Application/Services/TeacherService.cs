﻿using Microsoft.EntityFrameworkCore;
using NajotEdu.Application.Abstractions;
using NajotEdu.Application.Models;
using NajotEdu.Domain.Entities;
using NajotEdu.Domain.Enums;

namespace NajotEdu.Application.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IApplicationDbContext _context;
        private readonly IHashProvider _hashProvider;

        public TeacherService(IApplicationDbContext context, IHashProvider hashProvider)
        {
            _context = context;
            _hashProvider = hashProvider;
        }

        public async Task<TeacherViewModel> GetByIdAsync(int id)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(x => x.Id == id && x.Role == UserRole.Teacher);

            return new TeacherViewModel()
            {
                Id = entity.Id,
                UserName = entity.UserName,
                Fullname = entity.Fullname
            };
        }

        public async Task<List<TeacherViewModel>> GetAllAsync()
        {
            return await _context.Users
                .Where(x => x.Role == UserRole.Teacher)
                .Select(x => new TeacherViewModel()
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Fullname = x.Fullname
                })
                .ToListAsync();
        }

        public async Task CreateAsync(CreateTeacherModel model)
        {
            var entity = new User()
            {
                UserName = model.UserName,
                PasswordHash = _hashProvider.GetHash(model.Password),
                Fullname = model.Fullname,
                Role = UserRole.Teacher
            };

            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateTeacherModel model)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(x => x.Id == model.Id && x.Role == UserRole.Teacher);

            if (entity == null)
            {
                throw new Exception("Not found");
            }

            entity.UserName = model.UserName ?? entity.UserName;
            entity.Fullname = model.Fullname ?? entity.Fullname;
            entity.PasswordHash = model.Password == null ? entity.PasswordHash : _hashProvider.GetHash(model.Password);

            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(x => x.Id == id && x.Role == UserRole.Teacher);

            if (entity == null)
            {
                throw new Exception("Not found");
            }

            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
