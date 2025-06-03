using ClinicaFisioterapiaApi.Dtos.Users;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { mensagem = "Dados inválidos." });

        var userDto = await _userService.CreateUserAsync(dto);
        if (userDto == null)
            return BadRequest(new { mensagem = "Nome de usuário já cadastrado." });

        return Ok(new { mensagem = "Usuário cadastrado com sucesso!", usuario = userDto });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var userDto = await _userService.GetUserByIdAsync(id);
        if (userDto == null)
            return NotFound(new { mensagem = "Usuário não encontrado." });

        return Ok(userDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { mensagem = "Dados inválidos." });

        var updated = await _userService.UpdateUserAsync(id, dto);
        if (!updated)
            return NotFound(new { mensagem = "Usuário não encontrado." });

        return Ok(new { mensagem = "Usuário atualizado com sucesso!" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _userService.DeleteUserAsync(id);
        if (!deleted)
            return NotFound(new { mensagem = "Usuário não encontrado." });

        return Ok(new { mensagem = "Usuário excluído com sucesso!" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        bool senhaValida = await _userService.VerifyPasswordAsync(dto.Username, dto.Password);

        if (!senhaValida)
            return Unauthorized(new { mensagem = "Usuário ou senha inválidos." });

        return Ok(new { mensagem = "Login realizado com sucesso!" });
    }

}
