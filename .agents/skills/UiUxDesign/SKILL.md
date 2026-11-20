---
name: UiUxDesign
description: Directrices y estándares de diseño visual moderno, accesibilidad y experiencia de usuario (UI/UX) para aplicaciones de escritorio WPF.
---
# Propósito
Proveer principios de diseño y patrones de interfaz para construir aplicaciones de escritorio elegantes, intuitivas, fluidas y visualmente atractivas.

# Principios de Diseño UI/UX

## 1. Jerarquía Visual y Tipografía
- **Escala de fuentes**: Establecer niveles claros de lectura:
  - Títulos principales: 22px – 28px (Bold).
  - Encabezados de sección / Tarjetas: 15px – 16px (SemiBold).
  - Cuerpo / Textos regulares: 13px – 14px (Regular).
  - Metadatos, fechas o ayudas: 11px – 12px (Regular, color atenuado).
- **Contraste**: Garantizar ratio mínimo de contraste WCAG AA (mínimo 4.5:1 para texto normal) tanto en modo Claro como Oscuro.

## 2. Paleta de Colores Semántica y Elevación
- Emplear colores basados en propósito:
  - **Superficies**: Colores neutros con niveles sutiles de profundidad (fondo de ventana `#F8FAFC` / `#0F172A`, tarjetas `#FFFFFF` / `#1E293B`).
  - **Acento**: Un color principal vibrante (Índigo `#4F46E5`, Azul `#2563EB`) para acciones primarias e indicadores activos.
  - **Estados**: Verde para completado/éxito, Ámbar para pendiente/advertencia, Rojo para peligro/eliminar.
- **Elevación suave (*Soft Shadows*)**: Usar `DropShadowEffect` con baja opacidad (0.05 a 0.10) y alto radio de desenfoque (8 a 16px) en lugar de sombras duras.

## 3. Componentes Estilizados (Custom Controls)
- **Checkboxes**: Reemplazar la casilla cuadrada predeterminada por un selector circular estilizado de 20-24px con checkmark nítido vectorizado (`Path`) y transición suave de fondo al activarse.
- **Cajas de texto (Inputs)**:
  - Bordes redondeados (`CornerRadius="8"`).
  - Resaltado de borde en foco (`BorderBrush` con color de acento).
  - Espaciado interno generoso (`Padding="12,9"`).
  - Texto de ayuda o *watermark* claro que no obstaculice la escritura.
- **Botones**:
  - Primarios: Fondo de acento con texto contrastado y cambio de brillo en hover.
  - Píldoras de filtro: Estado activo con relleno de acento suave y texto en negrita.
  - Acciones destructivas: Botones "fantasma" que solo revelan el color rojo en hover para evitar ruido visual constante.
- **Barras de desplazamiento (*ScrollBars*)**:
  - Diseñar barras delgadas y redondeadas (ancho de 6-8px) con pulgar translúcido que evite ocupar espacio visual excesivo.

## 4. Estados Vacíos (*Empty States*)
- Nunca mostrar una lista vacía sin orientación.
- Presentar una tarjeta con icono amigable (ej. 📋, 🎯, ✨), mensaje motivacional y sugerencia de acción para guiar al usuario.

## 5. Micro-interacciones y Feedback
- El cursor debe cambiar a `Hand` en todos los elementos interactivos (botones, checkboxes, pestañas).
- Atajos de teclado intuitivos (ej. `Enter` para agregar tarea, `Esc` para limpiar búsqueda).
- Contadores dinámicos tipo insignia (*badges*) con fondos pasteles suaves.
