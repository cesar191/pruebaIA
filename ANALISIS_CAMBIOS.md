# Análisis de Cambios Necesarios - TodoList WPF

Documento generado siguiendo la skill **[ChangeAnalysis](file:///d:/RepoGit/pruebaIA/.agents/skills/ChangeAnalysis/SKILL.md)** para diagnosticar el estado del software y guiar las mejoras de manera estructurada.

---

## 1. Diagnóstico del Estado Actual

La aplicación TodoListWpf cuenta con una base sólida:
- Arquitectura MVVM desacoplada.
- Modelo reactivo mediante `INotifyPropertyChanged`.
- Persistencia local automática en JSON.
- Nombramiento de variables con formato `{tipo}{tarea}` según `VariableName`.

### Puntos de mejora detectados:
1. **Falta de soporte para Modo Oscuro**: Los estilos de color estaban fijos en la vista XAML (`#FFFFFF`, `#F4F6F9`, etc.), impidiendo la adaptación a entornos de baja luminosidad o preferencias del usuario.
2. **Ausencia de comentarios normalizados de tarea en funciones**: Algunos métodos carecían de una descripción corta al inicio que expusiera su responsabilidad única.
3. **Falta de suite de Pruebas Unitarias automatizadas**: No se disponía de un proyecto de test para validar de forma continua el ViewModel, la lógica de filtros y la persistencia ante futuros cambios.

---

## 2. Necesidades y Oportunidades de Mejora

### A. Funcionales
- **Soporte de Temas Claro y Oscuro**: Permitir al usuario cambiar en caliente entre paleta clara y oscura con persistencia del tema seleccionado.
- **Botón de conmutación en la cabecera**: Acceso directo y visible con icono descriptivo (☀️ / 🌙).

### B. Arquitectura y Calidad
- **Estandarización de Funciones**: Añadir a cada método un comentario corto de tarea siguiendo la skill `FunctionDesign`.
- **Pruebas Unitarias Automatizadas**: Crear `TodoListWpf.Tests` con pruebas xUnit cubriendo escenarios clave del `MainViewModel` y `TareaModel`.

### C. Experiencia de Usuario (UI/UX)
- Uso de `DynamicResource` en todos los controles para una transición de color instantánea sin reiniciar la aplicación.

---

## 3. Evaluación de Impacto y Riesgo

| Cambio Propuesto | Módulos / Archivos Afectados | Riesgo de Regresión | Complejidad |
| :--- | :--- | :--- | :--- |
| **Soporte de Temas (Light / Dark)** | `Themes/Theme.Light.xaml`, `Themes/Theme.Dark.xaml`, `App.xaml`, `MainWindow.xaml`, `MainViewModel.cs` | Muy bajo (reemplaza colores estáticos por recursos dinámicos) | Media |
| **Comentarios de Tarea en Métodos** | Todos los `.cs` en `ViewModels/`, `Models/`, `Services/`, `Commands/` | Nulo (no altera la lógica de ejecución) | Baja |
| **Suite de Pruebas Unitarias** | Nuevo proyecto `TodoListWpf.Tests/` | Nulo (proyecto independiente) | Baja |

---

## 4. Plan de Acción Priorizado

1. **Paso 1**: Crear los diccionarios de temas `Theme.Light.xaml` y `Theme.Dark.xaml` en `TodoListWpf/Themes/`.
2. **Paso 2**: Enlazar los temas en `App.xaml` y actualizar `MainWindow.xaml` para usar `DynamicResource`.
3. **Paso 3**: Extender `MainViewModel.cs` con el comando `CmdAlternarTema` y comentarios de función normalizados.
4. **Paso 4**: Crear el proyecto `TodoListWpf.Tests` con xUnit y escribir pruebas unitarias.
5. **Paso 5**: Compilar la solución completa, ejecutar `dotnet test` y verificar que todas las pruebas pasen al 100%.
