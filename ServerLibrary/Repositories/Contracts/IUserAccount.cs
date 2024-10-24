﻿using BaseLibrary.DTOs;
using BaseLibrary.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IUserAccount
    {
        Task<GeneralResponse> CreateUserAsync(Register user);
        Task<LoginResponse> SignInAsync(CustomLogin user);
        Task<LoginResponse> RefreshTokenAsnyc(RefreshToken token);
    }
}
