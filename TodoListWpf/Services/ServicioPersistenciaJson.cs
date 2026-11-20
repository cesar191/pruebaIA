using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using TodoListWpf.Models;

namespace TodoListWpf.Services;

/// <summary>
/// Implementación de persistencia de tareas utilizando archivos JSON locales.
/// </summary>
public class ServicioPersistenciaJson : IServicioPersistencia
{
    private readonly JsonSerializerOptions _optSerializacion = new()
    {
        WriteIndented = true
    };

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
            // Manejo silencioso o logging de fallo de guardado
        }
    }
}
