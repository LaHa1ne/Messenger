using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class User
    {
        public User()
        {
            Chats = new HashSet<Chat>();
            Messages = new HashSet<Message>();
            Friends = new HashSet<User>();
            Senders = new HashSet<User>();
        }

        public int UserId { get; set; }
        public string Nickname { get; set; } = null!;
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Chat> CreatedChats { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
        public virtual ICollection<Message> Messages { get; }
        public virtual ICollection<User> Friends { get; set; }
        public virtual ICollection<User> FriendsNavigation { get; set; }
        public virtual ICollection<User> Senders { get; set; }
        public virtual ICollection<User> SendersNavigation { get; set; }
    }
}
