using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace messenger2.DataLayer.DTO
{
    public class ChatListDTO
    {
        public ChatListDTO(int num_personal_chats, int num_group_chats)
        {
            Personal_chats = new List<ChatBriefInfoDTO>(num_personal_chats);
            Group_chats = new List<ChatBriefInfoDTO>(num_group_chats);
        }
        public List<ChatBriefInfoDTO> Personal_chats { get; set; }
        public List<ChatBriefInfoDTO> Group_chats { get; set; }
    }
}
