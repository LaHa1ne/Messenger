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
                if (Friends.Count()==0)
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

        public async Task<BaseRepsonse<IEnumerable<UserBriefInfoDTO>>> GetSenders(int UserId)
        {
            try
            {
                var Senders = await _userRepository.GetSenders(UserId);
                if (Senders.Count() == 0)
                {
                    return new BaseRepsonse<IEnumerable<UserBriefInfoDTO>>()
                    {
                        Description = "У вас нет приглашений",
                        StatusCode = DataLayer.Enums.StatusCode.UsersNotExists
                    };
                }

                return new BaseRepsonse<IEnumerable<UserBriefInfoDTO>>()
                {
                    Data = Senders,
                    Description = "Приглашения найдены",
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

        public async Task<BaseRepsonse<int>> AcceptFriendRequest(int UserId, int SenderId)
        {
            try
            {
                var num_senders = await _userRepository.AcceptFriendRequest(UserId, SenderId);

                return new BaseRepsonse<int>()
                {
                    Data = num_senders,
                    Description = "Приглашение в друзья принято",
                    StatusCode = DataLayer.Enums.StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseRepsonse<int>()
                {
                    Data = -1,
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<BaseRepsonse<int>> RejectFriendRequest(int UserId, int SenderId)
        {
            try
            {
                var num_senders = await _userRepository.RejectFriendRequest(UserId, SenderId);

                return new BaseRepsonse<int>()
                {
                    Data = num_senders,
                    Description = "Приглашение в друзья отклонено",
                    StatusCode = DataLayer.Enums.StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseRepsonse<int>()
                {
                    Data = -1,
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<BaseRepsonse<int>> SendFriendRequest(int SenderId, string UserNickname)
        {
            try
            {
                var is_exist = await _userRepository.SendFriendRequest(SenderId, UserNickname);

                return new BaseRepsonse<int>()
                {
                    Data = is_exist?1:0,
                    Description = is_exist ? "Приглашение отправлено" : "Пользователя с таким никнеймом не существует",
                    StatusCode = DataLayer.Enums.StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseRepsonse<int>()
                {
                    Data = -1,
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseRepsonse<int>> DeleteFriend(int UserId, int FriendId)
        {
            try
            {
                var num_friends = await _userRepository.DeleteFriend(UserId, FriendId);

                return new BaseRepsonse<int>()
                {
                    Data = num_friends,
                    Description = "Пользователь удален из друзей",
                    StatusCode = DataLayer.Enums.StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseRepsonse<int>()
                {
                    Data = -1,
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
