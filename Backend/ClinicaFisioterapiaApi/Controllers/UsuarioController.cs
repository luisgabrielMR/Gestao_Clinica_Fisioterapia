using Microsoft.AspNetCore.Mvc;
using ClinicaFisioterapiaApi.Data;
using ClinicaFisioterapiaApi.Models;
using System;
using System.Linq;

namespace ClinicaFisioterapiaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _context.Users.ToList();

                if (users == null || users.Count == 0)
                {
                    return NotFound("Usuário não encontrado");
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // POST: api/User
        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetUsers), new { id = user.UserId }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.InnerException?.Message}");
            }
        }

                // PUT: api/User/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingUser = _context.Users.Find(id);
                if (existingUser == null)
                    return NotFound("Usuário não encontrado");

                existingUser.Name = updatedUser.Name;
                existingUser.Password = updatedUser.Password;

                _context.SaveChanges();
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null)
                    return NotFound("Usuário não encontrado");

                _context.Users.Remove(user);
                _context.SaveChanges();
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
