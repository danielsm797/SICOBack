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
    [Route("api/estudiante")]
    [ApiController]
    public class TblEstudiantesController : ControllerBase
    {
        private readonly SicoDbContext _context = new SicoDbContext();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblEstudiante>>> GetTblEstudiantes()
        {
            if (_context.TblEstudiantes == null)
            {
                return NotFound();
            }

            return await _context.TblEstudiantes.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostTblEstudiante(TblEstudiante tblEstudiante)
        {
            if (_context.TblEstudiantes == null)
            {
                return Problem("Entity set 'SicoDbContext.TblEstudiantes'  is null.");
            }

            _context.TblEstudiantes.Add(tblEstudiante);
            await _context.SaveChangesAsync();

            return tblEstudiante.IdEstudiante;
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblEstudiante(int id)
        {
            if (_context.TblEstudiantes == null)
            {
                return NotFound();
            }

            var tblEstudiante = await _context.TblEstudiantes.FindAsync(id);
            if (tblEstudiante == null)
            {
                return NotFound();
            }

            _context.TblEstudiantes.Remove(tblEstudiante);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<TblCurso>> GetTblCursoByStudent(int id)
        {
            try
            {
                var cursos = (from x in _context.TblCursoXestudiantes
                              join y in _context.TblCursos on x.IdCurso equals y.IdCurso
                              where x.IdEstudiante == id
                              select y).ToList();

                return cursos;
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        } 
    }
}
