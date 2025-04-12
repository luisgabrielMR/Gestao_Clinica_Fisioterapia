using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace ClinicaFisioterapiaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TesteConexaoController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TesteConexaoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            try
            {
                var connString = _configuration.GetConnectionString("DefaultConnection");
                using var conn = new NpgsqlConnection(connString);
                conn.Open();
                return Ok("Conexão com o banco de dados foi estabelecida com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro: {ex.Message}");
            }
        }
    }
}
