using BLL.DTOModels;
using BLL.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupaController : ControllerBase
    {
        private readonly IGrupaService _grupaService;

        public GrupaController(IGrupaService grupaService)
        {
            _grupaService = grupaService;
        }


        [HttpGet("GetGrupy")]
        public async Task<ActionResult<List<GrupaFullNameDTO>>> GetAllGroups()
        {
            var grupy = await _grupaService.GetAllGrupy();
            return Ok(grupy);
        }

    }
}