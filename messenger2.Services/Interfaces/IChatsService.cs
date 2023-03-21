using messenger2.DataLayer.DTO;
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
    public interface IChatsService
    {
        Task<BaseRepsonse<ChatDTO>> LoadChat(int ChatId, int UserId);

        Task<BaseRepsonse<MessageListDTO>> LoadMoreMessages(int ChatId, int LastMessageId, int UserId);

        Task<BaseRepsonse<ChatListDTO>> GetChatList(int UserId);
    }
}
