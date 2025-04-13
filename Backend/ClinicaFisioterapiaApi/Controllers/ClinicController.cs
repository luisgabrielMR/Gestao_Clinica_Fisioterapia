using Microsoft.AspNetCore.Mvc;
using ClinicaFisioterapiaApi.Data;
using ClinicaFisioterapiaApi.Models;
using System;
using System.Linq;
using ClinicaFisioterapiaApi.Dtos.clinics;

namespace ClinicaFisioterapiaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClinicController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClinicController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Clinic
        [HttpGet]
        public IActionResult GetClinics()
        {
            try
            {
                var clinics = _context.Clinics
                    .Select(clinic => new ClinicDto
                    {
                        ClinicId = clinic.ClinicId,
                        Name = clinic.Name,
                        Address = clinic.Address,
                        Neighborhood = clinic.Neighborhood,
                        City = clinic.City,
                        State = clinic.State,
                        Zipcode = clinic.Zipcode
                    }).ToList();

                if (clinics == null || clinics.Count == 0)
                {
                    return NotFound("Nenhuma clínica encontrada.");
                }

                return Ok(clinics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // POST: api/Clinic
        [HttpPost]
        public IActionResult CreateClinic([FromBody] CreateClinicDto createClinicDto)
        {
            if (createClinicDto == null)
            {
                return BadRequest("Dados inválidos.");
            }

            try
            {
                var clinic = new Clinic
                {
                    Name = createClinicDto.Name,
                    Address = createClinicDto.Address,
                    Neighborhood = createClinicDto.Neighborhood,
                    City = createClinicDto.City,
                    State = createClinicDto.State,
                    Zipcode = createClinicDto.Zipcode
                };

                _context.Clinics.Add(clinic);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetClinics), new { id = clinic.ClinicId }, clinic);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
        // PUT: api/Clinic/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateClinic(int id, [FromBody] UpdateClinicDto updateClinicDto)
        {
            if (updateClinicDto == null)
            {
                return BadRequest("Dados inválidos.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingClinic = _context.Clinics.Find(id);

                if (existingClinic == null)
                {
                    return NotFound("Clínica não encontrada.");
                }

                if (updateClinicDto.Name != null)
                {
                    existingClinic.Name = updateClinicDto.Name;
                }
                if (updateClinicDto.Address != null)
                {
                    existingClinic.Address = updateClinicDto.Address;
                }
                if (updateClinicDto.Neighborhood != null)
                {
                    existingClinic.Neighborhood = updateClinicDto.Neighborhood;
                }
                if (updateClinicDto.City != null)
                {
                    existingClinic.City = updateClinicDto.City;
                }
                if (updateClinicDto.State != null)
                {
                    existingClinic.State = updateClinicDto.State;
                }
                if (updateClinicDto.Zipcode != null)
                {
                    existingClinic.Zipcode = updateClinicDto.Zipcode;
                }

                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
        // DELETE: api/Clinic
        [HttpDelete("{id}")]
        public IActionResult DeleteClinic(int id)
        {

            try
            {
                var clinic = _context.Clinics.Find(id);
                if (clinic == null)
                {
                    return NotFound("Clinica não encontrada");
                }
                _context.Clinics.Remove(clinic);
                _context.SaveChanges();
                return NoContent(); //204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
