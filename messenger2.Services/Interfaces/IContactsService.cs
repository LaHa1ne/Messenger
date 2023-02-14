using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using messenger2.DataLayer.DTO;
using messenger2.DataLayer.Responses;

namespace messenger2.Services.Interfaces
{
    public interface IContactsService
    {
        Task<BaseRepsonse<IEnumerable<UserBriefInfoDTO>>> GetFriends(int UserId);
    }
}
