﻿using AutoMapper;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Jhipster.Domain;
using Jhipster.Dto;
using Jhipster.Web.Extensions;
using Jhipster.Web.Filters;
using Jhipster.Web.Rest.Problems;
using Jhipster.Configuration;
using Jhipster.Crosscutting.Constants;
using Jhipster.Crosscutting.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Jhipster.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Jhipster.Dto.Authentication;
using Newtonsoft.Json;
using Org.BouncyCastle.Security;

namespace Jhipster.Controllers
{

    [Route("api")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _log;
        private readonly IMapper _userMapper;
        private readonly IMailService _mailService;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;

        public AccountController(ILogger<AccountController> log, UserManager<User> userManager, IUserService userService,
            IMapper userMapper, IMailService mailService)
        {
            _log = log;
            _userMapper = userMapper;
            _userManager = userManager;
            _userService = userService;
            _mailService = mailService;
        }

        /// <summary>
        /// Đăng ký tài khoản (user)
        /// </summary>
        /// <param name="managedUserDto"></param>
        /// <returns></returns>
        /// <exception cref="InvalidPasswordException"></exception>
        [HttpPost("register")]
        [ValidateModel]
        public async Task<IActionResult> RegisterAccount([FromBody] ManagedUserDto managedUserDto)
        {
            if (!CheckPasswordLength(managedUserDto.Password)) throw new InvalidPasswordException();
            var user = await _userService.RegisterUser(_userMapper.Map<User>(managedUserDto), managedUserDto.Password);
            return CreatedAtAction(nameof(GetAccount), user);
        }

        /// <summary>
        /// Kích hoạt tài khoản
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="InternalServerErrorException"></exception>
        [HttpGet("activate")]
        [ValidateModel]
        public async Task ActivateAccount([FromQuery(Name = "key")] string key)
        {
            var user = await _userService.ActivateRegistration(key);
            if (user == null) throw new InternalServerErrorException("Not user was found for this activation key");
        }

        /// <summary>
        ///   Khóa tài khoản
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="InternalServerErrorException"></exception>
        [HttpGet("Deactivate")]
        [ValidateModel]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task DeActivateAccount([FromQuery(Name = "id")] string id)
        {
            var user = await _userService.DeActivateRegistration(id);
            if (user == null) throw new InternalServerErrorException("Not user was found for this id");
        }

        /// <summary>
        /// Kiểm tra tài khoản được xác thực
        /// </summary>
        /// <returns></returns>
        [HttpGet("authenticate")]
        public string IsAuthenticated()
        {
            _log.LogDebug("REST request to check if the current user is authenticated");
            return _userManager.GetUserName(User);
        }

        /// <summary>
        /// Lấy tên nhóm quyền (Role)
        /// </summary>
        /// <returns></returns>
        [HttpGet("authorities")]
        public ActionResult<IEnumerable<string>> GetAuthorities()
        {
            return Ok(_userService.GetAuthorities());
        }

        /// <summary>
        /// validate User ? 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InternalServerErrorException"></exception>
        [AllowAnonymous]
        [HttpGet("validateUser")]
        public async Task<IActionResult> ValidateUser(string User, string ReferralUser)
        {
            var existingUser = await _userManager.FindByNameAsync(User.ToLower());

            if (existingUser != null)
            {
                throw new LoginAlreadyUsedException();
            }

            var existingReferralUser = await _userManager.FindByNameAsync(ReferralUser.ToLower());

            if (existingReferralUser == null)
            {
                throw new Exception("ReferralUser could not be found");
            }

            return Ok(true);
        }

        /// <summary>
        /// Lấy thông tin cơ bản của người dùng (account)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InternalServerErrorException"></exception>
        [Authorize]
        [HttpGet("account")]
        public async Task<ActionResult<UserDto>> GetAccount()
        {
            var user = await _userService.GetUserWithUserRoles();
            if (user == null) throw new InternalServerErrorException("User could not be found");
            var userDto = _userMapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        /// <summary>
        /// Thay đổi mật khẩu
        /// </summary>
        /// <param name="passwordChangeDto"></param>
        /// <returns></returns>
        /// <exception cref="InvalidPasswordException"></exception>
        [Authorize]
        [HttpPost("account/change-password")]
        [ValidateModel]
        public async Task<ActionResult> ChangePassword([FromBody] PasswordChangeDto passwordChangeDto)
        {
            if (!CheckPasswordLength(passwordChangeDto.NewPassword)) throw new InvalidPasswordException();
            if (passwordChangeDto.CurrentPassword.Equals(passwordChangeDto.NewPassword))
                throw new PasswordException("Something wrong!!");
            await _userService.ChangePassword(passwordChangeDto.CurrentPassword, passwordChangeDto.NewPassword);
            return Ok();
        }

        /// <summary>
        /// [Reset Password] Step 1. Gửi yêu cầu đặt lại mật khẩu (user)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="EmailNotFoundException"></exception>
        [HttpPost("account/reset-password/init")]
        public async Task<ActionResult> RequestPasswordReset()
        {
            var mail = await Request.BodyAsStringAsync();
            var user = await _userService.RequestPasswordReset(mail);
            if (user == null) throw new EmailNotFoundException();
            await _mailService.SendPasswordResetMail(user);
            return Ok();
        }

        /// <summary>
        /// [Reset Password] Step 2. Hoàn thành đặt lại mật khẩu (user)
        /// </summary>
        /// <param name="keyAndPasswordDto"></param>
        /// <returns></returns>
        /// <exception cref="InvalidPasswordException"></exception>
        /// <exception cref="InternalServerErrorException"></exception>
        [HttpPost("account/reset-password/finish")]
        [ValidateModel]
        public async Task RequestPasswordReset([FromBody] KeyAndPasswordDto keyAndPasswordDto)
        {
            if (!CheckPasswordLength(keyAndPasswordDto.NewPassword)) throw new InvalidPasswordException();
            var user = await _userService.CompletePasswordReset(keyAndPasswordDto.NewPassword, keyAndPasswordDto.Key);
            if (user == null) throw new InternalServerErrorException("No user was found for this reset key");
        }

        /// <summary>
        /// [Forgot Password] Step 1. Lấy các phương thức để gửi OTP khi quên mật khẩu
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>
        [HttpGet("account/forgot-password-method")]
        public async Task<ActionResult<ForgotPasswordMethodRsDTO>> MethodForgotPassord(string Login)
        {
            _log.LogDebug($"REST request to get method forgot Password : {Login}");
            try
            {
                var method = await _userService.ForgotPasswordMethod(Login);
                return Ok(method);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// [Forgot Password] Step 2. Yêu cầu gửi mã OTP về phương thức được gọi ở Step 1
        /// </summary>
        /// <param name="forgotPasswordOTPRqDTO"></param>
        /// <returns></returns>
        [HttpPost("account/forgot-password-Getotp")]
        public async Task<ActionResult> OTPForgotPassword([FromBody] ForgotPasswordOTPRqDTO forgotPasswordOTPRqDTO)
        {
            _log.LogDebug($"REST request to get otp forgot Password : {JsonConvert.SerializeObject(forgotPasswordOTPRqDTO)}");
            try
            {
                var user = await _userService.RequestOTPFWPass(forgotPasswordOTPRqDTO.Login, forgotPasswordOTPRqDTO.Type, forgotPasswordOTPRqDTO.Value);
                if (forgotPasswordOTPRqDTO.Type.Equals(MethodConstants.EMAIL))
                    await _mailService.SendPasswordForgotOTPMail(user);

                // Chưa xử lý
                if (forgotPasswordOTPRqDTO.Type.Equals(MethodConstants.MOB))
                {

                }

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// [Forgot Password] Step 3. Kiểm tra mã OTP lấy ở Step 2 
        /// </summary>
        /// <param name="forgotPasswordResetRqDTO"></param>
        /// <returns></returns>
        [HttpPost("account/forgot-password-CheckOtp")]
        public async Task<ActionResult> ForgotPasswordCheckOTP([FromBody] ForgotPasswordResetRqDTO forgotPasswordResetRqDTO)
        {
            _log.LogDebug($"REST request to forgot password Reset : {JsonConvert.SerializeObject(forgotPasswordResetRqDTO)}");
            try
            {
                var response = await _userService.CheckOTP(forgotPasswordResetRqDTO.Login, forgotPasswordResetRqDTO.Key);
                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// [Forgot Password] Step 4. nhập mật khẩu mới
        /// </summary>
        /// <param name="forgotPasswordResetRqDTO"></param>
        /// <returns></returns>
        [HttpPost("account/forgot-password-complete")]
        public async Task<ActionResult> CompleteForgotPassword([FromBody] ForgotPasswordCompleteRpDTO forgotPasswordCompleteRpDTO)
        {
            _log.LogDebug($"REST request to forgot password Reset : {JsonConvert.SerializeObject(forgotPasswordCompleteRpDTO)}");
            try
            {
                var response = await _userService.CompleteFwPassWord(forgotPasswordCompleteRpDTO.Login, forgotPasswordCompleteRpDTO.Key, forgotPasswordCompleteRpDTO.NewPassword);

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// [verified Email] Step 1. Yêu cầu gửi mã OTP 
        /// </summary>
        /// /// <param name="verifiedEmailRqDTO"></param>
        /// <returns></returns>
        [HttpPost("account/verified-email-otp")]
        public async Task<IActionResult> OTPVerifiedEmail([FromBody] VerifiedEmailRqDTO verifiedEmailRqDTO)
        {
            _log.LogError($"REST request to get otp verified Email : {JsonConvert.SerializeObject(verifiedEmailRqDTO)}");
            try
            {
                var user = await _userService.RequestOTPFWPass(verifiedEmailRqDTO.Login, "Email", verifiedEmailRqDTO.Value);
                //await _mailService.SendConfirmedOTPMail(user);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// [verified Email] Step 2. Kiểm tra mã OTP lấy ở Step 1 và xác minh tài khoản email
        /// </summary>
        /// <param name="verifiedEmailDTO"></param>
        /// <returns></returns>
        [HttpPost("account/verified-email-complete")]
        public async Task<IActionResult> CompleteOTPVerifiedEmail([FromBody] VerifiedEmailDTO verifiedEmailDTO)
        {
            _log.LogError($"REST request to verify Email : {JsonConvert.SerializeObject(verifiedEmailDTO)}");
            try
            {
                var response = await _userService.CompleteVerifiedEmail(verifiedEmailDTO.Login, verifiedEmailDTO.Key);
                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// check confirm Email: check tài khoản xác thực hay chưa
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpGet("account/check-confirmed-email")]
        public async Task<IActionResult> CheckConfirmedEmail(string email)
        {
            _log.LogError($"REST request to check confirmed Email : {JsonConvert.SerializeObject(email)}");
            try
            {
                var response = await _userService.EmailConfirmed(email);
                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        private static bool CheckPasswordLength(string password)
        {
            return !string.IsNullOrEmpty(password) &&
                   password.Length >= ManagedUserDto.PasswordMinLength &&
                   password.Length <= ManagedUserDto.PasswordMaxLength;
        }
    }
}
