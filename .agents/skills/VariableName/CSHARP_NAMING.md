# Guía de Nombramiento de Variables en C# (WPF)

Este documento detalla el formato de nombramiento de variables para C# en proyectos WPF, siguiendo la regla base:

`{tipo}{tarea}`

---

## 1. Reglas Generales

El nombramiento se compone de:
1. **`{tipo}`**: Prefijo de 3 o 4 caracteres que identifica el tipo de dato o estructura.
2. **`{tarea}`**: Nombre descriptivo en PascalCase o camelCase (según el ámbito) que expresa la función o propósito específico de la variable.

---

## 2. Prefijos de Tipo (`{tipo}`)

| Tipo de Dato / Componente | Prefijo | Ejemplo de Tarea | Resultado |
| :--- | :--- | :--- | :--- |
| `string` | `str` | TituloTarea | `strTituloTarea` |
| `bool` | `bool` | EstaCompletada | `boolEstaCompletada` |
| `int` / `long` | `int` | ContadorTareas | `intContadorTareas` |
| `DateTime` | `dtm` | FechaCreacion | `dtmFechaCreacion` |
| `ICommand` | `cmd` | AgregarTarea | `cmdAgregarTarea` |
| `ObservableCollection<T>` | `col` | Tareas | `colTareas` |
| `List<T>` / `IEnumerable<T>` | `lst` | TareasFiltradas | `lstTareasFiltradas` |
| Objeto / Modelo | `obj` | TareaSeleccionada | `objTareaSeleccionada` |
| Servicio / Interfaz | `srv` | Persistencia | `srvPersistencia` |

---

## 3. Ámbitos y Convenciones C#

### A. Propiedades Públicas (PascalCase con prefijo)
Para propiedades expuestas en ViewModels o Modelos:
```csharp
public string StrTitulo { get; set; }
public bool BoolCompletada { get; set; }
public ICommand CmdAgregarTarea { get; }
public ObservableCollection<TareaModel> ColTareas { get; }
```

### B. Campos Privados (`_` + camelCase con prefijo)
Para campos de respaldo (*backing fields*):
```csharp
private string _strTitulo = string.Empty;
private bool _boolCompletada;
private readonly IServicioPersistencia _srvPersistencia;
```

### C. Variables Locales y Parámetros (camelCase con prefijo)
En métodos, constructores y funciones:
```csharp
public void AgregarTarea(string strTituloNuevo, string strDescripcionNueva)
{
    int intIdNueva = ObtenerSiguienteId();
    var objNuevaTarea = new TareaModel(intIdNueva, strTituloNuevo, strDescripcionNueva);
    _colTareas.Add(objNuevaTarea);
}
```

---

## 4. Beneficios
- **Claridad inmediata**: Al leer el código se conoce el tipo y el propósito de la variable.
- **Mantenibilidad**: Fácil de refactorizar y extender sin confusiones de tipo ni acoplamiento.
