# Todo List WPF - Arquitectura MVVM

Aplicación de lista de tareas desarrollada en **WPF** (.NET 10 / Windows Presentation Foundation) siguiendo las especificaciones del directorio `.agents`.

---

## 🚀 Características
- **Arquitectura MVVM**: Separación estricta de responsabilidades entre Vistas, ViewModels, Modelos y Servicios.
- **Formato de variables estandarizado**: Nombramiento bajo el formato `{tipo}{tarea}` (documentado en `.agents/skills/VariableName/CSHARP_NAMING.md`).
- **Persistencia local automática**: Guardado y carga transparente de tareas en formato JSON (`IServicioPersistencia`).
- **Filtros y Búsqueda en tiempo real**: Filtrado por estado (*Todas*, *Pendientes*, *Completadas*) y búsqueda por texto.
- **Interfaz Moderna**: Diseño limpio con tarjetas, contadores en tiempo real, atajos de teclado y acciones rápidas.

---

## 🛠️ Estructura del Proyecto

```text
TodoListWpf/
├── Commands/
│   └── RelayCommand.cs              # Implementación reusable de ICommand
├── Converters/
│   ├── BoolToOpacityConverter.cs    # Opacidad para tareas completadas
│   └── BoolToStrikethroughConverter.cs # Tachado de texto dinámico
├── Models/
│   └── TareaModel.cs                # Modelo observable de datos
├── Services/
│   ├── IServicioPersistencia.cs     # Contrato de persistencia
│   └── ServicioPersistenciaJson.cs  # Implementación en JSON local
├── ViewModels/
│   ├── ViewModelBase.cs             # Base con INotifyPropertyChanged
│   └── MainViewModel.cs             # Lógica y estado de la aplicación
├── Views / XAML:
│   ├── MainWindow.xaml              # Interfaz de usuario responsiva
│   └── MainWindow.xaml.cs           # Code-behind limpio (DataContext)
└── TodoListWpf.csproj
```

---

## 🤖 Skills en `.agents`

1. **Commit**: Guía para análisis y creación de commits atómicos con descripciones claras.
2. **VariableName**: Guía y documento `.md` de nombramiento de variables bajo `{tipo}{tarea}` para C#.
3. **WpfMvvm**: Directrices modulares para diseño y mantenimiento de aplicaciones WPF con MVVM.
4. **Persistence**: Guía para persistencia de datos liviana y desacoplada.

---

## 💻 Cómo Ejecutar

Para compilar y ejecutar el proyecto desde la terminal:

```powershell
dotnet run --project TodoListWpf/TodoListWpf.csproj
```
