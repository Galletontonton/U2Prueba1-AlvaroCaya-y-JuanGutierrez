using System;
using System.Collections.Generic;

namespace U2_Prueba1.Models;

public partial class Asignatura
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string Codigo { get; set; } = null!;

    public DateOnly? FechaActualizacion { get; set; }

    public virtual ICollection<AsignaturasEstudiante> AsignaturasEstudiantes { get; set; } = new List<AsignaturasEstudiante>();

    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();
}
