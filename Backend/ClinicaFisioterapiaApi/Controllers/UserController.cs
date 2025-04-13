using Microsoft.AspNetCore.Mvc;
using ClinicaFisioterapiaApi.Data;
using ClinicaFisioterapiaApi.Models;
using System;
using System.Linq;
using ClinicaFisioterapiaApi.Dtos.Users;
using Microsoft.EntityFrameworkCore; // Necessário para o FirstOrDefaultAsync

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
                var users = _context.Users
                    .Select(user => new UserDto
                    {
                        UserId = user.UserId,
                        Name = user.Name
                    })
                    .ToList();

                if (users == null || users.Count == 0)
                {
                    return NotFound("Nenhum usuário encontrado.");
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
        public IActionResult CreateUser([FromBody] CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (_context.Users.Any(u => u.Name == createUserDto.Name))
                {
                    ModelState.AddModelError("Name", "Já existe um usuário com este nome.");
                    return BadRequest(ModelState);
                }

                var user = new User(createUserDto.Name, createUserDto.Password);

                _context.Users.Add(user);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, new UserDto { UserId = user.UserId, Name = user.Name });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.InnerException?.Message}");
            }
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _context.Users.Select(u => new UserDto { UserId = u.UserId, Name = u.Name }).FirstOrDefault(u => u.UserId == id);

                if (user == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingUser = _context.Users.Find(id);

                if (existingUser == null)
                {
                    return NotFound("Usuário não encontrado");
                }

                existingUser.Update(updateUserDto.Name, updateUserDto.Password);

                _context.SaveChanges();
                return NoContent(); // 204 No Content
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
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
                {
                    return NotFound("Usuário não encontrado");
                }
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