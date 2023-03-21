using messenger2.DataLayer.DTO;
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
        public int StatusCode { get; set; }
        public string Description { get; set; }
    }
}
