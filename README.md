# CartaSur — Examen Técnico Analista Programador

**Fecha:** Octubre 2024  
**Candidato:** Paul Boeykens  
**Evaluador:** Claudio Correa — Jefe de Desarrollo, CartaSur

---

## Stack tecnológico utilizado

| Tecnología | Versión | Motivo |
|---|---|---|
| .NET | 8.0 LTS | Compatible con migración NET 4.7 → NET Core de CartaSur |
| ASP.NET Core Web API | 8.0 | API REST en capas |
| Entity Framework Core | 8.0 | ORM estándar de .NET |
| SQL Server | 2019 Express | Motor de base de datos relacional |
| Angular | 17 | Framework frontend requerido por el stack de CartaSur |
| TypeScript | 5.x | Lenguaje tipado para Angular |
| Node.js | 20 LTS | Runtime para Angular CLI |

---

## Estructura del proyecto
```
CartaSur.Examen/
│
├── CartaSur.Domain/          → Entidades, DTOs, Interfaces
├── CartaSur.Repository/      → Acceso a datos (EF Core + SQL Server)
├── CartaSur.Service/         → Lógica de negocio
├── CartaSur.API/             → Controllers REST (ASP.NET Core 8)
└── cartasur-frontend/        → Frontend (Angular 17 standalone)
```

---

## Decisiones de arquitectura y buenas prácticas

### Arquitectura en capas (N-Layers)

Se implementó una solución en 4 capas siguiendo principios SOLID:

- **Domain** — núcleo de la aplicación. Contiene entidades, DTOs e interfaces. No depende de ninguna otra capa. Al colocar las interfaces aquí se aplica el **Principio de Inversión de Dependencias (DIP)**: las capas superiores dependen de abstracciones, no de implementaciones concretas.

- **Repository** — implementa el acceso a datos con Entity Framework Core. Implementa las interfaces definidas en Domain. Si mañana se cambia SQL Server por otro motor, solo se modifica esta capa.

- **Service** — contiene la lógica de negocio. Depende de interfaces de Repository, nunca de implementaciones directas. Esto permite testear la lógica con mocks sin necesitar base de datos real.

- **API** — controllers REST. Solo reciben la request HTTP, delegan al Service y devuelven la respuesta con el código HTTP correcto. Sin lógica de negocio en esta capa.

### ¿Por qué esta arquitectura?

CartaSur está migrando de .NET Framework 4.7 a .NET Core 6/8. Esta arquitectura permite migrar módulo por módulo sin reescribir el sistema completo. Cada capa compila como una DLL independiente y puede evolucionar sin afectar a las demás.

### Inyección de dependencias

Se utiliza el sistema de DI nativo de ASP.NET Core. Todas las dependencias se registran en `Program.cs` con `AddScoped`, apropiado para operaciones de base de datos (una instancia por request HTTP).

### Async/await en toda la cadena

Todas las operaciones de base de datos son asíncronas (Controller → Service → Repository). Esto evita bloquear hilos del servidor mientras se espera respuesta de SQL Server, mejorando la escalabilidad.

### DTOs separados de entidades

Se usa `FechaMaxVentasDto` para transferir solo los datos necesarios al cliente. Exponer la entidad completa implicaría un acoplamiento entre la base de datos y la API, y potenciales problemas de seguridad.

---

## Aclaraciones importantes

### ★ Consumo de API externa (Punto 6)

El punto 6 pide consumir `https://svct.cartasur.com.ar/api/dummy` y mostrar el resultado en la web.

**Implementación actual:** el consumo se hace desde Angular (browser) mediante `HttpClient`.

**Limitación conocida:** la API externa tiene restricciones CORS que impiden ser consumida directamente desde el browser en `localhost`. Esto genera el mensaje "No se pudo conectar con la API" — el error es manejado correctamente por la aplicación.

**Implementación correcta en producción:** el consumo de APIs externas debería hacerse desde el **backend** (.NET Service) por las siguientes razones:
- Evita exponer credenciales o API Keys en el código JavaScript del browser
- El servidor no tiene restricciones CORS — puede llamar a cualquier URL
- Permite cachear, transformar o combinar la respuesta con datos propios antes de enviarla al frontend

La implementación correcta sería:
```csharp
// En CartaSur.Service
public class DummyService : IDummyService
{
    private readonly HttpClient _httpClient;
    
    public DummyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<object?> GetDummyDataAsync()
    {
        return await _httpClient
            .GetFromJsonAsync<object>("https://svct.cartasur.com.ar/api/dummy");
    }
}

// En Program.cs
builder.Services.AddHttpClient<DummyService>();
```

Se optó por el enfoque desde Angular por simplicidad dado que el enunciado no especificaba conexión a base de datos para esta funcionalidad, pero se es consciente de que en un entorno productivo la llamada correspondería al backend.

### ★ Normalización (Punto 3)

Se aplicaron las primeras tres formas normales (1FN, 2FN, 3FN):

- **1FN** — ya se cumplía: valores atómicos en cada celda
- **2FN** — se separaron entidades con dependencias parciales: `CLIENTES`, `EMPLEADOS`, `SUCURSALES`, `PRODUCTOS`
- **3FN** — se eliminaron dependencias transitivas: nombre de sucursal no depende de la venta sino de la sucursal

El modelo normalizado usa `IDENTITY(1,1)` para claves primarias autogeneradas y `REFERENCES` para integridad referencial.

### ★ Angular 17 — Standalone Components

Se usa la arquitectura moderna de Angular 17 sin NgModules. Cada componente declara sus propias dependencias en el decorador `@Component`. Ventajas: código más explícito, mejor tree-shaking, facilita lazy loading y testing aislado.

### ★ Estado en memoria (Punto 5)

El enunciado especifica explícitamente *"No se pide conexión con base de datos para el almacenamiento de datos en este caso"*. Los empleados se almacenan en un array en `AppComponent` y se pierden al recargar la página. En producción se persistirían mediante un endpoint POST en la API.

### ★ CORS

Se configuró una política CORS específica en `Program.cs` que permite solo el origen `http://localhost:4200`. No se usa `AllowAnyOrigin()` que sería inseguro. En producción se reemplazaría por el dominio real del frontend.

---

## Cómo ejecutar el proyecto

### Requisitos previos

- SQL Server 2019 o superior
- .NET 8.0 SDK
- Node.js 20 LTS
- Angular CLI 17 (`npm install -g @angular/cli@17`)

### 1. Base de datos

Ejecutar los scripts en orden en SQL Server Management Studio o Azure Data Studio:
```sql
-- 1. Crear base y tabla original
CREATE DATABASE CartaSurExamen;
USE CartaSurExamen;

CREATE TABLE VENTAS (
    ID_VENTA INT NOT NULL PRIMARY KEY,
    Fecha_venta DATETIME,
    Dni_cliente VARCHAR(10),
    Nombre_empleado VARCHAR(100),
    Nombre_cliente VARCHAR(100),
    Importe_total DECIMAL(10,2),
    Direccion_envio_cliente VARCHAR(100),
    Direccion_sucursal_venta VARCHAR(100),
    Nombre_sucursal_venta VARCHAR(100),
    Producto VARCHAR(20),
    Cantidad INT
);

-- 2. Insertar datos de ejemplo
INSERT INTO VENTAS VALUES
(1,'2024-10-15 10:00:00','20123456','Juan Perez','Carlos Lopez',1500.00,'Av. Rivadavia 100','Av. Corrientes 500','Sucursal Centro','Notebook',1),
(2,'2024-10-15 14:30:00','27654321','Maria Gomez','Ana Martinez',800.50,'Calle Falsa 123','Av. Corrientes 500','Sucursal Centro','Mouse',3),
(3,'2024-10-20 09:15:00','30987654','Carlos Ruiz','Pedro Sanchez',2200.00,'Belgrano 200','San Martin 300','Sucursal Sur','Monitor',2);

-- 3. Query punto 2 - fecha con más ventas
SELECT TOP 1
    CAST(Fecha_venta AS DATE) AS Fecha,
    COUNT(*) AS CantidadVentas
FROM VENTAS
GROUP BY CAST(Fecha_venta AS DATE)
ORDER BY CantidadVentas DESC;
```

### 2. Backend
```bash
# Clonar repositorio y abrir solución
# Verificar connection string en CartaSur.API/appsettings.json
# Ajustar el nombre de la instancia SQL Server si es necesario:
# "Server=TU_INSTANCIA\\SQLEXPRESS;Database=CartaSurExamen;..."

# Desde Visual Studio 2022:
# - Seleccionar http en el dropdown de inicio
# - Presionar F5
# La API queda disponible en http://localhost:5168
```

Verificar endpoint: `http://localhost:5168/api/ventas/fecha-max-ventas`

Respuesta esperada:
```json
{
  "fecha": "2024-10-15",
  "cantidadVentas": 2
}
```

### 3. Frontend
```bash
cd cartasur-frontend
npm install
npx ng serve
# Disponible en http://localhost:4200
```

---

## Puntos del examen resueltos

| Punto | Descripción | Estado |
|---|---|---|
| 1 | Crear tabla VENTAS e insertar registros | ✅ |
| 2 | Query fecha con más ventas | ✅ |
| 3 | Normalización 3FN + DER | ✅ |
| 4 | Solución en capas .NET 8 + endpoint | ✅ |
| 5 | Página Angular con listado y formulario de empleados | ✅ |
| 6 | Consumo API externa con manejo de error | ✅ |