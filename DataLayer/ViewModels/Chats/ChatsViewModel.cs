using messenger2.DataLayer.DTO;
using messenger2.DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace messenger2.DataLayer.ViewModels.Chats
{
    public class ChatsViewModel
    {
        public ChatListDTO ChatList { get; set; }

        public ChatDTO SelectedChat { get; set; }
        public StatusCode StatusCode { get; set; }
        public string Description { get; set; }
    }
}
