using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyectoEjerciciosFisicos.Data;
using ProyectoEjerciciosFisicos.Models;

namespace ProyectoEjerciciosFisicos.Controllers;

public class TipoEjerciciosController : Controller
{
    private ApplicationDbContext _context;
    public TipoEjerciciosController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public JsonResult ListadoTipoEjercicios(int? id)
    {
        var tipoEjercicios = _context.TipoEjercicios.ToList();

        if (id != null)
        {
            tipoEjercicios = tipoEjercicios.Where(t => t.TipoEjercicioId == id).ToList();
        }

        return Json(tipoEjercicios);
    }

    public JsonResult GuardarTipoEjercicios(int tipoEjercicioId, string nombre)
    {
        string resultado = "";

        if (!String.IsNullOrEmpty(nombre))
        {
            nombre = nombre.ToUpper();
            if (tipoEjercicioId == 0)
            {
                var existeTipoEjercicio = _context.TipoEjercicios.Any(n => n.Nombre == nombre);
                if (!existeTipoEjercicio)
                {
                    var tipoEjercicio = new TipoEjercicio
                    {
                        Nombre = nombre
                    };
                    _context.Add(tipoEjercicio);
                    _context.SaveChanges();

                    resultado = "El tipo de ejercicio se ha guardado correctamente";
                }
                else
                {
                    resultado = "El tipo de ejercicio ya existe";
                }
            }
            else
            {
                var tipoEjercicioEditar = _context.TipoEjercicios.SingleOrDefault(t => t.TipoEjercicioId == tipoEjercicioId);
                if (tipoEjercicioEditar != null)
                {
                    var existeTipoEjercicioEditar = _context.TipoEjercicios.Where(n => n.Nombre == nombre && n.TipoEjercicioId != tipoEjercicioId).Count();
                    if (existeTipoEjercicioEditar == 0)
                    {
                        tipoEjercicioEditar.Nombre = nombre;

                        _context.SaveChanges();

                        resultado = "El tipo de ejercicio se ha actualizado correctamente";
                    }
                    else
                    {
                        resultado = "El tipo de ejercicio con ese nombre ya existe";
                    }
                }
            }
        }
        else
        {
            resultado = "Debe ingresar un nombre";
        }

        return Json(resultado);
    }

    public JsonResult EliminarTipoEjercicios(int tipoEjercicioId)
    {
        try
        {
            var eliminarTipoEjercicio = _context.TipoEjercicios.Find(tipoEjercicioId);

            if (eliminarTipoEjercicio == null)
            {
                return Json(false);
            }

            _context.Remove(eliminarTipoEjercicio);
            _context.SaveChanges();

            return Json(true);
        }

        catch (Exception)
        {
            return Json(new { success = false, messagge = "Error al eliminar el tipo de ejercicio" });
        }

    }
}