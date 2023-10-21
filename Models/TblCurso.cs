using System;
using System.Collections.Generic;

namespace SICOBack.Models;

public partial class TblCurso
{
    public int IdCurso { get; set; }

    public string StrNombre { get; set; } = null!;

    public DateTime? DtmFechaCreacion { get; set; }
}
