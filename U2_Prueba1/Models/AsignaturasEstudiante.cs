using System;
using System.Collections.Generic;

namespace U2_Prueba1.Models;

public partial class AsignaturasEstudiante
{
    public int Id { get; set; }

    public int EstudiantesId { get; set; }

    public int AsignaturasId { get; set; }

    public DateOnly? FechaRegistro { get; set; }

    public virtual Asignatura Asignaturas { get; set; } = null!;

    public virtual Estudiante Estudiantes { get; set; } = null!;
}
