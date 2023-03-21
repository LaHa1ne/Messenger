﻿using messenger2.DataLayer.DTO;
using messenger2.DataLayer.Responses;
using messenger2.DataLayer.ViewModels.Chats;
using messenger2.DataLayer.ViewModels.Contacts;
using messenger2.Services.Implementations;
using messenger2.Services.Interfaces;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace messenger2.Controllers
{
    public class ChatsController : Controller
    {
        private readonly IChatsService _chatsService;

        public ChatsController(IChatsService chatsService)
        {
            _chatsService = chatsService;
        }

        [HttpGet]
        public async Task<IActionResult> Chats(int ChatId)
        {
            int UserId = int.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var response_ChatList = await _chatsService.GetChatList(UserId);

            var ChatInfo = new ChatsViewModel();
            if (response_ChatList.StatusCode!= DataLayer.Enums.StatusCode.OK)
            {
                ChatInfo.StatusCode = 502;
                ChatInfo.Description = response_ChatList.Description;
                return View(ChatInfo);
            }
            ChatInfo.ChatList = response_ChatList.Data;
            ChatInfo.Description= response_ChatList.Description;

            if (ChatId!=0)
            {
                var response_SelectedChat = await _chatsService.LoadChat(ChatId, UserId);
                if (response_SelectedChat.StatusCode != DataLayer.Enums.StatusCode.OK)
                {
                    ChatInfo.StatusCode = 502;
                    ChatInfo.Description = response_SelectedChat.Description;
                    return View(ChatInfo);
                }
                ChatInfo.SelectedChat = response_SelectedChat.Data;
                ChatInfo.Description += "/n"+response_ChatList.Description;
            }
            ChatInfo.StatusCode = 200;
            return View(ChatInfo);
        }

        [HttpPost]
        public async Task<JsonResult> LoadSelectedChat([FromBody] ChatIdDTO data)
        {
            int UserId = int.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var response = await _chatsService.LoadChat(data.ChatId, UserId);
            if (response.StatusCode == DataLayer.Enums.StatusCode.OK)
            {
                return Json(response);
                //return Json(response.Data);
                /*return Json(new SelectedChatDTO()
                {
                    ChatId = data.ChatId,
                    Name = response.Data.Name,
                    MessageList = a.ViewData,
                    MembersList = PartialView("_ChatMembers", response.Data.Members),
                });*/
                /*return Json(new
                {
                    Name = response.Data.Name,
                    MessageList = PartialView("_SelectedChat", response.Data.Messages),
                    MembersList = PartialView("_ChatMembers", response.Data.Members),
                    StatusCode = 200,
                    Description = response.Description
                });*/
                /*var res = new BaseRepsonse<int>()
                {
                    Data = 1,
                    Description = "Приглашение отправлено",
                    StatusCode = DataLayer.Enums.StatusCode.OK
                };
                return Json(res);*/
            }
            return Json(new
            {
                StatusCode = 502,
                Description = response.Description
            });
        }
    }
}