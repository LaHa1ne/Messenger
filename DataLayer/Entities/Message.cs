using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Message
    {
        public int MessageId { get; set; }
        public int ChatId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public bool IsSystem { get; set; }
        public bool IsDeleted { get; set; }
        public int MessageCreatorId { get; set; }

        public virtual Chat Chat { get; set; } = null!;
        public virtual User MessageCreator { get; set; } = null!;
    }
}
