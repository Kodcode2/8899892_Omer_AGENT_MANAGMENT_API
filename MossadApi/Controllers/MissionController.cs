using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MossadApi.DAL;
namespace MossadApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly ILogger<MissionController> _logger;

        public MissionController(ILogger<MissionController> logger, DBContext context)
        {

            this._context = context;
            this._logger = logger;
        }
    }
}
