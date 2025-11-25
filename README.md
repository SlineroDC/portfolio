# SlineroDC Portfolio: De Logística al Código Eficiente

Este es el portafolio profesional de Sebastian Linero, un Desarrollador Full Stack con una perspectiva única en optimización de procesos, proveniente de una formación en Contabilidad y Logística.

El objetivo de este proyecto es demostrar habilidades en la arquitectura de aplicaciones modernas (SPA), UI/UX avanzado (Liquid Glass Design) y la integración de servicios en tiempo real (Live Status API).

## Características Principales

- Diseño Liquid Glass: Interfaz minimalista, oscura y elegante, inspirada en las tendencias de UI/UX de Apple (Glassmorphism).

- Arquitectura de Componentes: Desarrollado como una Single Page Application (SPA) con Vue 3 para máxima modularidad.

- Interacción Avanzada: Uso de Modales (<Teleport>) para expansión de contenido (Stack, Proyectos, Bio), manteniendo la fluidez de la página.

- Responsive Bento Grid: El layout se adapta a todos los tamaños de pantalla, desde móvil hasta escritorio.

- Demostración Full Stack: Incluye un widget simulado de "Live Status" (apiStatus) que emula la conexión a una API de .NET/C# para mostrar actividad en tiempo real.

- Seguridad: Enlaces externos utilizan target="_blank" rel="noopener noreferrer" para prevención de Tabnabbing.

## Stack Tecnológico

| Categoría | Tecnologías Clave | Versión |
|-----------|-------------------|---------|
| Frontend | Vue 3 (Composition API), JavaScript ES6+ | ^3.4 |
| Styling | Tailwind CSS | ^3.4 |
| Bundler | Vite | ^5.0 |
| Icons | Lucide-vue-next (Iconos como componentes) | ^0.3 |
| Backend Demo | C#, Node.js, Python (Referenciado) | N/A |
| Metodología | Scrum / Agile (Referenciado) | N/A |

📦 Estructura del Proyecto

El proyecto sigue una estructura limpia y fácil de mantener, separando responsabilidades:

```
slinero-portfolio/
├── src/
│   ├── assets/                 # Estilos globales y Tailwind
│   │   ├── base.css           # Estilos base
│   │   └── main.css           # Estilos principales
│   ├── components/
│   │   ├── bento/             # Componentes del Bento Grid
│   │   │   └── BentoGrid.vue
│   │   ├── icons/             # Iconos como componentes
│   │   ├── layout/            # Componentes de layout
│   │   │   ├── AppFooter.vue
│   │   │   └── NavBar.vue
│   │   ├── modals/            # Modales interactivos
│   │   │   ├── BioModal.vue
│   │   │   ├── ContactModal.vue
│   │   │   ├── ProjectModal.vue
│   │   │   └── StackModal.vue
│   │   └── ui/                # Componentes reutilizables
│   │       ├── GlassCard.vue
│   │       └── PillBadge.vue
│   ├── App.vue                # Componente Raíz y Orquestador de Modales
│   └── main.js                # Punto de entrada y carga de estilos
├── public/
│   └── images/                # Archivos estáticos (imágenes, favicon)
│       └── profile.jpeg
├── .editorconfig              # Configuración del editor
├── .eslintignore              # Archivos ignorados por ESLint
├── .gitattributes             # Atributos de Git
├── .gitignore                 # Archivos ignorados por Git
├── eslint.config.js           # Configuración de ESLint
├── index.html                 # HTML principal
├── jsconfig.json              # Configuración de JavaScript
├── package.json               # Dependencias y scripts
├── package-lock.json          # Lock file de npm
├── postcss.config.js          # Configurador de Tailwind para PostCSS
├── tailwind.config.js         # Configuración de colores y animaciones personalizadas
├── vite.config.js             # Configuración del bundler Vite
└── README.md                  # Este archivo
```


⚙️ Instalación y Uso Local

Sigue estos pasos para levantar el portafolio en tu máquina de desarrollo.

Prerequisitos

Necesitas tener instalado Node.js (versión 18+) y npm (incluido con Node.js).

Paso 1: Clonar e Instalar Dependencias

# 1. Clona el repositorio
git clone [https://github.com/SlineroDC/portfolio.git] slinero-portfolio
cd slinero-portfolio

# 2. Instala todas las dependencias
npm install


Paso 2: Ejecutar el Proyecto

Utiliza el comando de desarrollo de Vite. Esto compila el código, aplica Tailwind y levanta un servidor local.

npm run dev


El portafolio estará disponible en http://localhost:5173.

## Personalización

Para que el portafolio sea 100% tuyo:

Imágenes: Reemplaza public/images/profile.png con tu propia foto.

Contacto: Asegúrate de que el mailto: en ContactModal.vue y NavBar.vue sea tu correo personal.

Live Status: Para integrar el widget "Live Status" con una API real (Node.js/C#), consulta la documentación sobre Webhooks de GitHub y el endpoint /status de tu backend.

## Contribuciones y Contacto

Si encuentras algún error o tienes sugerencias de mejora (ej. animaciones GSAP), ¡siéntete libre de abrir un issue o enviar un Pull Request!

Contacto: [sebastianlinero15@gmail.com]

LinkedIn: [https://www.linkedin.com/in/slinerodc/]

GitHub: [https://github.com/SlineroDC]