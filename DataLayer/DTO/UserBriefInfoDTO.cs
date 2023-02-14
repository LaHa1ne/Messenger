using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace messenger2.DataLayer.DTO
{
    public class UserBriefInfoDTO
    {
        public UserBriefInfoDTO(int UserId, string Nickname) 
        {
            this.UserId = UserId;
            this.Nickname = Nickname;
        }
        public int UserId { get; set; }
        public string Nickname { get; set; }
    }
}
