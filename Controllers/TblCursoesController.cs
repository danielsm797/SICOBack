﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SICOBack.Models;

namespace SICOBack.Controllers
{
    [Route("api/curso")]
    [ApiController]
    public class TblCursoesController : ControllerBase
    {
        private readonly SicoDbContext _context = new();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblCurso>>> GetTblCursos()
        {
            if (_context.TblCursos == null)
            {
                return NotFound();
            }
            return await _context.TblCursos.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostTblCurso(TblCurso tblCurso)
        {
            if (_context.TblCursos == null)
            {
                return Problem("Entity set 'SicoDbContext.TblCursos'  is null.");
            }

            _context.TblCursos.Add(tblCurso);
            await _context.SaveChangesAsync();

            return tblCurso.IdCurso;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblCurso(int id)
        {
            if (_context.TblCursos == null)
            {
                return NotFound();
            }

            var tblCurso = await _context.TblCursos.FindAsync(id);
            if (tblCurso == null)
            {
                return NotFound();
            }

            _context.TblCursos.Remove(tblCurso);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}