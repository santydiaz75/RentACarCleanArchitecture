# 🚗 Rent a Car - Clean Architecture

Una aplicación de gestión de alquiler de vehículos desarrollada con **.NET 10** siguiendo los principios de **Clean Architecture**. Este proyecto demuestra patrones y prácticas recomendadas en desarrollo de software moderno con ASP.NET Core.

## 📋 Descripción del Proyecto

**Rent a Car** es una API REST que permite gestionar:
- **Usuarios y Autenticación**: Registro, login y gestión de roles/permisos con JWT
- **Vehículos**: Catálogo de vehículos con detalles, accesorios y disponibilidad
- **Alquileres**: Reserva, confirmación y gestión del ciclo de vida de alquileres
- **Reseñas**: Sistema de comentarios y valoraciones de usuarios sobre vehículos

## 🏗️ Arquitectura

El proyecto sigue **Clean Architecture** organizando el código en capas independientes:

```
RentACarCleanArchitecture/
├── src/
│   ├── RentACarCleanArchitecture.Api              # Capa de Presentación (Controllers, Endpoints)
│   ├── RentACarCleanArchitecture.Application      # Capa de Aplicación (Use Cases, Handlers)
│   ├── RentACarCleanArchitecture.Domain           # Capa de Dominio (Entidades, Value Objects, Reglas de negocio)
│   └── RentACarCleanArchitecture.Infrastructure   # Capa de Infraestructura (DB, Auth, Persistencia)
└── test/
	├── RentACarCleanArchitecture.Domain.UnitTests                    # Tests unitarios de dominio
	├── RentACarCleanArchitecture.Application.UnitTests               # Tests unitarios de aplicación
	├── RentACarCleanArchitecture.Application.IntegrationTests        # Tests de integración
	├── RentACarCleanArchitecture.Api.FunctionalTests                 # Tests funcionales de API
	└── RentACarCleanArchitecture.ArchitectureTests                   # Tests de arquitectura
```

### Capas del Proyecto

| Capa | Responsabilidad | Ejemplos |
|------|-----------------|----------|
| **Domain** | Lógica de negocio central | `Alquiler.cs`, `Vehiculo.cs`, `User.cs`, Reglas de validación |
| **Application** | Casos de uso y orquestación | `ReservarAlquilerCommand`, `GetVehiculosQuery`, Domain Event Handlers |
| **Infrastructure** | Implementación técnica | Entity Framework, PostgreSQL, Autenticación JWT, Logging (Serilog) |
| **Api** | Puntos de entrada HTTP | Controllers, Endpoints, Middleware, Swagger |

### Patrones Implementados

- **CQRS**: Separación de comandos (writes) y queries (reads)
- **Domain-Driven Design (DDD)**: Agregados, Value Objects, Domain Events
- **Repository Pattern**: Abstracción del acceso a datos
- **Unit of Work Pattern**: Transaccionalidad coordinada
- **Dependency Injection**: Inyección de dependencias con Microsoft.Extensions.DependencyInjection
- **Authorization with Permissions**: Sistema granular de permisos basado en roles
- **Pagination**: Paginación en listados

## 🛠️ Tecnologías

| Componente | Versión/Librería |
|-----------|------------------|
| **.NET** | 10.0 |
| **Runtime** | ASP.NET Core 10.0 |
| **Base de Datos** | PostgreSQL (Neon) |
| **ORM** | Entity Framework Core 10.0 |
| **Autenticación** | JWT Bearer |
| **Logging** | Serilog + Seq |
| **API Versioning** | Asp.Versioning 10.2.1 |
| **API Documentation** | Swagger/Swashbuckle 10.0 |
| **Testing** | xUnit, FluentAssertions |
| **Docker** | Containerización |

## 🚀 Guía de Inicio

### Requisitos Previos

- **.NET 10.0** SDK instalado ([descargar](https://dotnet.microsoft.com/download))
- **PostgreSQL** (o acceso a instancia en nube)
- **Visual Studio 2026** (o VS Code con C# Dev Kit)
- **Docker** (opcional, para containerización)

### 1. Clonar el Repositorio

```bash
git clone https://github.com/santydiaz75/RentACarCleanArchitecture.git
cd RentACarCleanArchitecture
```

### 2. Configurar la Base de Datos

#### Opción A: Usar PostgreSQL Local

```bash
# Crear base de datos
createdb rentacarcleanarchitecturedb

# Aplicar migraciones
dotnet ef database update -p src/RentACarCleanArchitecture.Infrastructure -s src/RentACarCleanArchitecture.Api
```

#### Opción B: Usar Neon (Base de Datos en Nube)

La aplicación ya está configurada para usar Neon. Actualizar la cadena de conexión en `appsettings.json`:

```json
{
  "ConnectionStrings": {
	"Database": "Host=<neon-host>; Database=<db-name>; Username=<user>; Password=<password>; SSL Mode=VerifyFull; Channel Binding=Require;"
  }
}
```

### 3. Configurar Secrets (JWT y Base de Datos)

```bash
# En Visual Studio o usando dotnet user-secrets
dotnet user-secrets init -p src/RentACarCleanArchitecture.Api

dotnet user-secrets set "Jwt:SecretKey" "<tu-secret-key>" -p src/RentACarCleanArchitecture.Api
dotnet user-secrets set "ConnectionStrings:Database" "<tu-connection-string>" -p src/RentACarCleanArchitecture.Api
```

### 4. Restaurar Dependencias

```bash
dotnet restore
```

### 5. Ejecutar la API

#### Con Visual Studio
1. Abrir `RentACarCleanArchitecture.slnx` en Visual Studio
2. Establecer `RentACarCleanArchitecture.Api` como startup project
3. Presionar `F5` o clic en "Run"

#### Con CLI
```bash
cd src/RentACarCleanArchitecture.Api
dotnet run
```

La API estará disponible en: `https://localhost:5159`

### 6. Acceder a Swagger UI

Navegar a: `https://localhost:5159/swagger`

## 🧪 Ejecución de Tests

### Tests Unitarios de Dominio
```bash
dotnet test test/RentACarCleanArchitecture.Domain.UnitTests
```

### Tests Unitarios de Aplicación
```bash
dotnet test test/RentACarCleanArchitecture.Application.UnitTests
```

### Tests de Integración
```bash
dotnet test test/RentACarCleanArchitecture.Application.IntegrationTests
```

### Tests Funcionales de API
```bash
dotnet test test/RentACarCleanArchitecture.Api.FunctionalTests
```

### Tests de Arquitectura
```bash
dotnet test test/RentACarCleanArchitecture.ArchitectureTests
```

### Ejecutar Todos los Tests
```bash
dotnet test
```

## 🐳 Docker

### Construir la Imagen

```bash
docker build -t rentacar:latest -f src/RentACarCleanArchitecture.Api/Dockerfile .
```

### Ejecutar el Contenedor

```bash
docker run -p 5000:5000 \
  -e "ConnectionStrings:Database=<connection-string>" \
  -e "Jwt:SecretKey=<secret-key>" \
  rentacar:latest
```

## 📝 Endpoints Principales

### Usuarios
- `POST /v1/users` - Registro de usuario
- `POST /v1/users/login` - Login
- `GET /v1/users` - Listar usuarios (paginado)

### Vehículos
- `GET /v1/vehiculos` - Listar vehículos disponibles (paginado)
- `GET /v1/vehiculos/{id}` - Detalle de vehículo
- `POST /v1/vehiculos` - Crear vehículo (Admin)
- `PUT /v1/vehiculos/{id}` - Actualizar vehículo (Admin)

### Alquileres
- `POST /v1/alquileres/reservar` - Reservar vehículo
- `GET /v1/alquileres/{id}` - Detalle de alquiler
- `PATCH /v1/alquileres/{id}/confirmar` - Confirmar reserva
- `PATCH /v1/alquileres/{id}/cancelar` - Cancelar alquiler

### Reseñas
- `POST /v1/reviews` - Crear reseña
- `GET /v1/reviews/vehiculo/{vehiculoId}` - Listar reseñas de vehículo

## 🔐 Autenticación y Autorización

### Flujo de Autenticación

1. Usuario se registra (`POST /v1/users`)
2. Usuario realiza login (`POST /v1/users/login`) → recibe JWT
3. JWT se envía en header: `Authorization: Bearer <token>`

### Roles y Permisos

El sistema implementa autorización basada en permisos:
- **Admin**: Acceso total
- **User**: Acceso a vehículos y alquileres personales
- **Reviewer**: Puede crear reseñas

## 🔧 Configuración

### `appsettings.json`

```json
{
  "ConnectionStrings": {
	"Database": "..." // PostgreSQL connection string
  },
  "Jwt": {
	"SecretKey": "...",
	"Issuer": "http://localhost:5159",
	"Audience": "http://localhost:5159"
  },
  "Serilog": {
	"MinimumLevel": "Information",
	"WriteTo": [
	  { "Name": "Console" },
	  { "Name": "Seq", "Args": { "serverUrl": "http://localhost:5341" } }
	]
  }
}
```

### Entornos

- **Development** (`appsettings.Development.json`): Logging verboso
- **Test** (`appsettings.Test.json`): Configuración de testing
- **Production**: Se define en variables de entorno

## 📊 Logging

La aplicación usa **Serilog** para logging estructurado:

- **Console Sink**: Logs en consola (desarrollo)
- **Seq Sink**: Logs centralizados en Seq (producción) - `http://localhost:5341`

Niveles de log:
- `Information` - Eventos importantes de aplicación
- `Warning` - Situaciones inusuales
- `Error` - Errores
- `Debug` - Información de debugging (solo desarrollo)

## 🚨 Manejo de Errores

La API devuelve respuestas de error estructuradas:

```json
{
  "code": "USER_NOT_FOUND",
  "message": "El usuario no fue encontrado",
  "statusCode": 404
}
```

## 📈 Performance

- **Pagination**: Soporte para paginación en listados
- **Dapper Queries**: Consultas SQL optimizadas con Dapper para lecturas
- **EF Core**: Entity Framework Core para persistencia transaccional
- **Indexing**: Índices en base de datos para búsquedas frecuentes

## 🤝 Contribuir

1. Fork el proyecto
2. Crear rama de feature: `git checkout -b feature/AmazingFeature`
3. Commit cambios: `git commit -m 'Add AmazingFeature'`
4. Push a la rama: `git push origin feature/AmazingFeature`
5. Abrir Pull Request

## 📄 Licencia

Este proyecto está bajo licencia MIT. Ver `LICENSE` para más detalles.

## 👨‍💻 Autor

**Santiago Díaz**  
GitHub: [@santydiaz75](https://github.com/santydiaz75)  
Repositorio: [RentACarCleanArchitecture](https://github.com/santydiaz75/RentACarCleanArchitecture)

## 📚 Recursos Adicionales

- [Clean Architecture de Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Domain-Driven Design - Eric Evans](https://domainlanguage.com/ddd/)
- [CQRS Pattern](https://martinfowler.com/bliki/CQRS.html)
- [Documentación de ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)

## ❓ Preguntas Frecuentes

### ¿Cómo cambiar la base de datos de PostgreSQL a otra?

Actualizar el DbContext en `Infrastructure` y cambiar el provider de EF Core (e.g., SQL Server, MySQL).

### ¿Cómo agregar nuevas migraciones?

```bash
dotnet ef migrations add NombreMigracion -p src/RentACarCleanArchitecture.Infrastructure -s src/RentACarCleanArchitecture.Api
```

### ¿Dónde están los eventos de dominio?

En `Domain/<Agregado>/Events/` - se publican desde las entidades y se manejan en `Application/<Agregado>/<Caso>/Handlers`

### ¿Cómo agregar nuevos endpoints?

1. Crear `Command/Query` en `Application`
2. Crear `Handler` que implemente la lógica
3. Crear endpoint en `Api/Controllers` o usar `Minimal APIs`
4. Registrar handler en DI en `Program.cs`

---

**⭐ Si te resulta útil este proyecto, considera darle una estrella en GitHub!**
