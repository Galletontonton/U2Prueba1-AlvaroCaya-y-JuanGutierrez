using System;
using System.Collections.Generic;

namespace U2_Prueba1.Models;

public partial class Nota
{
    public int Id { get; set; }

    public float Nota1 { get; set; }

    public float Ponderacion { get; set; }

    public int EstudiantesId { get; set; }

    public int AsignaturasId { get; set; }

    public virtual Asignatura Asignaturas { get; set; } = null!;

    public virtual Estudiante Estudiantes { get; set; } = null!;
}
