using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ProyectoEjerciciosFisicos.Models;

public class TipoEjercicio
{
    [Key]
    public int TipoEjercicioId { get; set; }
    public string Nombre { get; set; }
    public bool Eliminado { get; set; }
}
