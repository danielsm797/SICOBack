using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SICOBack.Classes;
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

            return Ok(tblEstudiante.IdEstudiante);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblEstudiante(int id)
        {
            if (_context.TblEstudiantes == null)
            {
                return NotFound();
            }

            // Eliminamos los cursos por estudiante

            var cursosXEstudiante = (from x in _context.TblCursoXestudiantes
             where x.IdEstudiante == id
             select x).ToList();

            if (cursosXEstudiante.Count > 0)
            {
                _context.TblCursoXestudiantes.RemoveRange(cursosXEstudiante);
                await _context.SaveChangesAsync();
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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblEstudiante(int id, TblEstudiante tblEstudiante)
        {
            if (id != tblEstudiante.IdEstudiante)
            {
                return BadRequest();
            }

            tblEstudiante.DtmFechaCreacion = DateTime.Now;

            _context.Entry(tblEstudiante).State = EntityState.Modified;

            try
            {         
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblEstudianteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CursoXEstudiante>> GetTblCursoByStudent(int id)
        {
            try
            {
                var cursos = (from x in _context.TblCursoXestudiantes
                              join y in _context.TblCursos on x.IdCurso equals y.IdCurso
                              where x.IdEstudiante == id
                              select new
                              {
                                  idCursoXEstudiante = x.IdCursoXestudiante,
                                  idCurso = y.IdCurso,
                                  strNombre = y.StrNombre,
                                  dtmFechaCreacion = y.DtmFechaCreacion
                              }).ToList();

                return Ok(cursos);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        private bool TblEstudianteExists(int id)
        {
            return (_context.TblEstudiantes?.Any(e => e.IdEstudiante == id)).GetValueOrDefault();
        }
    }
}
