using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace messenger2.DataLayer.DTO
{
    public class MessageBriefInfoDTO
    {
        public int MessageId { get; set; }
        public string Nickname { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public bool IsSystem { get; set; }
        public bool IsMine { get; set; }
    }

}
