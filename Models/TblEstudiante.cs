using System;
using System.Collections.Generic;

namespace SICOBack.Models;

public partial class TblEstudiante
{
    public int IdEstudiante { get; set; }

    public string StrIdentificacion { get; set; } = null!;

    public string StrPrimerNombre { get; set; } = null!;

    public string? StrSegundoNombre { get; set; }

    public string StrPrimerApellido { get; set; } = null!;

    public string StrSegundoApellido { get; set; } = null!;

    public DateTime? DtmFechaCreacion { get; set; }

    public string StrEmail { get; set; } = null!;
}
