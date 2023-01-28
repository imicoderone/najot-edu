﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NajotEdu.Application.Abstractions;
using NajotEdu.Application.Models;
using NajotEdu.Domain.Entities;

namespace NajotEdu.Application.Services
{
    public class GroupService : IGroupService
    {
        private readonly IApplicationDbContext _context;

        public GroupService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GroupViewModel> GetByIdAsync(int id)
        {
            var entity = await _context.Groups.FirstOrDefaultAsync(x => x.Id == id);
            
            return new GroupViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                TeacherId = entity.TeacherId,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate
            };
        }

        public async Task<List<GroupViewModel>> GetAllAsync()
        {
            return await _context.Groups
                .Select(x => new GroupViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    TeacherId = x.TeacherId,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate
                })
                .ToListAsync();
        }

        public async Task CreateAsync(CreateGroupModel model)
        {
            var entity = new Group()
            {
                Name = model.Name,
                TeacherId = model.TeacherId,
                StartDate = model.StartDate.ToDateTime(TimeOnly.MinValue),
                EndDate = model.EndDate.ToDateTime(TimeOnly.MaxValue)
            };

            _context.Groups.Add(entity);

            var lessons = CreateLessons(entity, model.LessonStartTime, model.LessonEndTime);

            _context.Lessons.AddRange(lessons);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateGroupModel model)
        {
            var entity = await _context.Groups.Include(x => x.Lessons).FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity == null)
            {
                throw new Exception("Not found");
            }

            entity.Name = model.Name ?? entity.Name;
            entity.TeacherId = model.TeacherId ?? entity.TeacherId;

            _context.Groups.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Groups.FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                throw new Exception("Not found");
            }

            _context.Groups.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LessonViewModel>> GetLessonsAsync(int groupId)
        {
            var lessons = await _context.Lessons.Where(x => x.GroupId == groupId)
                .Select(x => new LessonViewModel()
                {
                    Id = x.Id,
                    GroupId = x.GroupId,
                    StartDateTime = x.StartDateTime,
                    EndDateTime = x.EndDateTime
                })
                .ToListAsync();

            return lessons;
        }

        public async Task AddStudentAsync(AddStudentGroupModel model, int groupId)
        {
            if (!await _context.Students.AnyAsync(x => x.Id == model.StudentId))
            {
                throw new Exception("Not found");
            }

            if (!await _context.Groups.AnyAsync(x => x.Id == groupId))
            {
                throw new Exception("Not found");
            }

            var studentInGroup = new StudentGroup()
            {
                GroupId = groupId,
                StudentId = model.StudentId,
                IsPayed = model.IsPayed,
                JoinedDate = model.JoinedDate
            };

            _context.StudentsGroups.Add(studentInGroup);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveStudentAsync(int studentId, int groupId)
        {
            var entity = await
                _context.StudentsGroups.FirstOrDefaultAsync(x => x.GroupId == groupId && x.StudentId == studentId);

            if (entity == null)
            {
                throw new Exception("Not found");
            }
            
            _context.StudentsGroups.Remove(entity);

            await _context.SaveChangesAsync();
        }

        private List<Lesson> CreateLessons(Group entity, TimeSpan lessonStartTime, TimeSpan lessonEndTime)
        {
            var lessons = new List<Lesson>();

            var totalDaysFromStartToEnd = (entity.EndDate - entity.StartDate).Days;

            var currentDate = entity.StartDate;
            for (int i = 1; i <= totalDaysFromStartToEnd; i++)
            {
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    var lesson = new Lesson()
                    {
                        Group = entity,
                        StartDateTime = currentDate.Date + lessonStartTime,
                        EndDateTime = currentDate.Date + lessonEndTime
                    };

                    lessons.Add(lesson);
                }

                currentDate = currentDate.AddDays(1);
            }

            return lessons;
        }
    }
}
