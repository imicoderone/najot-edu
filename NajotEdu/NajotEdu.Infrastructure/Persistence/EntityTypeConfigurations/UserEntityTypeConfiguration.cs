﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NajotEdu.Domain.Entities;
using NajotEdu.Domain.Enums;

namespace NajotEdu.Infrastructure.Persistence.EntityTypeConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.UserName)
                .HasMaxLength(30)
                .IsRequired();

            builder.HasIndex(i => i.UserName)
                .IsUnique();

            builder.HasData(new User()
            {
                Id = 1,
                UserName = "Admin",
                PasswordHash =
                    "CA5B9811BE39C13BA3F8265C006761214B85F36FFE177C482AA548A30FC2C8994F5AE33790A4AE6A302B65A05A906AAED4912F02C0E69FC6CE14A9C90AD998A0",
                Role = UserRole.Admin,
                Fullname = "Adminbek Adminov"
            });
        }
    }
}
