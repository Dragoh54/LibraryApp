﻿using LibraryApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Application.Interfaces.Auth;

public interface IJwtProvider
{
    public string GenerateAccessToken(UserEntity user);
    public RefreshToken GenerateRefreshToken(UserEntity user);
}
