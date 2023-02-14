using DataAccessLayer.Interfaces;
using DataLayer.Entities;
using messenger2.DataLayer.Enums;
using messenger2.DataLayer.Helpers;
using messenger2.DataLayer.Responses;
using messenger2.DataLayer.ViewModels.Account;
using messenger2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace messenger2.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseRepsonse<ClaimsIdentity>> Register(RegistrationViewModel registrationModel)
        {
            try
            {
                var emailExists = await _userRepository.IsUserExistsByEmail(registrationModel.Email);
                var nicknameExists = await _userRepository.IsUserExistsByNickname(registrationModel.Nickname);
                if (emailExists || nicknameExists)
                {
                    string Description = emailExists && nicknameExists ? "Имя пользователя и эл. почта уже заняты" : emailExists ? "Эл. почта уже занята" : "Имя пользователя уже занято";
                    return new BaseRepsonse<ClaimsIdentity>()
                    {
                        Description = Description,
                        StatusCode = DataLayer.Enums.StatusCode.UserExists
                    };
                }

                var user = new User()
                {
                    Email = registrationModel.Email,
                    Nickname = registrationModel.Nickname,
                    Password = HashPasswordHelper.GetHashPassword(registrationModel.Password)
                };

                await _userRepository.Create(user);
                var result = Authenticate(user);

                return new BaseRepsonse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "Аккаунт создан",
                    StatusCode = DataLayer.Enums.StatusCode.OK
                };
            }

            catch (Exception ex)
            {
                return new BaseRepsonse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseRepsonse<ClaimsIdentity>> Login(LoginViewModel loginModel)
        {
            try
            {
                var user = await _userRepository.GetByEmailAndPassword(loginModel.Email, HashPasswordHelper.GetHashPassword(loginModel.Password));
                if (user == null)
                {
                    return new BaseRepsonse<ClaimsIdentity>() { Description = "Неправильная почта или пароль" };
                }
                var result = Authenticate(user);

                return new BaseRepsonse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseRepsonse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Nickname),
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString())
            };
            return new ClaimsIdentity(claims, authenticationType: "ApplicationCookie",
                ClaimsIdentity.DefaultRoleClaimType, "UserId");
        }
    }
}
