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

namespace messenger2.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactsService _contactsService;

        public ContactsController(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }


        [HttpGet]
        public async Task<IActionResult> Friends()
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

        [HttpPost]
        //[IgnoreAntiforgeryToken]
        public JsonResult DeleteFriend([FromBody] UserIdDTO data)
        {

            var str = data.UserId;

            return Json(new { success=true});
        }

        [HttpPost]
        //[IgnoreAntiforgeryToken]
        public JsonResult MyJson([FromBody] UserIdDTO data)
        {
            var id = data.UserId;
            return Json(new { el = "abc" });
        }

        [HttpPost]
        public JsonResult AjaxTest2(string testStr)
        {

            return Json("Сервер получил данные: " + testStr);

        }

    }
}
