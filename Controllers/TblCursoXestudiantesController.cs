using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SICOBack.Models;

namespace SICOBack.Controllers
{
    [Route("api/cursosXEstudiante")]
    [ApiController]
    public class TblCursoXestudiantesController : ControllerBase
    {
        private readonly SicoDbContext _context = new();

        [HttpPost]
        public async Task<ActionResult<int>> PostTblCursoXestudiante(TblCursoXestudiante tblCursoXestudiante)
        {
            if (_context.TblCursoXestudiantes == null)
            {
                return Problem("Entity set 'SicoDbContext.TblCursoXestudiantes'  is null.");
            }

            // Validamos que el curso no se encuentre asignado al mismo estudiante

            var curso = _context
                .TblCursoXestudiantes
                .Where(x => x.IdCurso == tblCursoXestudiante.IdCurso && x.IdEstudiante == tblCursoXestudiante.IdEstudiante)
                .ToList();
    
            if (curso.Count > 0)
            {
                return Problem("El estudiante ya cuenta con este curso asignado");
            }

            _context.TblCursoXestudiantes.Add(tblCursoXestudiante);
            await _context.SaveChangesAsync();

            return tblCursoXestudiante.IdCursoXestudiante;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblCursoXestudiante(int id)
        {
            if (_context.TblCursoXestudiantes == null)
            {
                return NotFound();
            }

            var tblCursoXestudiante = await _context.TblCursoXestudiantes.FindAsync(id);
            if (tblCursoXestudiante == null)
            {
                return NotFound();
            }

            _context.TblCursoXestudiantes.Remove(tblCursoXestudiante);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
