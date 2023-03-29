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
        Task<BaseRepsonse<IEnumerable<UserBriefInfoDTO>>> GetSenders(int UserId);

        Task<BaseRepsonse<bool>> AcceptFriendRequest(int UserId, int SenderId);
        Task<BaseRepsonse<bool>> RejectFriendRequest(int UserId, int SenderId);
        Task<BaseRepsonse<bool>> SendFriendRequest(int SenderId, string UserNickname);
        Task<BaseRepsonse<bool>> DeleteFriend(int UserId, int FriendId);
    }
}
