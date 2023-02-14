using DataAccessLayer.Interfaces;
using DataLayer.Entities;
using messenger2.DataLayer.DTO;
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
    public class ContactsService : IContactsService
    {
        private readonly IUserRepository _userRepository;

        public ContactsService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseRepsonse<IEnumerable<UserBriefInfoDTO>>> GetFriends(int UserId)
        {
            try
            {
                var Friends = await _userRepository.GetFriends(UserId);
                if (Friends == null)
                {
                    return new BaseRepsonse<IEnumerable<UserBriefInfoDTO>>()
                    {
                        Description = "У вас нет друзей :с",
                        StatusCode = DataLayer.Enums.StatusCode.UsersNotExists
                    };
                }

                return new BaseRepsonse<IEnumerable<UserBriefInfoDTO>>()
                {
                    Data = Friends,
                    Description = "Друзья найдены",
                    StatusCode = DataLayer.Enums.StatusCode.OK
                };
            }

            catch (Exception ex)
            {
                return new BaseRepsonse<IEnumerable<UserBriefInfoDTO>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
