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
                return new BaseRepsonse<IEnumerable<UserBriefInfoDTO>>()
                {
                    Data = Friends,
                    Description = Friends.Count() == 0 ? "Пользователь не имеет друзей" : "Список друзей получен",
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
                return new BaseRepsonse<IEnumerable<UserBriefInfoDTO>>()
                {
                    Data = Senders,
                    Description = Senders.Count() == 0 ? "Пользователь не имеет приглашений" : "Список приглашений получен",
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

        public async Task<BaseRepsonse<bool>> AcceptFriendRequest(int UserId, int SenderId)
        {
            try
            {
                return new BaseRepsonse<bool>()
                {
                    Data = await _userRepository.AcceptFriendRequest(UserId, SenderId),
                    Description = "Приглашение в друзья принято",
                    StatusCode = DataLayer.Enums.StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseRepsonse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<BaseRepsonse<bool>> RejectFriendRequest(int UserId, int SenderId)
        {
            try
            {
                return new BaseRepsonse<bool>()
                {
                    Data = await _userRepository.RejectFriendRequest(UserId, SenderId),
                    Description = "Приглашение в друзья отклонено",
                    StatusCode = DataLayer.Enums.StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseRepsonse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<BaseRepsonse<bool>> SendFriendRequest(int SenderId, string UserNickname)
        {
            try
            {
                var sender = await _userRepository.GetByUserId(SenderId);
                if (sender.Nickname == UserNickname)
                {
                    return new BaseRepsonse<bool>()
                    {
                        Data = false,
                        Description = "Нельзя добавить в друзья себя",
                        StatusCode = DataLayer.Enums.StatusCode.OK
                    };
                }

                var IsSuccess = await _userRepository.SendFriendRequest(SenderId, UserNickname);
                return new BaseRepsonse<bool>()
                {
                    Data = IsSuccess,
                    Description = IsSuccess ? "Запрос успешно отправлен" : "Пользователя с таким никнеймом не существует",
                    StatusCode = DataLayer.Enums.StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseRepsonse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseRepsonse<bool>> DeleteFriend(int UserId, int FriendId)
        {
            try
            {
                return new BaseRepsonse<bool>()
                {
                    Data = await _userRepository.DeleteFriend(UserId, FriendId),
                    Description = "Пользователь удален из друзей",
                    StatusCode = DataLayer.Enums.StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseRepsonse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
