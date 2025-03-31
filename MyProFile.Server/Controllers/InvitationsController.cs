using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProFile.Data;
using MyProFile.Server.Utilities;

namespace MyProFile.Server.Controllers
{
    // Само админ може да праща покани
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class InvitationsController : ControllerBase
    {
        private readonly MyProFileDbContext _context;
        private readonly MailHelper _mailHelper;

        public InvitationsController(MyProFileDbContext context, MailHelper mailHelper)
        {
            _context = context;
            _mailHelper = mailHelper;
        }

    }
}
