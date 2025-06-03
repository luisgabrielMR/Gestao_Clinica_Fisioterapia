using Microsoft.AspNetCore.Mvc;
using ClinicaFisioterapiaApi.Dtos.clinics;
using ClinicaFisioterapiaApi.Services;

namespace ClinicaFisioterapiaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClinicController : ControllerBase
    {
        private readonly IClinicService _service;

        public ClinicController(IClinicService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClinicDto>>> GetAll()
        {
            var clinics = await _service.GetAllAsync();
            return Ok(clinics);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClinicDto>> GetById(int id)
        {
            var clinic = await _service.GetByIdAsync(id);

            if (clinic is null)
                return NotFound("Clínica não encontrada.");

            return Ok(clinic);
        }

        [HttpPost]
        public async Task<ActionResult<ClinicDto>> Create([FromBody] CreateClinicDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados inválidos.");

            var clinic = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = clinic.ClinicId }, clinic);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateClinicDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);

            if (!success)
                return NotFound("Clínica não encontrada.");

            return Ok("Clínica atualizada com sucesso.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);

            if (!success)
                return NotFound("Clínica não encontrada.");

            return Ok("Clínica removida com sucesso.");
        }
    }
}
