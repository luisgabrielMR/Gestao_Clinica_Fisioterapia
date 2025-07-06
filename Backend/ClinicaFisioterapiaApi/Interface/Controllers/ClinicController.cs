using ClinicaFisioterapiaApi.Application.UseCases.Clinics;
using ClinicaFisioterapiaApi.Interface.Dtos.Clinics.Input;
using ClinicaFisioterapiaApi.Interface.Dtos.Clinics.Output;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaFisioterapiaApi.Interface.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClinicController : ControllerBase
{
    private readonly CreateClinicUseCase _createClinicUseCase;
    private readonly GetClinicByIdUseCase _getClinicByIdUseCase;
    private readonly GetClinicsPagedUseCase _getClinicsPagedUseCase;
    private readonly UpdateClinicUseCase _updateClinicUseCase;
    private readonly DeleteClinicUseCase _deleteClinicUseCase;

    public ClinicController(
        CreateClinicUseCase createClinicUseCase,
        GetClinicByIdUseCase getClinicByIdUseCase,
        GetClinicsPagedUseCase getClinicsPagedUseCase,
        UpdateClinicUseCase updateClinicUseCase,
        DeleteClinicUseCase deleteClinicUseCase)
    {
        _createClinicUseCase = createClinicUseCase;
        _getClinicByIdUseCase = getClinicByIdUseCase;
        _getClinicsPagedUseCase = getClinicsPagedUseCase;
        _updateClinicUseCase = updateClinicUseCase;
        _deleteClinicUseCase = deleteClinicUseCase;
    }

    // TODO: [Authorize(Roles = "Admin")] — restringir no futuro
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateClinicDto dto)
    {
        var id = await _createClinicUseCase.ExecuteAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    // TODO: [Authorize(Roles = "Admin")] — restringir no futuro
    [HttpGet("{id}")]
    public async Task<ActionResult<ClinicDto>> GetById(int id)
    {
        var clinic = await _getClinicByIdUseCase.ExecuteAsync(id);
        if (clinic == null)
            return NotFound("Clínica não encontrada.");

        return Ok(clinic);
    }

    // TODO: [Authorize(Roles = "Admin")] — restringir no futuro
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClinicDto>>> GetPaged(
        [FromQuery] string? filter,
        [FromQuery] string? sort,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10)
    {
        var clinics = await _getClinicsPagedUseCase.ExecuteAsync(filter, sort, page, size);
        return Ok(clinics);
    }

    // TODO: [Authorize(Roles = "Admin")] — restringir no futuro
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateClinicDto dto)
    {
        var updated = await _updateClinicUseCase.ExecuteAsync(id, dto);
        return updated ? NoContent() : NotFound("Clínica não encontrada.");
    }

    // TODO: [Authorize(Roles = "Admin")] — restringir no futuro
    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        var deleted = await _deleteClinicUseCase.ExecuteAsync(id);
        return deleted ? NoContent() : NotFound("Clínica não encontrada.");
    }
}
