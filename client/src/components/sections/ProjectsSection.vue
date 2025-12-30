<script setup>
import { Github, ExternalLink, Map, Smartphone, Terminal, Box } from 'lucide-vue-next';

// Datos de tus proyectos
const projects = [
  {
    id: 3, // ID Nuevo
    title: "Firmeza ERP Platform",
    description: "Solución ERP integral y modular para construcción. Ecosistema de 3 apps: Backoffice (Razor), API REST (.NET 8) y Cliente (Vue 3). Arquitectura Hexagonal y Dockerizada.",
    role: "Full Stack Architect",
    tech: [".NET 8", "Vue 3", "PostgreSQL", "Docker", "Clean Arch"],
    repo: "https://github.com/SlineroDC/modular-erp-dotnet",
    demo: null,
    icon: Box,
    bgClass: "bg-gradient-to-br from-[#2e0249] to-black",
    color: "text-purple-400",
    shadow: "shadow-purple-500/20"
  },
  {
    id: 1,
    title: "Connect BQ",
    description: "MVP para optimizar la movilidad urbana en Barranquilla. Lideré el equipo como Scrum Master y diseñé la arquitectura de la API RESTful con Node.js y GeoJSON.",
    role: "Backend & Scrum Master",
    tech: ["Node.js", "Express", "MongoDB", "Leaflet"],
    repo: "https://github.com/connect-bq/bq_connect",
    demo: "https://bq-connect.vercel.app/",
    icon: Map,
    bgClass: "bg-gradient-to-br from-[#0f172a] to-black",
    color: "text-blue-400",
    shadow: "shadow-blue-500/20"
  },
  {
    id: 4,
    title: "Veterinary Console",
    description: "Sistema de gestión clínica en consola pura. Implementa Arquitectura en 3 Capas, Inyección de Dependencias y RBAC (Roles) sin frameworks visuales. C# puro.",
    role: "Backend Developer",
    tech: ["C#", ".NET 8", "FluentValidation", "POO", "Layered Arch"],
    repo: "https://github.com/SlineroDC/VeterinaryManagementConsole",
    demo: null,
    icon: Terminal,
    bgClass: "bg-gradient-to-br from-[#1a1a1a] to-black border-gray-800",
    color: "text-gray-400",
    shadow: "shadow-gray-500/10"
  },
  {
    id: 2,
    title: "Bill Up",
    description: "SPA gamificada para educación financiera. Enfoque total en reactividad, gestión de estado con Pinia y UX/UI para jóvenes.",
    role: "Frontend Developer",
    tech: ["Vue.js 3", "Tailwind", "Json-Server"],
    repo: "https://github.com/WillmanGZ/Bills-Up",
    demo: "https://bills-up.vercel.app/",
    icon: Smartphone,
    bgClass: "bg-gradient-to-br from-[#064e3b] to-black",
    color: "text-emerald-400",
    shadow: "shadow-emerald-500/20"
  }
];
</script>

<template>
  <div class="space-y-32">

    <div
      class="text-center space-y-4 mb-20"
      v-motion-slide-visible-once
      :visible="{ opacity: 1, y: 0, transition: { duration: 800 } }"
      :initial="{ opacity: 0, y: 50 }"
    >
      <h2 class="text-3xl md:text-5xl font-bold text-white">Proyectos Destacados</h2>
      <p class="text-gray-400 max-w-xl mx-auto text-lg font-light">
        Desde arquitecturas empresariales complejas hasta MVPs ágiles.
      </p>
    </div>

    <div
      v-for="(project, index) in projects"
      :key="project.id"
      class="grid md:grid-cols-2 gap-12 items-center group/project"
      v-motion-slide-visible-once
      :delay="200"
    >

      <div
        class="relative rounded-[2rem] overflow-hidden border border-white/10 h-72 md:h-96 flex items-center justify-center shadow-2xl transition-all duration-500 hover:scale-[1.02]"
        :class="[project.bgClass, project.shadow, index % 2 === 1 ? 'md:order-last' : '']"
      >
        <div class="absolute inset-0 opacity-30 bg-[url('https://www.transparenttextures.com/patterns/cubes.png')]"></div>

        <component
            :is="project.icon"
            class="w-32 h-32 text-white/20 group-hover/project:text-white/40 group-hover/project:scale-110 transition-all duration-700"
        />

        <div class="absolute inset-0 bg-black/60 backdrop-blur-sm opacity-0 group-hover/project:opacity-100 transition-opacity duration-300 flex items-center justify-center gap-6">

           <a
             :href="project.repo"
             target="_blank"
             class="flex flex-col items-center gap-2 text-white hover:text-emerald-400 transition-colors transform translate-y-4 group-hover/project:translate-y-0 duration-300"
           >
             <div class="p-4 bg-white/10 rounded-full hover:bg-white/20 backdrop-blur-md">
               <Github class="w-6 h-6"/>
             </div>
             <span class="text-xs font-bold uppercase tracking-widest">Código</span>
           </a>

           <a
             v-if="project.demo"
             :href="project.demo"
             target="_blank"
             class="flex flex-col items-center gap-2 text-white hover:text-emerald-400 transition-colors transform translate-y-4 group-hover/project:translate-y-0 duration-300 delay-100"
           >
             <div class="p-4 bg-white/10 rounded-full hover:bg-white/20 backdrop-blur-md">
               <ExternalLink class="w-6 h-6"/>
             </div>
             <span class="text-xs font-bold uppercase tracking-widest">Demo</span>
           </a>

           <div
             v-else
             class="flex flex-col items-center gap-2 text-gray-400 transform translate-y-4 group-hover/project:translate-y-0 duration-300 delay-100"
           >
              <div class="p-4 bg-white/5 rounded-full border border-white/5">
                <Terminal class="w-6 h-6"/>
              </div>
              <span class="text-xs font-bold uppercase tracking-widest">CLI App</span>
           </div>

        </div>
      </div>

      <div class="space-y-6 relative">
        <div class="flex items-center gap-3">
           <span class="w-12 h-[2px] bg-white/20"></span>
           <span class="font-mono text-sm uppercase tracking-widest font-bold" :class="project.color">
             {{ project.role }}
           </span>
        </div>

        <h3 class="text-4xl md:text-5xl font-bold text-white leading-tight">
          {{ project.title }}
        </h3>

        <p class="text-gray-400 text-lg leading-relaxed font-light">
          {{ project.description }}
        </p>

        <div class="flex flex-wrap gap-2 pt-2">
          <span
            v-for="tech in project.tech"
            :key="tech"
            class="px-4 py-2 rounded-lg bg-white/5 border border-white/10 text-xs font-mono text-gray-300 hover:border-white/30 transition-colors cursor-default"
          >
            {{ tech }}
          </span>
        </div>

        <div class="md:hidden pt-4 flex gap-4">
           <a :href="project.repo" target="_blank" class="text-white underline decoration-emerald-500 underline-offset-4 font-medium text-sm">
             Ver Código
           </a>
           <a v-if="project.demo" :href="project.demo" target="_blank" class="text-white underline decoration-blue-500 underline-offset-4 font-medium text-sm">
             Ver Demo
           </a>
        </div>

      </div>

    </div>

  </div>
</template>
