---
name: UnitTesting
description: Estándares y directrices para la creación y ejecución de pruebas unitarias automatizadas con patrón AAA.
---
# Propósito
Asegurar la confiabilidad, estabilidad y facilidad de refactorización del código mediante pruebas unitarias modulares, rápidas e independientes.

# Directrices

## 1. Estructura del Proyecto de Pruebas
- Crear un proyecto específico separado: `<NombreProyecto>.Tests` (ej. `TodoListWpf.Tests`).
- Referenciar al proyecto principal en el archivo `.csproj`.
- Organizar las carpetas de pruebas reflejando la estructura del proyecto origen: `ViewModelsTests/`, `ModelsTests/`, `ServicesTests/`.

## 2. Convención de Nombres de Métodos de Prueba
Usar la convención descriptiva de 3 partes:
`{MetodoOTarea}_{CondicionOEscenario}_{ResultadoEsperado}`

Ejemplos:
- `EjecutarAgregarTarea_ConTituloValido_IncrementaTotalTareas()`
- `EjecutarEliminarTarea_TareaExistente_RemueveDeColeccion()`
- `AlternarCompletada_TareaPendiente_CambiaEstadoACompletada()`
- `GuardarYCargar_TareasValidas_RecuperaListaIdentica()`

## 3. Estructura de Cada Prueba (Patrón AAA)
Cada método de prueba debe estar claramente seccionado:
```csharp
[Fact]
public void EjecutarAgregarTarea_ConTituloValido_IncrementaTotalTareas()
{
    // Arrange (Preparación): Configurar el estado inicial y dependencias
    var srvPersistenciaFalsa = new ServicioPersistenciaMemoria();
    var objViewModel = new MainViewModel(srvPersistenciaFalsa);
    objViewModel.StrNuevoTitulo = "Comprar leche";

    // Act (Acción): Ejecutar la operación bajo prueba
    objViewModel.CmdAgregarTarea.Execute(null);

    // Assert (Verificación): Comprobar que el resultado cumple la expectativa
    Assert.Contains(objViewModel.ColTareas, t => t.StrTituloTarea == "Comprar leche");
}
```

## 4. Pruebas Aisladas y Rápidas
- No depender del sistema de archivos real si es posible; usar mocks o implementaciones en memoria para servicios como persistencia (`IServicioPersistencia`).
- Las pruebas deben poder ejecutarse en cualquier entorno mediante:
  ```powershell
  dotnet test
  ```
