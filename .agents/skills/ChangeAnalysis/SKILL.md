---
name: ChangeAnalysis
description: Metodología estructurada para indicar, diagnosticar y analizar los cambios necesarios en el programa antes de implementarlos.
---
# Propósito
Proveer un marco simple y ordenado para evaluar el estado actual del código, identificar oportunidades de mejora, detectar cuellos de botella o requerimientos faltantes, y definir planes de acción claros antes de aplicar cambios.

# Estructura del Análisis de Cambios

Al analizar qué cambios son necesarios en la aplicación, se debe generar un reporte o sección estructurada con los siguientes 4 pilares:

## 1. Diagnóstico del Estado Actual
- Resumen de lo que la aplicación hace actualmente.
- Puntos de dolor, limitaciones técnicas o funciones ausentes identificadas.

## 2. Necesidades y Oportunidades de Mejora
Clasificar los cambios en 3 categorías:
- **Funcionales**: Nuevas características solicitadas (ej. fechas de vencimiento, temas, categorías).
- **Arquitectura y Calidad**: Refactorizaciones, desacoplamiento, pruebas unitarias, manejo de errores.
- **Experiencia de Usuario (UI/UX)**: Accesibilidad, contraste, atajos de teclado, feedback visual.

## 3. Evaluación de Impacto y Riesgo
Para cada cambio necesario, estimar:
- **Archivos/Módulos afectados**: Qué modelos, vistas o servicios sufrirán modificaciones.
- **Riesgo de regresión**: Si puede romper funcionalidades existentes.
- **Complejidad**: Baja, Media o Alta.

## 4. Plan de Acción Priorizado
Lista numerada de pasos concretos a seguir:
1. `Paso 1`: [Descripción de la tarea inicial con menor acoplamiento o base]
2. `Paso 2`: [Implementación de la lógica / ViewModel]
3. `Paso 3`: [Ajustes en la Vista / XAML]
4. `Paso 4`: [Pruebas unitarias de verificación y compilación]
