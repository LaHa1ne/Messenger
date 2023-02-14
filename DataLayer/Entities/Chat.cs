using DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Chat
    {
        public Chat()
        {
            ChatMembers = new HashSet<User>();
        }

        public int ChatId { get; set; }
        public string Name { get; set; } = null!;
        public ChatType Type { get; set; }
        public int ChatCreatorId { get; set; }
        public virtual User ChatCreator { get; set; } = null!;
        public virtual ICollection<User> ChatMembers { get; set; }
    }
}
