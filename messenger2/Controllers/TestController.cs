using messenger2.DataLayer.DTO;
using Microsoft.AspNetCore.Mvc;

namespace messenger2.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult MyJson([FromBody] UserIdDTO data)
        {
            var id = data.UserId;
            
            return Json(new {se="ss"});
        }
    }
}
