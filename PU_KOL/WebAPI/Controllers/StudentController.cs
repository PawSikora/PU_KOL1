using BLL.DTOModels;
using BLL.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService) { _studentService = studentService; }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _studentService.GetAllStudenci();
            return Ok(result);
        }


        [HttpGet("GetStudent/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _studentService.GetStudentById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpPost("AddStudent")]
        public async Task<IActionResult> Create(StudentDTO dto)
        {
            var created = await _studentService.CreateStudent(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.ID }, created);
        }


        [HttpPut("UpdateStudent/{id}")]
        public async Task<IActionResult> Update(int id, StudentDTO dto)
        {
            var updated = await _studentService.UpdateStudent(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }


        [HttpDelete("DeleteStudent/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _studentService.DeleteStudent(id);
            if (!result) return NotFound();
            return NoContent();
        }

    }
}
