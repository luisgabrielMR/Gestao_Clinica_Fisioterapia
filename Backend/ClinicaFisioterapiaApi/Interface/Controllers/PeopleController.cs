using AutoMapper;
using ClinicaFisioterapiaApi.Application.UseCases.People;
using ClinicaFisioterapiaApi.Interface.Dtos.People.Input;
using ClinicaFisioterapiaApi.Interface.Dtos.People.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaFisioterapiaApi.Interface.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase
{
    private readonly ILogger<PeopleController> _logger;
    private readonly IMapper _mapper;
    private readonly GetAllPeopleUseCase _getAll;
    private readonly GetPersonByIdUseCase _getById;
    private readonly CreatePersonUseCase _create;
    private readonly UpdatePersonUseCase _update;
    private readonly DeletePersonUseCase _delete;

    public PeopleController(
        ILogger<PeopleController> logger,
        IMapper mapper,
        GetAllPeopleUseCase getAll,
        GetPersonByIdUseCase getById,
        CreatePersonUseCase create,
        UpdatePersonUseCase update,
        DeletePersonUseCase delete)
    {
        _logger = logger;
        _mapper = mapper;
        _getAll = getAll;
        _getById = getById;
        _create = create;
        _update = update;
        _delete = delete;
    }

    /// <summary>Lista todas as pessoas cadastradas.</summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var result = await _getAll.ExecuteAsync();
        return Ok(result);
    }

    /// <summary>Retorna os dados de uma pessoa específica.</summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var person = await _getById.ExecuteAsync(id);
        if (person == null)
            return NotFound("Pessoa não encontrada.");

        return Ok(person);
    }


    /// <summary>Cria uma nova pessoa.</summary>
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] CreatePersonDto dto)
    {
        try
        {
            await _create.ExecuteAsync(dto);
            return Ok("Pessoa cadastrada com sucesso.");
        }
        catch (ApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
    }


    /// <summary>Atualiza os dados de uma pessoa existente.</summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreatePersonDto dto)
    {
        try
        {
            var success = await _update.ExecuteAsync(id, dto);
            if (!success)
                return NotFound("Pessoa não encontrada.");

            return Ok("Pessoa atualizada com sucesso.");
        }
        catch (ApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
    }


    /// <summary>Remove logicamente uma pessoa.</summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _delete.ExecuteAsync(id);
        if (!success)
            return NotFound("Pessoa não encontrada.");

        return Ok("Pessoa excluída com sucesso.");
    }
}
