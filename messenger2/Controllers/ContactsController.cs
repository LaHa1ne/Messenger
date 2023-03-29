using DataLayer.Entities;
using messenger2.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using messenger2.DataLayer.ViewModels.Contacts;
using System.Text.Json;
using System.Text.Json.Serialization;
using messenger2.Models;
using messenger2.DataLayer.DTO;
using Microsoft.EntityFrameworkCore.Diagnostics;
using messenger2.DataLayer.Responses;
using Microsoft.AspNetCore.Authorization;

namespace messenger2.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
        private readonly IContactsService _contactsService;

        public ContactsController(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }


        [HttpGet]
        public async Task<IActionResult> Contacts()
        {
            int UserId = int.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var response = await _contactsService.GetFriends(UserId);


            var UsersInfo = new UsersBriefInfoViewModel()
            {
                UsersInfo = response.Data,
                StatusCode = response.StatusCode switch
                {
                    DataLayer.Enums.StatusCode.OK => 0,
                    DataLayer.Enums.StatusCode.UsersNotExists => 1,
                    _ => 2
                },
                Description = response.Description
            };

            return View(UsersInfo);
        }

        [HttpGet]
        public async Task<IActionResult> GetFriendsList()
        {
            int UserId = int.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var response = await _contactsService.GetFriends(UserId);

            var UsersInfo = new UsersBriefInfoViewModel()
            {
                UsersInfo = response.Data,
                StatusCode = (int)response.StatusCode,
                Description = response.Description
            };

            return PartialView("_FriendsList", UsersInfo);
        }

        [HttpGet]
        public async Task<IActionResult> GetSendersList()
        {
            int UserId = int.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var response = await _contactsService.GetSenders(UserId);


            var UsersInfo = new UsersBriefInfoViewModel()
            {
                UsersInfo = response.Data,
                StatusCode = (int)response.StatusCode,
                Description = response.Description
            };

            return PartialView("_SendersList", UsersInfo);
        }

        [HttpGet]
        public async Task<IActionResult> AddFriendMenu()
        {
            return PartialView("_AddFriend");
        }

        [HttpPost]
        public async Task<JsonResult> AcceptFriendRequest([FromBody] UserIdDTO data)
        {
            int UserId = int.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var response = await _contactsService.AcceptFriendRequest(UserId, SenderId: Convert.ToInt32(data.UserId));

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> RejectFriendRequest([FromBody] UserIdDTO data)
        {
            int UserId = int.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var response = await _contactsService.RejectFriendRequest(UserId, SenderId: Convert.ToInt32(data.UserId));

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> SendFriendRequest([FromBody] UserNicknameDTO data)
        {
            int SenderId = int.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var response = await _contactsService.SendFriendRequest(SenderId, UserNickname: data.Nickname);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteFriend([FromBody] UserIdDTO data)
        {
            int UserId = int.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var response = await _contactsService.DeleteFriend(UserId, FriendId: Convert.ToInt32(data.UserId));

            return Json(response);
        }

    }
}
