# Todo List Studio - WPF Moderno & Estilizado

Aplicación de gestión de tareas desarrollada en **WPF** (.NET 10 / Windows Presentation Foundation) siguiendo rigurosamente las especificaciones del directorio `.agents`.

---

## 🚀 Características y Experiencia de Usuario (UI/UX)
- **Diseño Moderno y Estilizado**:
  - Paleta de colores cuidada basada en Slate & Indigo con soporte en caliente para **Modo Claro** y **Modo Oscuro** (`ThemeManager`).
  - **Checkboxes circulares minimalistas** de 22px con animación y checkmark vectorial limpio.
  - **Barras de desplazamiento delgadas** (*slim modern scrollbars*) no invasivas.
  - **Tarjetas con elevación suave** (*soft shadows*) y micro-interacciones al pasar el cursor (`IsMouseOver`).
  - **Inputs modernos** con textos de ayuda (*watermarks* automáticos) y botones de acción rápida.
  - **Empty States intuitivos**: Pantalla amigable cuando no existen tareas en la categoría seleccionada con icono contextual.
  - Botón de borrado minimalista estilo *ghost* que solo resalta en color de peligro al interactuar.
- **Arquitectura MVVM**: Desacoplamiento total entre Vistas, ViewModels, Modelos y Servicios (`WpfMvvm`).
- **Nombramiento `{tipo}{tarea}`**: Estándar transversal documentado en [.agents/skills/VariableName/CSHARP_NAMING.md](file:///d:/RepoGit/pruebaIA/.agents/skills/VariableName/CSHARP_NAMING.md) (`VariableName`).
- **Documentación de Tarea por Función**: Cada método incluye un comentario de responsabilidad única (`FunctionDesign`).
- **Persistencia JSON**: Guardado automático transparente en disco local (`Persistence`).
- **Suite de Pruebas Automatizadas**: 15 pruebas unitarias con xUnit y patrón AAA (`UnitTesting`).
- **Análisis de Evolución**: Diagnóstico continuo en [ANALISIS_CAMBIOS.md](file:///d:/RepoGit/pruebaIA/ANALISIS_CAMBIOS.md) (`ChangeAnalysis`).

---

## 🤖 Skills en `.agents/skills/`

| Skill | Propósito |
| :--- | :--- |
| **[UiUxDesign](file:///d:/RepoGit/pruebaIA/.agents/skills/UiUxDesign/SKILL.md)** | Directrices de jerarquía visual, componentes estilizados, accesibilidad y empty states. |
| **[ThemeManager](file:///d:/RepoGit/pruebaIA/.agents/skills/ThemeManager/SKILL.md)** | Gestión de temas Claro/Oscuro en tiempo de ejecución con `DynamicResource`. |
| **[FunctionDesign](file:///d:/RepoGit/pruebaIA/.agents/skills/FunctionDesign/SKILL.md)** | Estandarización de métodos con comentario descriptivo de tarea y responsabilidad única. |
| **[UnitTesting](file:///d:/RepoGit/pruebaIA/.agents/skills/UnitTesting/SKILL.md)** | Pruebas unitarias automatizadas con patrón AAA y convención de nombres clara. |
| **[ChangeAnalysis](file:///d:/RepoGit/pruebaIA/.agents/skills/ChangeAnalysis/SKILL.md)** | Metodología estructurada de diagnóstico, impacto y plan de acción para cambios. |
| **[WpfMvvm](file:///d:/RepoGit/pruebaIA/.agents/skills/WpfMvvm/SKILL.md)** | Arquitectura desacoplada Modelo-Vista-ViewModel nativa en WPF. |
| **[Persistence](file:///d:/RepoGit/pruebaIA/.agents/skills/Persistence/SKILL.md)** | Persistencia ligera en disco mediante serialización JSON. |
| **[VariableName](file:///d:/RepoGit/pruebaIA/.agents/skills/VariableName/SKILL.md)** | Formato de nombramiento de variables `{tipo}{tarea}`. |
| **[Commit](file:///d:/RepoGit/pruebaIA/.agents/skills/Commit/SKILL.md)** | Análisis y creación de commits atómicos con descripciones claras. |

---

## 💻 Comandos de Ejecución

### Ejecutar la Aplicación:
```powershell
dotnet run --project TodoListWpf/TodoListWpf.csproj
```

### Ejecutar las Pruebas Unitarias:
```powershell
dotnet test TodoListWpf.Tests/TodoListWpf.Tests.csproj
```
