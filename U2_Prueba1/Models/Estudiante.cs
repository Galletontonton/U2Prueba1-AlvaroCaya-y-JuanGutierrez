using System;
using System.Collections.Generic;

namespace U2_Prueba1.Models;

public partial class Estudiante
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Rut { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int? Edad { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string Password { get; set; } = null!;

    public virtual ICollection<AsignaturasEstudiante> AsignaturasEstudiantes { get; set; } = new List<AsignaturasEstudiante>();

    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();
}
