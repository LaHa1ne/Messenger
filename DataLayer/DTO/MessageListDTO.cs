using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace messenger2.DataLayer.DTO
{
    public class MessageListDTO
    {
        public MessageListDTO(int num_messages) 
        {
            Messages = new List<MessageBriefInfoDTO>(num_messages);
        }
        public List<MessageBriefInfoDTO> Messages { get; set; }

        public bool HasMoreMessages { get; set; }


    }
}
