﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jhipster.Crosscutting.Constants;
using Jhipster.Crosscutting.Exceptions;
using Jhipster.Domain;
using Jhipster.Domain.Services.Interfaces;
using Jhipster.Dto;
using Jhipster.Dto.Authentication;
using Jhipster.Service.Utilities;
using LanguageExt.UnitsOfMeasure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Crmf;
using RestSharp;

namespace Jhipster.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<UserService> _log;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public UserService(ILogger<UserService> log, UserManager<User> userManager,
            IPasswordHasher<User> passwordHasher, RoleManager<Role> roleManager,
            IHttpContextAccessor httpContextAccessor, IConfiguration configuration
            )
        {
            _log = log;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public virtual async Task<User> CreateUser(User userToCreate,string pw)
        {
            var user = new User
            {
                UserName = userToCreate.Login.ToLower(),
                FirstName = userToCreate.FirstName,
                LastName = userToCreate.LastName,
                Email = userToCreate.Email.ToLower(),
                PhoneNumber = userToCreate.PhoneNumber,
                ImageUrl = userToCreate.ImageUrl,
                LangKey = userToCreate.LangKey ?? Constants.DefaultLangKey,
                PasswordHash = _userManager.PasswordHasher.HashPassword(null, pw),
                Activated = true
            };
            await _userManager.CreateAsync(user);
            await CreateUserRoles(user, userToCreate.UserRoles.Select(iur => iur.Role.Name).ToHashSet());
            _log.LogDebug($"Created Information for User: {user}");
            return user;
        }

        public virtual async Task<User> UpdateUser(User userToUpdate)
        {
            //TODO use Optional
            var user = await _userManager.FindByIdAsync(userToUpdate.Id);
            user.Login = userToUpdate.Login.ToLower();
            user.UserName = userToUpdate.Login.ToLower();
            user.FirstName = userToUpdate.FirstName;
            user.LastName = userToUpdate.LastName;
            user.Email = userToUpdate.Email;
            user.PhoneNumber = userToUpdate.PhoneNumber;
            user.ImageUrl = userToUpdate.ImageUrl;
            user.Activated = userToUpdate.Activated;
            user.LangKey = userToUpdate.LangKey;
            await _userManager.UpdateAsync(user);
            //await _collaboratorRepository.UpdateRefCodeCollab(user.Login, user.ReferralCode);
            await UpdateUserRoles(user, userToUpdate.UserRoles.Select(iur => iur.Role.Name).ToHashSet());
            return user;
        }

        public virtual async Task<User> CompletePasswordReset(string newPassword, string key)
        {
            _log.LogDebug($"Reset user password for reset key {key}");
            var user = await _userManager.Users.SingleOrDefaultAsync(it => it.ResetKey == key);
            if (user == null || user.ResetDate <= DateTime.Now.Subtract(86400.Seconds())) return null;
            user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);
            user.ResetKey = null;
            user.ResetDate = null;
            await _userManager.UpdateAsync(user);
            return user;
        }



        public virtual async Task<User> AdminPasswordReset(string login, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(login);
            if (user == null) throw new InternalServerErrorException("User could not be found");
            user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);
            user.ResetKey = null;
            user.ResetDate = null;
            await _userManager.UpdateAsync(user);
            return user;
        }

        public virtual async Task<User> RequestPasswordReset(string mail)
        {
            var user = await _userManager.FindByEmailAsync(mail);
            if (user == null) return null;
            user.ResetKey = RandomUtil.GenerateResetKey();
            user.ResetDate = DateTime.Now;
            await _userManager.UpdateAsync(user);
            return user;
        }

        public virtual async Task ChangePassword(string currentClearTextPassword, string newPassword)
        {
            var userName = _userManager.GetUserName(_httpContextAccessor.HttpContext.User);
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var currentEncryptedPassword = user.PasswordHash;
                var isInvalidPassword =
                    _passwordHasher.VerifyHashedPassword(user, currentEncryptedPassword, currentClearTextPassword) !=
                    PasswordVerificationResult.Success;
                if (isInvalidPassword) throw new InvalidPasswordException();

                var encryptedPassword = _passwordHasher.HashPassword(user, newPassword);


                user.PasswordHash = encryptedPassword;
                await _userManager.UpdateAsync(user);
                _log.LogDebug($"Changed password for User: {user}");
            }
        }

        public virtual async Task<User> ActivateRegistration(string key)
        {
            _log.LogDebug($"Activating user for activation key {key}");
            var user = await _userManager.Users.SingleOrDefaultAsync(it => it.ActivationKey == key);
            if (user == null) return null;
            user.Activated = true;
            user.ActivationKey = null;
            await _userManager.UpdateAsync(user);

            List<UserRole> userRoles = new List<UserRole>();
            userRoles.Add(new UserRole
            {
                Role = new Role
                {
                    Name = RolesConstants.USER,
                    NormalizedName = RolesConstants.USER
                },
                RoleId = RolesConstants.USER.ToLower()
            });
            await CreateUserRoles(user, userRoles.Select(iur => iur.Role.Name).ToHashSet());
            _log.LogDebug($"Activated user: {user}");
            return user;
        }

        public virtual async Task<User> DeActivateRegistration(string id)
        {
            _log.LogDebug($"DeActivating user with id: {id}");
            var user = await _userManager.Users.SingleOrDefaultAsync(it => it.Id == id);
            if (user == null) return null;
            user.Activated = false;
            user.ActivationKey = RandomUtil.GenerateActivationKey();
            await _userManager.UpdateAsync(user);
            return user;
        }

        public virtual async Task<User> RegisterUser(User userToRegister, string password)
        {
            var existingUser = await _userManager.FindByNameAsync(userToRegister.Login.ToLower());
            if (existingUser != null)
            {
                var removed = await RemoveNonActivatedUser(existingUser);
                if (!removed) throw new LoginAlreadyUsedException();
            }
            if (!string.IsNullOrEmpty(userToRegister.Email))
            {
                existingUser = _userManager.Users.FirstOrDefault(it => it.Email == userToRegister.Email);
                if (existingUser != null)
                {
                    var removed = await RemoveNonActivatedUser(existingUser);
                    if (!removed) throw new EmailAlreadyUsedException();
                }

            }
            var newUser = new User
            {
                Login = userToRegister.Login,
                // new user gets initially a generated password
                PasswordHash = _passwordHasher.HashPassword(null, password),
                FirstName = userToRegister.FirstName,
                LastName = userToRegister.LastName,
                Email = userToRegister.Email?.ToLowerInvariant(),
                PhoneNumber = userToRegister.PhoneNumber,
                ImageUrl = userToRegister.ImageUrl,
                LangKey = userToRegister.LangKey,
                // new user is not active
                //Activated = false,
                Activated = true,
                // new user gets registration key
                //ActivationKey = RandomUtil.GenerateActivationKey()
                //TODO manage authorities
            };
            await _userManager.CreateAsync(newUser);
            List<UserRole> userRoles = new List<UserRole>();
            userRoles.Add(new UserRole
            {
                Role = new Role
                {
                    Name = RolesConstants.USER,
                    NormalizedName = RolesConstants.USER
                },
                RoleId = RolesConstants.USER.ToLower()
            });
            await CreateUserRoles(newUser, userRoles.Select(iur => iur.Role.Name).ToHashSet());
            _log.LogDebug($"Created Information for User: {newUser}");
            return newUser;
        }
        //public virtual async Task UpdateUser(string firstName, string lastName, string email, string langKey, string imageUrl, string phoneNumber);
        public virtual async Task UpdateUser(string email, string phoneNumber)
        {
            var userName = _userManager.GetUserName(_httpContextAccessor.HttpContext.User);
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                user.PhoneNumber = phoneNumber;
                //user.FirstName = firstName;
                // user.LastName = lastName;
                user.Email = email;
                //user.LangKey = langKey;
                //user.ImageUrl = imageUrl;
                await _userManager.UpdateAsync(user);
                _log.LogDebug($"Changed Information for User: {user}");
            }
        }

        public virtual async Task<User> GetUserWithUserRoles()
        {
            var userName = _userManager.GetUserName(_httpContextAccessor.HttpContext.User);
            if (userName == null) return null;
            return await GetUserWithUserRolesByName(userName);
        }

        public virtual IEnumerable<string> GetAuthorities()
        {
            return _roleManager.Roles.Select(it => it.Name).AsQueryable();
        }

        public virtual async Task DeleteUser(string login)
        {
            var user = await _userManager.FindByNameAsync(login);
            if (user != null)
            {
                await DeleteUserRoles(user);
                await _userManager.DeleteAsync(user);
                _log.LogDebug($"Deleted User: {user}");
            }
        }

        public virtual async Task<List<ForgotPasswordMethodRsDTO>> ForgotPasswordMethod(string login)
        {
            var user = await _userManager.FindByNameAsync(login);
            if (user == null) throw new InternalServerErrorException("User could not be found");
            List<ForgotPasswordMethodRsDTO> list = new List<ForgotPasswordMethodRsDTO>();
            if (user.EmailConfirmed)
            {
                list.Add(new ForgotPasswordMethodRsDTO
                {
                    Type = MethodConstants.EMAIL,
                    Value = user.Email
                });
            }

            if (user.PhoneNumberConfirmed)
            {
                list.Add(new ForgotPasswordMethodRsDTO
                {
                    Type = MethodConstants.MOB,
                    Value = user.PhoneNumber
                });
            }

            return list;
        }

        public virtual async Task<User> RequestOTPFWPass(string login, string type, string value)
        {
            User user = null;
            if (type.Equals(MethodConstants.EMAIL))
            {
                user = await _userManager.FindByEmailAsync(value);
                if (user == null) throw new InternalServerErrorException("Method could not be found"); ;
                user.ResetKey = RandomUtil.GenarateOTP();
                user.ResetDate = DateTime.Now;
                await _userManager.UpdateAsync(user);
            }

            // Chưa làm
            if (type.Equals(MethodConstants.MOB))
            {

            }

            return user;
        }

        public virtual async Task<string> CheckOTP(string login, string key)
        {
            _log.LogDebug($"Reset user password for reset key {key}");
            var user = await _userManager.FindByNameAsync(login);
            if (user == null || user.ResetDate <= DateTime.Now.Subtract(120.Seconds()) || !key.Equals(user.ResetKey)) throw new InternalServerErrorException("OTP expire or invalid. Try again.");
            user.ResetKey = null;
            user.ResetKey = RandomUtil.GenerateKeyPassword();
            user.ResetDate = null;
            await _userManager.UpdateAsync(user);
            var value = user.ResetKey;
            return value;
        }

        public virtual async Task<bool> CompleteFwPassWord(string login, string key, string newPassWord)
        {
            _log.LogDebug($"Reset user password for reset key {key}");
            var user = await _userManager.FindByNameAsync(login);
            if (user == null || !key.Equals(user.ResetKey)) throw new InternalServerErrorException("Something wrong. Try again.");
            user.ResetKey = null;
            user.PasswordHash = _passwordHasher.HashPassword(user, newPassWord);
            await _userManager.UpdateAsync(user);
            return true;
        }
        public async Task<bool> EmailConfirmed(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user.EmailConfirmed;
        }
        public virtual async Task<bool> CompleteVerifiedEmail(string login, string key)
        {
            _log.LogDebug($"Verified user email for key {key}");
            var user = await _userManager.FindByNameAsync(login);
            if (user == null || user.ResetDate <= DateTime.Now.Subtract(120.Seconds()) || !key.Equals(user.ResetKey)) throw new InternalServerErrorException("OTP expire or invalid. Try again.");
            user.ResetKey = null;
            user.ResetDate = null;
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);

            var client = new RestClient(_configuration.GetConnectionString("AIO"));
            client.AddDefaultHeader("Authorization", $"Bearer {_configuration.GetValue<string>("Security:TokenDefault")}");
            Object req = null;


            req = new
            {
                Body = _configuration["NotificationTemplate:EmailNotVerifedSuccess:Body"],
                Title = _configuration["NotificationTemplate:EmailNotVerifedSuccess:Title"],
                NotificationTypeId = Guid.Parse(_configuration["NotificationTemplate:EmailNotVerifedSuccess:NotificationTypeId"]),
                SendType = int.Parse(_configuration["NotificationTemplate:EmailNotVerifedSuccess:SendType"]),
                SendTo = user.Id,
                Metadata = _configuration["NotificationTemplate:EmailNotVerifedSuccess:Metadata"],
                Note = _configuration["NotificationTemplate:EmailNotVerifedSuccess:Note"],
                IsTrigger = bool.Parse(_configuration["NotificationTemplate:EmailNotVerifedSuccess:IsTrigger"]),
                CreatedBy = Constants.SystemAccount
            };


            var requestNoti = new RestRequest("/gateway/delivery/notification/add", Method.Post);
            requestNoti.AddJsonBody(req);
            await client.ExecuteAsync(requestNoti);


            return true;
        }

        public string GetReferralCodeByUsername(string name)
        {
            var user = _userManager.Users
                .SingleOrDefault(u => u.UserName.ToLower() == name.ToLower());
            return user.UserName;
        }

        public string GetFullnameByReferralCode(string ReferalCode)
        {
            var user = _userManager.Users
                .SingleOrDefault(u => u.Login.ToLower().Equals(ReferalCode.ToLower()));
            if (user == null)
            {
                return ReferalCode;
            }
            return user.FirstName;
        }
        private async Task<User> GetUserWithUserRolesByName(string name)
        {
            return await _userManager.Users
                .Include(it => it.UserRoles)
                .ThenInclude(r => r.Role)
                .SingleOrDefaultAsync(it => it.UserName == name);
        }

        private async Task<bool> RemoveNonActivatedUser(User existingUser)
        {
            if (existingUser.Activated) return false;

            await _userManager.DeleteAsync(existingUser);
            return true;
        }

        private async Task CreateUserRoles(User user, IEnumerable<string> roles)
        {
            if (roles == null || !roles.Any()) return;

            foreach (var role in roles) await _userManager.AddToRoleAsync(user, role);
        }

        private async Task UpdateUserRoles(User user, IEnumerable<string> roles)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var rolesToRemove = userRoles.Except(roles).ToArray();
            var rolesToAdd = roles.Except(userRoles).Distinct().ToArray();
            await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            await _userManager.AddToRolesAsync(user, rolesToAdd);
        }

        private async Task DeleteUserRoles(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);
        }

        public async Task<bool> CheckExistedUser(string ReferCode)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(p => p.Login.ToLower().Equals(ReferCode.ToLower()));
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public string GetFullnameByUsername(string username)
        {
            var user = _userManager.Users
                .SingleOrDefault(u => u.Login.ToLower().Equals(username.ToLower()));
            if (user == null)
            {
                return username;
            }
            return user.FirstName;
        }

        public async Task<string> GetUserIdByUsername(string username)
        {
            var userid = _userManager.Users
                .Where(u => u.Login.ToLower().Equals(username.ToLower())).Select(p => p.Id).FirstOrDefault();
            if (userid != null)
            {
                return userid;
            }
            return string.Empty;
        }
        public async Task<List<User>> GetListUser()
        {
            var user = _userManager.Users.Where(u => true);
            if (user != null)
            {
                return user.ToList();
            }
            return null;
        }

        public async Task<User> GetUser(string UserId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == UserId);
            if (user == null) return null;
            return user;
        }
    }
}
