using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_API_MHW.Contexts;
using Proyecto_API_MHW.Models;

namespace Proyecto_API_MHW.Controllers
{
    [ApiController]
    [Route("api")]
    public class MonstroGrandeController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;
        public MonstroGrandeController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        [HttpGet("mg")]
        public async Task<ActionResult<List<vMonstroGrandes>>> GetMG()
        {
           return await applicationDbContext.vMonstroGrandes.ToListAsync();
        }
    }
}
