using DataAccessLayer.Interfaces;
using DataLayer.Entities;
using messenger2.DataLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace messenger2.DataAccessLayer.Interfaces
{
    public interface IChatRepository : IBaseRepository<Chat>
    {
        Task<ChatDTO> LoadChat(int ChatId, int num_messages_to_load, int UserId);

        Task<MessageListDTO> LoadMoreMessages(int ChatId, int num_messages_to_load, int LastMessageId, int UserId);

        Task<bool> AddMessage(int ChatId, Message message);

        Task<IEnumerable<ChatBriefInfoDTO>> GetPersonalChats(int UserId);

        Task<IEnumerable<ChatBriefInfoDTO>> GetGroupChats(int UserId);

    }
}
