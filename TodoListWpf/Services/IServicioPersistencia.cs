using System.Collections.Generic;
using TodoListWpf.Models;

namespace TodoListWpf.Services;

/// <summary>
/// Contrato para el servicio de persistencia de tareas.
/// </summary>
public interface IServicioPersistencia
{
    List<TareaModel> CargarTareas(string strRutaArchivo);
    void GuardarTareas(string strRutaArchivo, IEnumerable<TareaModel> colTareasAGuardar);
}
