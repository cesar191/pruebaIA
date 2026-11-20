---
name: Persistence
description: Directrices para persistencia simple de datos en aplicaciones de escritorio
---
# Propósito
Proveer un mecanismo ligero, robusto y fácil de modificar para guardar y recuperar el estado de la aplicación en disco (por ejemplo en formato JSON).

# Directrices
1. **Formato estándar**: Preferir JSON usando `System.Text.Json` por su alto rendimiento y ausencia de dependencias externas.
2. **Ubicación segura**: Guardar los archivos de datos en una ruta accesible y predecible (carpeta local de la aplicación o `AppData/Local`).
3. **Manejo de errores**:
   - Si el archivo no existe, iniciar con una colección vacía o datos por defecto.
   - Capturar excepciones de lectura/escritura para evitar que la aplicación falle inesperadamente.
4. **Desacoplamiento**: Encapsular la lógica en un servicio (ej. `IServicioPersistencia` / `ServicioPersistencia`) para facilitar el cambio futuro a SQLite o una base de datos remota si se requiere.
