using messenger2.DataLayer.Responses;
using messenger2.DataLayer.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace messenger2.Services.Interfaces
{
    public interface IAccountService
    {
        Task<BaseRepsonse<ClaimsIdentity>> Register(RegistrationViewModel registrationModel);

        Task<BaseRepsonse<ClaimsIdentity>> Login(LoginViewModel loginModel);
    }
}
