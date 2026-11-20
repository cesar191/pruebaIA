---
name: ThemeManager
description: Directrices para la implementación y gestión de temas Claro (Light) y Oscuro (Dark) en aplicaciones WPF.
---
# Propósito
Proveer un sistema desacoplado, modular y fácil de modificar para alternar entre temas claro y oscuro dinámicamente en tiempo de ejecución.

# Arquitectura de Temas en WPF

## 1. Diccionarios de Recursos Semánticos
Definir recursos dinámicos (`DynamicResource`) en lugar de colores estáticos fijos:
- `BrushFondoPrincipal`: Color del fondo general de la ventana.
- `BrushFondoTarjeta`: Superficie de contenedores, tarjetas y elementos de lista.
- `BrushTextoPrimario`: Color de texto para títulos y contenido principal con alto contraste.
- `BrushTextoSecundario`: Color de texto para subtítulos, fechas o descripciones atenuadas.
- `BrushBorde`: Color de líneas divisorias y bordes de tarjetas/cajas de texto.
- `BrushAcento`: Color principal de acción (botones, selección, resaltados).

## 2. Archivos de Temas
Separar cada paleta en su propio `ResourceDictionary`:
- `Themes/Theme.Light.xaml`: Paleta clara (fondos blancos/grises suaves `#F4F6F9`, textos oscuros `#111827`).
- `Themes/Theme.Dark.xaml`: Paleta oscura (fondos oscuros `#1E1E2E`, superficies `#2A2A3E`, textos claros `#E0E0E0`).

## 3. Conmutación en Tiempo de Ejecución
Implementar un servicio o comando en el ViewModel (`CmdAlternarTema` o `CmdCambiarTema`) que reemplace el `MergedDictionary` activo en `Application.Current.Resources`:

```csharp
/// <summary>
/// Tarea: Alterna entre el tema claro y oscuro actualizando los recursos de la aplicación.
/// </summary>
public void AlternarTema(bool boolModoOscuro)
{
    string strRutaTema = boolModoOscuro ? "Themes/Theme.Dark.xaml" : "Themes/Theme.Light.xaml";
    var objNuevoTema = new ResourceDictionary { Source = new Uri(strRutaTema, UriKind.Relative) };
    
    Application.Current.Resources.MergedDictionaries.Clear();
    Application.Current.Resources.MergedDictionaries.Add(objNuevoTema);
}
```

## 4. Fácil Extensión
Para agregar un tercer tema (ej. Alto Contraste, Neón, Azul Corporativo), basta con crear un nuevo diccionario de recursos con las mismas claves semánticas.
