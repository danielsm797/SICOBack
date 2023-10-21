using System;
using System.Collections.Generic;

namespace SICOBack.Models;

public partial class TblCursoXestudiante
{
    public int IdCursoXestudiante { get; set; }

    public int? IdEstudiante { get; set; }

    public int? IdCurso { get; set; }

    public virtual TblCurso? IdCursoNavigation { get; set; }

    public virtual TblEstudiante? IdEstudianteNavigation { get; set; }
}
