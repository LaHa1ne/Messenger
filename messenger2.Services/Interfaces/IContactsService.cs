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

        Task<BaseRepsonse<int>> AcceptFriendRequest(int UserId, int SenderId);
        Task<BaseRepsonse<int>> RejectFriendRequest(int UserId, int SenderId);
        Task<BaseRepsonse<int>> SendFriendRequest(int SenderId, string UserNickname);
        Task<BaseRepsonse<int>> DeleteFriend(int UserId, int FriendId);
    }
}
