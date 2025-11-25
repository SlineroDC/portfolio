<script setup>
import { X, Map, Smartphone, Github, ExternalLink } from 'lucide-vue-next';

// Emitimos 'close' para que el padre sepa cuándo cerrar
defineEmits(['close']);
</script>

<template>
  <!-- Backdrop oscuro con blur -->
  <div class="fixed inset-0 z-[100] flex items-center justify-center p-4 liquid-backdrop" @click="$emit('close')">
    
    <!-- Contenedor del Modal -->
    <!-- @click.stop evita que se cierre si haces clic DENTRO del modal -->
    <div 
      class="bg-[#0a0a0a]/90 backdrop-blur-3xl w-full max-w-4xl h-[85vh] rounded-[2rem] shadow-2xl flex flex-col relative overflow-hidden border border-white/10" 
      @click.stop
    >
      
      <!-- Header Sticky -->
      <div class="p-6 border-b border-white/5 flex justify-between items-center sticky top-0 bg-[#0a0a0a]/90 z-20 backdrop-blur-md">
        <div>
          <h2 class="text-2xl font-semibold text-white">Proyectos Destacados</h2>
          <p class="text-xs text-gray-500">Casos de estudio recientes</p>
        </div>
        <button 
          @click="$emit('close')" 
          class="w-10 h-10 rounded-full bg-white/5 flex items-center justify-center hover:bg-white/10 transition-colors text-white"
        >
          <X class="w-5 h-5" />
        </button>
      </div>

      <!-- Cuerpo Scrollable -->
      <div class="flex-1 overflow-y-auto p-6 md:p-10 space-y-16">
        
        <!-- PROYECTO 1: Connect BQ -->
        <div class="grid md:grid-cols-2 gap-10">
           <!-- Texto -->
           <div class="order-2 md:order-1 space-y-5">
              <div class="flex gap-2">
                <span class="px-3 py-1 bg-blue-500/10 text-blue-400 text-[10px] font-bold uppercase rounded-full border border-blue-500/20">Scrum Master</span>
                <span class="px-3 py-1 bg-white/5 text-gray-400 text-[10px] font-bold uppercase rounded-full border border-white/10">Backend</span>
              </div>
              
              <h3 class="text-3xl font-bold text-white">Connect BQ</h3>
              <p class="text-gray-400 text-sm leading-relaxed font-light">
                MVP para optimizar la movilidad urbana en Barranquilla. Como <strong>Product Owner & Scrum Master</strong>, lideré un equipo de 5 desarrolladores. Además, diseñé la arquitectura de la API RESTful.
              </p>
              
              <!-- Tech Tags -->
              <div class="flex flex-wrap gap-2 text-xs font-mono text-gray-500">
                  <span class="px-2 py-1 bg-white/5 rounded border border-white/5">Node.js</span>
                  <span class="px-2 py-1 bg-white/5 rounded border border-white/5">Express</span>
                  <span class="px-2 py-1 bg-white/5 rounded border border-white/5">MongoDB</span>
                  <span class="px-2 py-1 bg-white/5 rounded border border-white/5">Leaflet</span>
              </div>

              <!-- Links -->
              <div class="pt-4 flex gap-4">
                 <a href="https://github.com/connect-bq/bq_connect" target="_blank" rel="noopener noreferrer" class="px-5 py-2.5 bg-white text-black rounded-lg font-medium text-sm hover:bg-gray-200 transition-colors flex items-center gap-2">
                    <Github class="w-4 h-4"/> Repo
                 </a>
                 <a href="https://bq-connect.vercel.app/" target="_blank" rel="noopener noreferrer" class="px-5 py-2.5 border border-white/10 text-white rounded-lg font-medium text-sm hover:bg-white/5 transition-colors flex items-center gap-2">
                    <ExternalLink class="w-4 h-4"/> Demo
                 </a>
              </div>
           </div>
           
           <!-- Imagen / Preview -->
           <div class="order-1 md:order-2 h-64 rounded-2xl bg-gradient-to-br from-[#0f172a] to-black border border-white/10 flex items-center justify-center relative overflow-hidden group">
              <!-- Un mapa genérico de fondo -->
              <div class="absolute inset-0 bg-[url('https://maps.wikimedia.org/img/osm-intl,12,10.9639,-74.7964,300x200.png')] bg-cover opacity-30 grayscale group-hover:grayscale-0 transition-all duration-700"></div>
              <Map class="w-16 h-16 text-white relative z-10 drop-shadow-lg" />
           </div>
        </div>

        <!-- Divisor Sutil -->
        <div class="h-px bg-gradient-to-r from-transparent via-white/10 to-transparent w-full"></div>

        <!-- PROYECTO 2: Bill Up -->
        <div class="grid md:grid-cols-2 gap-10">
           <!-- Imagen -->
           <div class="h-64 rounded-2xl bg-gradient-to-br from-[#064e3b] to-black border border-white/10 flex items-center justify-center relative overflow-hidden group">
              <div class="absolute inset-0 bg-emerald-900/20"></div>
              <Smartphone class="w-16 h-16 text-emerald-500/80 group-hover:scale-110 transition-transform duration-500 relative z-10" />
           </div>
           
           <!-- Texto -->
           <div class="space-y-5">
              <div class="flex gap-2">
                <span class="px-3 py-1 bg-emerald-500/10 text-emerald-400 text-[10px] font-bold uppercase rounded-full border border-emerald-500/20">Frontend</span>
              </div>
              
              <h3 class="text-3xl font-bold text-white">Bill Up</h3>
              <p class="text-gray-400 text-sm leading-relaxed font-light">
                Aplicación SPA gamificada para educación financiera en jóvenes. Me enfoqué en la **reactividad** de la interfaz y la lógica de "logros" usando Vue.js y TailwindCSS.
              </p>
              
              <div class="flex flex-wrap gap-2 text-xs font-mono text-gray-500">
                  <span class="px-2 py-1 bg-white/5 rounded border border-white/5">Vue.js 3</span>
                  <span class="px-2 py-1 bg-white/5 rounded border border-white/5">Tailwind</span>
                  <span class="px-2 py-1 bg-white/5 rounded border border-white/5">Json-Server</span>
              </div>

              <div class="pt-4 flex gap-4">
                 <a href="https://github.com/WillmanGZ/Bills-Up" target="_blank" rel="noopener noreferrer" class="px-5 py-2.5 bg-white text-black rounded-lg font-medium text-sm hover:bg-gray-200 transition-colors flex items-center gap-2">
                    <Github class="w-4 h-4"/> Repo
                 </a>
                 <a href="https://bills-up.vercel.app/" target="_blank" rel="noopener noreferrer" class="px-5 py-2.5 border border-white/10 text-white rounded-lg font-medium text-sm hover:bg-white/5 transition-colors flex items-center gap-2">
                    <ExternalLink class="w-4 h-4"/> Demo
                 </a>
              </div>
           </div>
        </div>

      </div>
    </div>
  </div>
</template>