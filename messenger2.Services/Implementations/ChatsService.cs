using DataAccessLayer.Interfaces;
using DataLayer.Entities;
using messenger2.DataAccessLayer.Interfaces;
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
    public class ChatsService : IChatsService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;

        public ChatsService(IChatRepository chatRepository, IUserRepository userRepository)
        {
            _chatRepository = chatRepository;
            _userRepository = userRepository;
        }

        public async Task<BaseRepsonse<ChatDTO>> LoadChat(int ChatId, int UserId)
        {
            try
            {
                var chatDTO = await _chatRepository.LoadChat(ChatId, num_messages_to_load:4, UserId);

                return new BaseRepsonse<ChatDTO>()
                {
                    Data = chatDTO,
                    Description = "Инф. о чате успешно получена",
                    StatusCode = DataLayer.Enums.StatusCode.OK
                };
            }

            catch (Exception ex)
            {
                return new BaseRepsonse<ChatDTO>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseRepsonse<ChatDTO>> LoadPersonalChatWithFriend(int FriendId, int UserId)
        {
            try
            {
                var user = await _userRepository.GetByUserId(UserId);
                var friend = await _userRepository.GetByUserId(FriendId);
                var chatDTO = await _chatRepository.LoadPersonalChatWithFriend(user, num_messages_to_load: 4, friend);

                return new BaseRepsonse<ChatDTO>()
                {
                    Data = chatDTO,
                    Description = "Инф. о чате успешно получена",
                    StatusCode = DataLayer.Enums.StatusCode.OK
                };
            }

            catch (Exception ex)
            {
                return new BaseRepsonse<ChatDTO>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseRepsonse<MessageListDTO>> LoadMoreMessages(int ChatId, int FirstMessageId, int UserId)
        {
            try
            {
                var messageListDTO = await _chatRepository.LoadMoreMessages(ChatId,num_messages_to_load:2, FirstMessageId, UserId);

                return new BaseRepsonse<MessageListDTO>()
                {
                    Data = messageListDTO,
                    Description = "Инф. о сообщениях успешно получена",
                    StatusCode = DataLayer.Enums.StatusCode.OK
                };
            }

            catch (Exception ex)
            {
                return new BaseRepsonse<MessageListDTO>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }


        public async Task<BaseRepsonse<bool>> AddMessage(int ChatId, Message message)
        {
            try
            {
                var isSuccess = await _chatRepository.AddMessage(ChatId, message);

                return new BaseRepsonse<bool>()
                {
                    Data = isSuccess,
                    Description = "Запрос на добавление сообщения обработан",
                    StatusCode = DataLayer.Enums.StatusCode.OK
                };
            }

            catch (Exception ex)
            {
                return new BaseRepsonse<bool>()
                {
                    Data=false,
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseRepsonse<ChatListDTO>> GetChatList(int UserId)
        {
            try
            {
                var personal_chats = await _chatRepository.GetPersonalChats(UserId);
                var group_chats = await _chatRepository.GetGroupChats(UserId);

                return new BaseRepsonse<ChatListDTO>()
                {
                    Data = new ChatListDTO(personal_chats.Count(), group_chats.Count())
                    {
                        Personal_chats = (List<ChatBriefInfoDTO>) personal_chats,
                        Group_chats = (List<ChatBriefInfoDTO>) group_chats
                    },
                    Description = "Чаты успешно загружены",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseRepsonse<ChatListDTO>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
