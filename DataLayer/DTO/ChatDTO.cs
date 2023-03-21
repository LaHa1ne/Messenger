using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace messenger2.DataLayer.DTO
{
    public class ChatDTO
    {
        public ChatDTO(int num_messages, int num_members) 
        {
            Messages = new List<MessageBriefInfoDTO>(num_messages+1);
            Members = new List<UserBriefInfoDTO>(num_members);
        }
        public int ChatId { get; set; }
        public string Name { get; set; }
        public List<MessageBriefInfoDTO> Messages { get; set; }

        public List<UserBriefInfoDTO> Members { get; set; }

        public bool HasMoreMessages { get; set; }

    }
}
