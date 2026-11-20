using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using TodoListWpf.Models;

namespace TodoListWpf.Services;

/// <summary>
/// Servicio de persistencia en disco mediante archivos en formato JSON.
/// </summary>
public class ServicioPersistenciaJson : IServicioPersistencia
{
    private readonly JsonSerializerOptions _optSerializacion = new()
    {
        WriteIndented = true
    };

    /// <summary>
    /// Tarea: Carga y deserializa las tareas almacenadas en el archivo JSON especificado.
    /// </summary>
    public List<TareaModel> CargarTareas(string strRutaArchivo)
    {
        try
        {
            if (!File.Exists(strRutaArchivo))
            {
                return new List<TareaModel>();
            }

            string strContenidoJson = File.ReadAllText(strRutaArchivo);
            var colTareasCargadas = JsonSerializer.Deserialize<List<TareaModel>>(strContenidoJson, _optSerializacion);
            return colTareasCargadas ?? new List<TareaModel>();
        }
        catch
        {
            return new List<TareaModel>();
        }
    }

    /// <summary>
    /// Tarea: Serializa y escribe la colección de tareas en el archivo JSON especificado.
    /// </summary>
    public void GuardarTareas(string strRutaArchivo, IEnumerable<TareaModel> colTareasAGuardar)
    {
        try
        {
            string? strDirectorioPadre = Path.GetDirectoryName(strRutaArchivo);
            if (!string.IsNullOrEmpty(strDirectorioPadre) && !Directory.Exists(strDirectorioPadre))
            {
                Directory.CreateDirectory(strDirectorioPadre);
            }

            string strContenidoJson = JsonSerializer.Serialize(colTareasAGuardar, _optSerializacion);
            File.WriteAllText(strRutaArchivo, strContenidoJson);
        }
        catch
        {
            // Manejo seguro ante restricciones de escritura
        }
    }
}
