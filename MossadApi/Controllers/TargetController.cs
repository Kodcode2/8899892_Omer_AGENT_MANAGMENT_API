using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MossadApi.DAL;

namespace MossadApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TargetController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly ILogger<TargetController> _logger;

        public TargetController(ILogger<TargetController> logger, DBContext context)
        {

            this._context = context;
            this._logger = logger;
        }
    }
}
