﻿using NajotEdu.Domain.Enums;

namespace NajotEdu.Domain.Entities
{
    public class User
    {
        public User()
        {
            Groups = new HashSet<Group>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Fullname { get; set; }
        public UserRole Role { get; set; }
        public string? PhotoPath { get; set; }

        public ICollection<Group> Groups { get; set; }
    }
}
