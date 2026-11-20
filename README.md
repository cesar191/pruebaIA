# Todo List WPF - Arquitectura MVVM con Soporte Claro / Oscuro

Aplicación de lista de tareas desarrollada en **WPF** (.NET 10 / Windows Presentation Foundation) siguiendo rigurosamente las especificaciones del directorio `.agents`.

---

## 🚀 Características
- **Arquitectura MVVM**: Separación estricta de responsabilidades entre Vistas, ViewModels, Modelos y Servicios.
- **Formato de variables `{tipo}{tarea}`**: Nombramiento estandarizado documentado en [.agents/skills/VariableName/CSHARP_NAMING.md](file:///d:/RepoGit/pruebaIA/.agents/skills/VariableName/CSHARP_NAMING.md).
- **Documentación de Funciones**: Cada método inicia con un comentario corto que describe su tarea específica (`FunctionDesign`).
- **Gestión de Temas (Claro / Oscuro)**: Conmutación dinámica en tiempo de ejecución con `DynamicResource` (`ThemeManager`).
- **Persistencia local automática**: Guardado y carga transparente en JSON (`Persistence`).
- **Filtros y Búsqueda en tiempo real**: Filtrado por estado (*Todas*, *Pendientes*, *Completadas*) y búsqueda por texto.
- **Suite de Pruebas Unitarias**: Cobertura con xUnit siguiendo el patrón AAA (`UnitTesting`).
- **Análisis de Cambios**: Diagnóstico y hoja de ruta estructurada en [ANALISIS_CAMBIOS.md](file:///d:/RepoGit/pruebaIA/ANALISIS_CAMBIOS.md) (`ChangeAnalysis`).

---

## 🛠️ Estructura del Proyecto

```text
pruebaIA/
├── .agents/
│   └── skills/
│       ├── Commit/            # Guía de commits atómicos
│       ├── VariableName/      # Convención {tipo}{tarea} y guía C#
│       ├── FunctionDesign/    # Estándar de comentarios de tarea en funciones
│       ├── ThemeManager/      # Gestión de temas Claro/Oscuro en WPF
│       ├── UnitTesting/       # Pautas para pruebas unitarias (AAA)
│       ├── ChangeAnalysis/    # Análisis estructurado de cambios necesarios
│       ├── WpfMvvm/           # Arquitectura MVVM desacoplada
│       └── Persistence/       # Persistencia ligera en disco
│
├── TodoListWpf/
│   ├── Commands/              # RelayCommand
│   ├── Converters/            # Conversores para UI reactiva
│   ├── Models/                # TareaModel con INotifyPropertyChanged
│   ├── Services/              # IServicioPersistencia y ServicioPersistenciaJson
│   ├── Themes/                # Theme.Light.xaml y Theme.Dark.xaml
│   ├── ViewModels/            # MainViewModel y ViewModelBase
│   └── Views/                 # MainWindow.xaml y MainWindow.xaml.cs
│
└── TodoListWpf.Tests/         # Suite de pruebas unitarias xUnit
    ├── ModelsTests/
    ├── ServicesTests/
    └── ViewModelsTests/
```

---

## 💻 Comandos Principales

### Ejecutar la Aplicación:
```powershell
dotnet run --project TodoListWpf/TodoListWpf.csproj
```

### Ejecutar las Pruebas Unitarias:
```powershell
dotnet test TodoListWpf.Tests/TodoListWpf.Tests.csproj
```
