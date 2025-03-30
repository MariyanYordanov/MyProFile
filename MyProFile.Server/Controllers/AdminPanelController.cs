using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyProFile.Server.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminPanelController : ControllerBase
    {
        [HttpGet("dashboard")]
        public IActionResult GetDashboard()
        {
            return Ok("Достъп до админ панела разрешен.");
        }

        [HttpPost("create-student")]
        public IActionResult CreateStudent()
        {
            // TODO: логика за създаване на ученик
            return Ok("Ученикът е създаден успешно.");
        }

        [HttpDelete("delete-student/{id}")]
        public IActionResult DeleteStudent(int id)
        {
            // TODO: логика за изтриване на ученик
            return Ok($"Ученик с ID {id} е изтрит.");
        }

        // Тестови метод за роля и токен
        [HttpGet("secure-test")]
        public IActionResult SecureTest()
        {
            return Ok("Само админ вижда това.");
        }
    }
}
