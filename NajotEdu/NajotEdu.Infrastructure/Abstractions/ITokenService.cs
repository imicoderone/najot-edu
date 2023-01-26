﻿using NajotEdu.Domain.Entities;

namespace NajotEdu.Infrastructure.Abstractions
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
    }
}
