# Items Manager

Sistema de gestión de inventario desarrollado como proyecto de practica.

---

##  Acerca del Proyecto

Items Manager es una aplicación web que demuestra conceptos fundamentales de desarrollo de software moderno. Implementa un sistema completo de gestión de inventario con operaciones CRUD, relaciones complejas en base de datos, validaciones y una interfaz responsive y profesional.

Este proyecto fue desarrollado para mostrar mis skills y dominio acerca de ASP NET CORE MVC.

---

## Características Principales

### Backend
- **Patrón MVC** - Separación clara entre Model, View y Controller
- **CRUD Completo** - Create, Read, Update y Delete operations
- **Relaciones de Base de Datos**
  - 1:1 (Items ↔ SerialNumber)
  - 1:N (Category → Items)
  - N:N (Items ↔ Clients con tabla intermedia)
- **Entity Framework Core** - ORM con migraciones automáticas
- **Async/Await** - Operaciones asincrónicas con Task
- **Validaciones** - Data Annotations y validación en formularios
- **Seeding de Datos** - Datos iniciales con OnModelCreating

### Frontend
- **Diseño Responsivo** - Mobile-first, funciona en todos los dispositivos
- **CSS Avanzado** - Gradientes, animaciones, Flexbox, Grid
- **Interfaz Moderna** - Glassmorphism, transiciones suaves
- **Animaciones** - Scroll-triggered con Intersection Observer
- **Bootstrap 5** - Framework CSS para componentes
- **Font Awesome** - Iconografía profesional

---

## Stack Tecnológico

| Capa | Tecnología | Versión |
|------|------------|---------|
| **Backend** | ASP.NET Core | 8.0 |
| **Lenguaje** | C# | 12.0 |
| **ORM** | Entity Framework Core | 8.0 |
| **Base de Datos** | SQL Server | 2019+ |
| **Frontend** | HTML5 + CSS3 | Latest |
| **Framework CSS** | Bootstrap | 5.3 |
| **JavaScript** | Vanilla JS | ES6+ |
| **Iconos** | Font Awesome | 6.4.0 |

---

##  Conceptos Implementados

### Patrones y Arquitectura
- [x] Patrón MVC (Model-View-Controller)
- [x] SOLID Principles (Single Responsibility)
- [x] Separación de responsabilidades
- [x] Repository pattern (a través de DbContext)

### Base de Datos
- [x] Relaciones 1:1 (Items - SerialNumber)
- [x] Relaciones 1:N (Category - Items)
- [x] Relaciones N:N (Items - Clients con tabla intermedia)
- [x] Foreign Keys y cascading deletes
- [x] Migraciones con Entity Framework
- [x] Seeding de datos automático

### C# y LINQ
- [x] Async/Await y Task
- [x] LINQ queries (Select, Where, Include)
- [x] LINQ to Entities
- [x] String interpolation
- [x] Null-coalescing operators

### ASP.NET Core
- [x] Routing conventions
- [x] Dependency Injection
- [x] ModelBinding y Data Annotations
- [x] ActionResults (View, RedirectToAction)
- [x] ViewData y ViewBag
- [x] Tag Helpers (asp-for, asp-items, etc.)

### Frontend
- [x] CSS Variables
- [x] CSS Gradients (linear, 135deg)
- [x] Flexbox Layout
- [x] CSS Grid
- [x] Media Queries (responsive)
- [x] CSS Animations (@keyframes)
- [x] CSS Transitions
- [x] Glassmorphism (backdrop-filter)

### JavaScript
- [x] Intersection Observer API
- [x] DOM Manipulation (querySelector, classList)
- [x] Event Handling
- [x] ES6+ Features (arrow functions, const/let)

---

##  Instalación

### Requisitos Previos
- **.NET 8.0 SDK** - [Descargar](https://dotnet.microsoft.com/download)
- **SQL Server** - [Descargar](https://www.microsoft.com/sql-server/sql-server-downloads)
- **Visual Studio Code o Visual Studio** (opcional)
- **Git** - [Descargar](https://git-scm.com/)

### Pasos

1. **Clonar el repositorio**
```bash
git clone https://github.com/tu-usuario/Items-Manager.git
cd Items-Manager
```

2. **Restaurar dependencias**
```bash
dotnet restore
```

3. **Configurar base de datos** (en `appsettings.json`)
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=ItemsManagerDB;Trusted_Connection=true;"
}
```

4. **Aplicar migraciones**
```bash
dotnet ef database update
```

5. **Ejecutar la aplicación**
```bash
dotnet run
```

6. **Acceder a la aplicación**
```
http://localhost:5000/
```

---

##  Estructura del Proyecto

```
Items-Manager/
├── Controllers/
│   ├── HomeController.cs
│   └── ItemsController.cs
├── Models/
│   ├── Item.cs
│   ├── Category.cs
│   ├── SerialNumber.cs
│   ├── Client.cs
│   └── ItemClient.cs
├── Data/
│   ├── MyAppContext.cs
│   └── Migrations/
├── Views/
│   ├── Shared/
│   │   └── _Layout.cshtml
│   ├── Home/
│   │   ├── Index.cshtml (Landing Page)
│   │   └── Privacy.cshtml
│   └── Items/
│       ├── Index.cshtml
│       ├── Create.cshtml
│       ├── Edit.cshtml
│       └── Delete.cshtml
├── wwwroot/
│   ├── css/
│   │   ├── site.css
│   │   └── home-landing.css
│   ├── js/
│   │   └── site.js
│   └── lib/
│       ├── bootstrap/
│       └── jquery/
├── appsettings.json
└── Program.cs
```

---

##  Uso

### Landing Page
Accede a `http://localhost:5000/` para ver la landing page educativa del proyecto con:
- Resumen de conceptos aprendidos
- Stack tecnológico utilizado
- Call-to-action al sistema de inventario

### Sistema de Gestión de Inventario
Accede a `http://localhost:5000/Items` para:
- **Ver Items** - Tabla con todos los productos
- **Crear Item** - Formulario para agregar nuevo item
- **Editar Item** - Actualizar datos del producto
- **Eliminar Item** - Remover un item del inventario

### Características
- Cada item puede tener:
  - Nombre y precio
  - Categoría (relación 1:N)
  - Número de serie único (relación 1:1)
  - Múltiples clientes asociados (relación N:N)

---

##  Relaciones de Base de Datos

```
┌─────────────┐
│  Category   │
└──────┬──────┘
       │ 1:N
       │
       ▼
┌─────────────┐         ┌───────────────┐
│    Item     │◄───────►│ SerialNumber  │ (1:1)
└──────┬──────┘         └───────────────┘
       │
       │ N:N (con tabla intermedia)
       │
       ▼
┌─────────────────┐
│  ItemClient     │
└────┬────────┬───┘
     │        │
     ▼        ▼
┌────────┐ ┌────────┐
│ Client │ │  Item  │
└────────┘ └────────┘
```

---

##  Estilos y Diseño

### Paleta de Colores
- **Primario:** `#2563eb` (Azul)
- **Secundario:** `#10b981` (Verde)
- **Peligro:** `#ef4444` (Rojo)
- **Warning:** `#f59e0b` (Naranja)
- **Dark:** `#1f2937` (Gris oscuro)

### Tipografía
- **Font:** Segoe UI, Tahoma, Geneva, sans-serif
- **Headings:** 700 weight
- **Body:** 400-500 weight

### Animaciones
- Hero icon bounce: 2s infinite
- Feature cards fade-in: 0.6s ease-out
- Button hover: 0.3s ease with translateY
- Smooth scroll behavior

---

##  Testing

### Pruebas Manuales Recomendadas

1. **CRUD Operations**
   - [ ] Crear un nuevo item
   - [ ] Editar un item existente
   - [ ] Ver lista de items
   - [ ] Eliminar un item

2. **Relaciones**
   - [ ] Asignar categoría a un item
   - [ ] Verificar que se muestre la categoría
   - [ ] Crear múltiples items con misma categoría
   - [ ] Asociar múltiples clientes a un item

3. **Responsive**
   - [ ] Probar en desktop (1920x1080)
   - [ ] Probar en tablet (768x1024)
   - [ ] Probar en móvil (375x667)

4. **Animaciones**
   - [ ] Verificar fade-in de tarjetas en scroll
   - [ ] Probar hover effects en botones
   - [ ] Verificar smooth scroll

---

##  Recursos de Aprendizaje

### Conceptos Claves
- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core/)
- [C# Language](https://docs.microsoft.com/dotnet/csharp/)
- [MDN Web Docs](https://developer.mozilla.org/)
- [Bootstrap Documentation](https://getbootstrap.com/docs/)


---

## Contribuciones

Este es un proyecto educativo. Las contribuciones no son esperadas, pero cualquier feedback es bienvenido.

---

##  Licencia

Este proyecto está bajo licencia MIT. Ver archivo `LICENSE` para más detalles.

---

##  Autor

**[Brayan Jimenez]**
- 📧 Email: [brayanjimenezdev@outlook.com]
- 💼 LinkedIn: [https://www.linkedin.com/in/brayanjimenezdev/]
- 🐙 GitHub: [@BrayanJ732](https://github.com/Brayanj732)

---

## Checklist de Características

### Backend
- [x] Models definidos
- [x] DbContext configurado
- [x] Migraciones implementadas
- [x] Controllers CRUD
- [x] Relaciones 1:1
- [x] Relaciones 1:N
- [x] Relaciones N:N
- [x] Validaciones
- [x] Seeding de datos

### Frontend
- [x] Landing page
- [x] Tabla de items
- [x] Formularios (Create/Edit)
- [x] Confirmación de delete
- [x] Responsive design
- [x] Animaciones
- [x] Iconos
- [x] Separación de CSS

---

## Lo que Aprendí

Este proyecto me permitió practicar y solidificar conocimientos en:

Arquitectura MVC
Entity Framework Core avanzado
Async/Await en C#
LINQ queries complejas
CSS avanzado (gradientes, animaciones)
Responsive design (mobile-first)
Buenas prácticas de código
Separación de responsabilidades
Versionado con Git

---

## Screenshots

### Landing Page
![Landing Page Screenshot]

### Items Index
![Items Table Screenshot]

### Form Create
![Create Form Screenshot]

*Nota: Agregar screenshots reales después*

