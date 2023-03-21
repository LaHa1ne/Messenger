using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace messenger2.DataLayer.DTO
{
    public class ChatBriefInfoDTO
    {
        public int ChatId { get; set; }
        public string Name { get; set; }
        public string TextLastMessage { get; set; }
        public DateTime DateLastMessage { get; set; }

    }
}
