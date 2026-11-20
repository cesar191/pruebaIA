---
name: WpfMvvm
description: Guía de arquitectura y patrones MVVM simples para aplicaciones WPF
---
# Propósito
Establecer una estructura limpia, modular y fácil de modificar y mejorar para aplicaciones de escritorio WPF usando el patrón MVVM (Model-View-ViewModel) sin dependencias pesadas innecesarias.

# Estructura Recomendada
- **Models/**: Entidades de datos puras que representan el dominio del negocio (ej. `TareaModel.cs`).
- **ViewModels/**: Clases que gestionan el estado y la lógica de la UI, implementando `INotifyPropertyChanged` para el enlace bidireccional de datos.
- **Commands/**: Implementación reutilizable y ligera de `ICommand` (ej. `RelayCommand.cs`) para desacoplar las acciones de los eventos de la vista.
- **Views/**: Ventanas o controles de usuario en XAML limpios, donde la lógica de presentación se conecta mediante Data Binding y DataContext.
- **Services/**: Servicios auxiliares (persistencia, notificaciones, diálogo) inyectados o referenciados en los ViewModels.

# Principios Clave
1. **Desacoplamiento**: La vista no debe contener lógica de negocio en su code-behind (`.xaml.cs`).
2. **Simplicidad**: Utilizar implementaciones nativas directas antes de introducir frameworks externos pesados.
3. **Extensibilidad**: Diseñar propiedades observables y comandos independientes para que añadir nuevas funcionalidades sea intuitivo y seguro.
