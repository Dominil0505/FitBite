using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using DieticianApp.Models.Entities;
using DieticianApp.Models.JoinTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations
{
    public class UserAccountRepository(IOptions<JwtSection> config,AppDbContext _context) : IUserAccount
    {
        public async Task<GeneralResponse> CreateUserAsync(Register user)
        {
            if (user is null)
                return new GeneralResponse(false, "Model is empty");

            var userIsExist = await _context.Users.FirstOrDefaultAsync(_ => _.Email == user.Email);
            if (userIsExist != null)
                return new GeneralResponse(false, "User is already registered");

            var applicationUser = await AddToDatabase(new Users
            {
                User_Name = user.Name,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
            });

            if (user.Role == "Dietician")
            {
                var role = await FindRole("Dietician");
                await AddToDatabase(new Dieticians { User_Id = applicationUser.User_Id });
                await AddUserRoles(applicationUser.User_Id, role.Role_Id);
            }
            else if(user.Role == "Patient")
            {
                var role = await FindRole("Patient");
                await AddToDatabase(new Patients { User_Id = applicationUser.User_Id });
                await AddUserRoles(applicationUser.User_Id, role.Role_Id);
            }
            else if (user.Role == "Admin")
            {
                var role = await FindRole("Admin");
                await AddToDatabase(new Admins { User_Id = applicationUser.User_Id });
                await AddUserRoles(applicationUser.User_Id, role.Role_Id);
            }
            else
            {
                return new GeneralResponse(false, "Role is not found");
            }

            return new GeneralResponse(true, "Successfully registred");
        }

        public async Task<LoginResponse> RefreshTokenAsnyc(RefreshToken token)
        {
            if (token is null) return new LoginResponse(false, "Model is empty");

            var findToken = await _context.RefreshTokenInfos.FirstOrDefaultAsync(_ => _.Token!.Equals(token.Token));
            if (findToken is null) return new LoginResponse(false, "Refresh token is required");

            var user = await _context.Users.FirstOrDefaultAsync(_ => _.User_Id == findToken.UserId);
            if (user is null) return new LoginResponse(false, "Refresh token could not be generated because user not found");

            var userRole = await FindUserRole(user.User_Id);
            var roleName = await FindRoleName(userRole.RoleId);
            string jwtToken = GenerateToken(user, roleName.Role_Name!);
            string refreshToken = GenerateRefreshToken();

            var updateRefreshToken = await _context.RefreshTokenInfos.FirstOrDefaultAsync(_ => _.UserId == user.User_Id);
            if (updateRefreshToken is null) return new LoginResponse(false, "Refresh token could not be generated because user has not signed in");
            updateRefreshToken.Token = refreshToken;

            return new LoginResponse(true, "Token refreshed successfully", jwtToken, refreshToken);
        }

        public async Task<LoginResponse> SignInAsync(CustomLogin user)
        {
            if (user is null) 
                return new LoginResponse(false, "Model is empty");

            var findUser = await FindUserByEmail(user.Email!);
            if (findUser is null) 
                return new LoginResponse(false, "User not found");

            if (!BCrypt.Net.BCrypt.Verify(user.Password, findUser.Password))
                return new LoginResponse(false, "Email or Password not valid");

            var getUserRole = await FindUserRole(findUser.User_Id);
            if (getUserRole is null) 
                return new LoginResponse(false, "User role not found");

            var getRoleName = await FindRoleName(getUserRole.RoleId);
            if (getRoleName is null) 
                return new LoginResponse(false, "User role not found");

            var jwtToken = GenerateToken(findUser, getRoleName!.Role_Name);
            string refreshToken = GenerateRefreshToken();

            var findUserToken = await _context.RefreshTokenInfos.FirstOrDefaultAsync(_ => _.UserId == findUser.User_Id);

            if(findUserToken is not null)
            {
                findUserToken!.Token = refreshToken;
                await _context.SaveChangesAsync();
            }
            else
            {
                await AddToDatabase(new RefreshTokenInfo() { Token = refreshToken, UserId = findUser.User_Id });
            }

            return new LoginResponse(true, "Login Successfully", jwtToken, refreshToken);
        }

        private string GenerateToken(Users user, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Value.Key!));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userClaims = new[] 
            {
                new Claim(ClaimTypes.NameIdentifier, user.User_Id.ToString()),
                new Claim(ClaimTypes.Name, user.User_Name!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, role!)
            };

            var token = new JwtSecurityToken(
                issuer: config.Value.Issuer,
                audience: config.Value.Audience,
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<T> AddToDatabase<T>(T model)
        {
            var result = _context.Add(model);
            await _context.SaveChangesAsync();
            return (T)result.Entity;
        }

        private async Task AddUserRoles(int user_Id, int role_Id)
        {
            User_Roles user_roles = new User_Roles { UserId = user_Id, RoleId = role_Id };
            _context.User_Roles.Add(user_roles);
            await _context.SaveChangesAsync();
        }

        private async Task<Roles> FindRole(string roleName) => await _context.Roles.FirstOrDefaultAsync(_ => _.Role_Name == roleName);
        private async Task<User_Roles> FindUserRole(int userId) => await _context.User_Roles.FirstOrDefaultAsync(_ => _.UserId == userId);
        private async Task<Roles> FindRoleName(int roleId) => await _context.Roles.FirstOrDefaultAsync(_ => _.Role_Id == roleId);
        private async Task<Users> FindUserByEmail(string email) => await _context.Users.FirstOrDefaultAsync(_ => _.Email!.ToLower()!.Equals(email!.ToLower()));
        private static string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}
