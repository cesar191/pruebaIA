---
name: FunctionDesign
description: Estándares para el diseño de funciones y métodos con comentario inicial descriptivo de tarea y responsabilidad única.
---
# Propósito
Garantizar que cada función o método sea claro, autodocumentado, fácil de mantener y cumpla con el principio de responsabilidad única.

# Directrices

## 1. Comentario Inicial de Tarea (Obligatorio)
Al inicio de toda función o método se debe colocar un comentario corto (1 o 2 líneas) que explique exactamente **qué tarea cumple** el método.
- En C#, usar etiquetas de documentación XML `<summary>` o comentarios directos `// Tarea: ...` según el nivel de visibilidad.
- El comentario debe responder a la pregunta: *¿Qué hace esta función y para qué sirve?*

Ejemplo:
```csharp
/// <summary>
/// Tarea: Agrega una nueva tarea a la colección y persiste el estado en disco.
/// </summary>
public void AgregarTarea()
{
    // ...
}
```

## 2. Nombramiento de Parámetros y Variables
- Seguir el formato `{tipo}{tarea}` definido en la skill `VariableName`.
- Parámetros en camelCase: `strTituloNuevo`, `boolEsUrgente`, `intIdTarea`.
- Variables locales en camelCase: `colTareasFiltradas`, `objNuevaTarea`.

## 3. Principio de Responsabilidad Única
- Cada función debe tener un único objetivo bien delimitado.
- Si una función supera las 25-30 líneas o realiza más de una tarea (por ejemplo: calcular, validar y guardar en archivo a la vez), debe modularizarse en métodos auxiliares privados.

## 4. Retorno y Manejo Limpio
- Preferir retornos tempranos (*guard clauses*) para evitar anidamientos excesivos.
- Documentar si el método puede devolver valores nulos o generar excepciones controladas.
