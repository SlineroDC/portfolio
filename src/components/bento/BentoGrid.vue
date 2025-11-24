<script setup>
// Importamos los componentes UI reutilizables
import GlassCard from '../ui/GlassCard.vue';
import PillBadge from '../ui/PillBadge.vue';

// Importamos los iconos necesarios
import { 
  Activity, GitCommit, FileCode, Cpu, Database, Container, 
  Terminal, Layout, Flag, FolderOpen, ArrowUpRight, 
  Linkedin, Github, Mail, MessageCircle 
} from 'lucide-vue-next';

// Props: Recibimos datos dinámicos desde el padre (App.vue)
defineProps({
  apiStatus: {
    type: Object,
    required: true,
    // Estructura esperada: { activity: String, message: String }
  }
});

// Emits: Definimos qué señales enviaremos hacia arriba
const emit = defineEmits(['open-projects', 'open-stack']);
</script>

<template>
  <div class="grid grid-cols-1 md:grid-cols-4 gap-4 md:gap-5 auto-rows-[auto]">
    
    <!-- 1. HERO / BIO (2 columnas x 2 filas) -->
    <GlassCard class="md:col-span-2 md:row-span-2 p-8 flex flex-col justify-center relative overflow-hidden group">
      <div class="relative z-10">
        <!-- Badge reutilizable -->
        <div class="mb-6">
          <PillBadge color="emerald" :pulse="true">Open to Work</PillBadge>
        </div>
        
        <h1 class="text-3xl md:text-5xl font-semibold mb-6 leading-[1.1] text-white tracking-tight">
          De Logística a <br>
          <span class="text-transparent bg-clip-text bg-gradient-to-r from-gray-100 to-gray-500">
            Código Eficiente.
          </span>
        </h1>
        
        <p class="text-gray-400 text-lg leading-relaxed font-light max-w-md">
          Desarrollador Full Stack. Transformo mi experiencia en gestión de procesos en arquitecturas de software robustas.
          <br>
          <span class="text-emerald-400/80 text-sm mt-2 block font-medium">
            "Applying accounting logic to find bugs."
          </span>
        </p>
      </div>
      
      <!-- Efecto de luz ambiental (Glow) -->
      <div class="absolute -right-20 -top-20 w-80 h-80 bg-emerald-500/10 rounded-full blur-[80px] group-hover:bg-emerald-500/15 transition-colors duration-700 pointer-events-none"></div>
    </GlassCard>

    <!-- 2. LIVE STATUS (1x1) -->
    <GlassCard class="md:col-span-1 p-6 flex flex-col justify-between relative overflow-hidden group">
      <div class="flex justify-between items-start">
        <div class="p-2 bg-white/5 rounded-full border border-white/10 text-gray-400">
          <Activity class="w-5 h-5" />
        </div>
        <!-- Ping Animation -->
        <span class="flex h-3 w-3 relative">
          <span class="animate-ping absolute inline-flex h-full w-full rounded-full bg-emerald-400 opacity-50"></span>
          <span class="relative inline-flex rounded-full h-3 w-3 bg-emerald-500"></span>
        </span>
      </div>
      
      <div class="mt-4">
        <p class="text-[10px] text-gray-500 uppercase font-bold tracking-widest mb-1">Live Status</p>
        <p class="text-lg font-semibold text-white truncate group-hover:text-emerald-400 transition-colors">
          {{ apiStatus.activity }}
        </p>
        
        <!-- Último commit simulado -->
        <div class="mt-3 py-1.5 px-3 rounded-lg bg-black/40 border border-white/5 flex items-center gap-2">
          <GitCommit class="w-3 h-3 text-gray-500" />
          <span class="text-xs text-gray-400 font-mono truncate">
            {{ apiStatus.message }}
          </span>
        </div>
      </div>
    </GlassCard>

    <!-- 3. AVATAR / FOTO (1x1) -->
    <GlassCard class="md:col-span-1 p-0 overflow-hidden relative group border-0">
      <!-- NOTA: Asegúrate de poner tu foto real en /public/images/profile.png -->
      <img 
        src="/images/profile.png" 
        alt="Sebastian Linero" 
        class="w-full h-full object-cover opacity-80 group-hover:opacity-100 group-hover:scale-105 transition-all duration-700 grayscale hover:grayscale-0" 
        onerror="this.src='https://ui-avatars.com/api/?name=Sebastian+Linero&background=0D8ABC&color=fff&size=512'"
      />
      <div class="absolute inset-0 bg-gradient-to-t from-black/90 via-transparent to-transparent opacity-60"></div>
      <div class="absolute bottom-4 left-4">
          <p class="text-white text-sm font-medium">Sebastian Linero</p>
          <p class="text-gray-400 text-xs">Full Stack Dev</p>
      </div>
    </GlassCard>

    <!-- 4. TECH STACK (2x1) - CLICKABLE -->
    <GlassCard 
      @click="emit('open-stack')"
      class="md:col-span-2 p-0 relative overflow-hidden cursor-pointer group"
    >
      <!-- Sombra lateral para el efecto infinito -->
      <div class="absolute inset-0 bg-gradient-to-r from-[#050505] via-transparent to-[#050505] z-20 pointer-events-none"></div>
      
      <div class="h-full flex flex-col justify-center py-6 relative z-10">
        <p class="text-center text-[10px] font-bold text-gray-600 uppercase mb-5 tracking-[0.3em] group-hover:text-white transition-colors">
          Tech Stack
        </p>
        
        <!-- Marquee Infinito -->
        <div class="relative flex overflow-hidden w-full opacity-60 group-hover:opacity-100 transition-opacity duration-500">
          <div class="flex animate-marquee whitespace-nowrap gap-12 group-hover:[animation-play-state:paused] items-center px-4">
            <!-- Iteramos 2 veces para llenar el ancho y que el loop no salte -->
            <span v-for="n in 2" :key="n" class="flex gap-12 items-center text-lg font-medium text-gray-400">
              <span class="flex items-center gap-3"><FileCode class="w-5 h-5"/> Vue.js</span>
              <span class="flex items-center gap-3 text-gray-200"><Cpu class="w-5 h-5"/> .NET Core</span>
              <span class="flex items-center gap-3"><Database class="w-5 h-5"/> SQL</span>
              <span class="flex items-center gap-3"><Container class="w-5 h-5"/> Docker</span>
              <span class="flex items-center gap-3 text-emerald-500"><Terminal class="w-5 h-5"/> Node.js</span>
              <span class="flex items-center gap-3"><Layout class="w-5 h-5"/> Tailwind</span>
            </span>
          </div>
        </div>
      </div>
    </GlassCard>

    <!-- 5. F1 PASSION (1x1) -->
    <!-- Esta tarjeta es única, tiene estilo propio rojo Ferrari -->
    <article class="md:col-span-1 rounded-[2rem] p-6 flex flex-col justify-between relative overflow-hidden group border border-red-900/30 shadow-lg bg-[#0f0202] transition-all duration-500 hover:-translate-y-1 hover:shadow-red-900/20">
      <div class="absolute inset-0 bg-gradient-to-br from-[#300a0a] to-transparent opacity-60"></div>
      
      <!-- Icono decorativo fondo -->
      <div class="absolute -right-4 -top-4 text-red-600/20 opacity-50 transform rotate-12 group-hover:rotate-0 transition-all duration-700">
        <Flag class="w-32 h-32" />
      </div>
      
      <div class="relative z-10">
        <p class="text-[10px] text-red-400 font-bold uppercase tracking-widest">Passion</p>
      </div>
      
      <div class="relative z-10 mt-2">
        <div class="text-3xl font-bold italic mb-2 text-red-100 tracking-tighter">F1</div>
        <p class="text-xs font-light leading-relaxed text-red-200/80">
          "Obsesionado con la optimización. Buscando la vuelta perfecta en el código."
        </p>
      </div>
    </article>

    <!-- 6. PROYECTOS TRIGGER (1x2) - CLICKABLE -->
    <GlassCard 
      @click="emit('open-projects')"
      class="md:col-span-1 md:row-span-2 p-6 relative cursor-pointer group hover:bg-white/10 transition-colors flex flex-col"
    >
      <div class="flex justify-between items-start mb-4">
        <div class="w-10 h-10 bg-white/5 rounded-full border border-white/10 flex items-center justify-center text-gray-300">
          <FolderOpen class="w-5 h-5" />
        </div>
        <div class="w-8 h-8 rounded-full border border-white/10 flex items-center justify-center text-gray-500 group-hover:border-white/40 group-hover:text-white transition-all">
          <ArrowUpRight class="w-4 h-4" />
        </div>
      </div>
      
      <h3 class="text-2xl font-semibold mb-1 text-white">Proyectos</h3>
      <p class="text-xs text-gray-500 mb-8">Ver casos de estudio.</p>
      
      <!-- Mini lista visual -->
      <div class="space-y-3 mt-auto">
        <!-- Item 1 -->
        <div class="flex items-center gap-3 p-3 rounded-xl bg-white/5 border border-white/5 group-hover:border-white/10 transition-colors">
          <div class="w-1.5 h-1.5 rounded-full bg-blue-500 shadow-[0_0_8px_rgba(59,130,246,0.5)]"></div>
          <div>
            <p class="text-sm font-medium text-gray-200">Connect BQ</p>
            <p class="text-[10px] text-gray-500">Mobility MVP</p>
          </div>
        </div>
        <!-- Item 2 -->
        <div class="flex items-center gap-3 p-3 rounded-xl bg-white/5 border border-white/5 group-hover:border-white/10 transition-colors">
          <div class="w-1.5 h-1.5 rounded-full bg-emerald-500 shadow-[0_0_8px_rgba(16,185,129,0.5)]"></div>
          <div>
            <p class="text-sm font-medium text-gray-200">Bill Up</p>
            <p class="text-[10px] text-gray-500">FinTech App</p>
          </div>
        </div>
      </div>
    </GlassCard>

    <!-- 7. CONTACT / SOCIALS (2x1) -->
    <GlassCard class="md:col-span-2 p-6 flex flex-col md:flex-row items-center justify-between gap-6">
      <div class="text-center md:text-left">
        <p class="text-[10px] text-gray-500 uppercase font-bold tracking-widest mb-2">Contacto Directo</p>
        <a href="mailto:sebastianlinero15@gmail.com" class="text-lg font-medium text-white hover:text-emerald-400 transition-colors flex items-center gap-2 font-mono">
          <Mail class="w-4 h-4" /> sebastianlinero15@gmail.com
        </a>
      </div>
      <div class="flex gap-4">
        <a href="https://linkedin.com" target="_blank" class="w-12 h-12 rounded-full bg-white/5 border border-white/10 flex items-center justify-center hover:bg-white/10 hover:scale-110 transition-all text-gray-300 hover:text-white group">
          <Linkedin class="w-5 h-5 group-hover:text-[#0077b5] transition-colors" />
        </a>
        <a href="https://github.com" target="_blank" class="w-12 h-12 rounded-full bg-white/5 border border-white/10 flex items-center justify-center hover:bg-white/10 hover:scale-110 transition-all text-gray-300 hover:text-white group">
          <Github class="w-5 h-5 group-hover:text-white transition-colors" />
        </a>
         <a href="#" class="w-12 h-12 rounded-full bg-white/5 border border-white/10 flex items-center justify-center hover:bg-white/10 hover:scale-110 transition-all text-gray-300 hover:text-white group">
          <MessageCircle class="w-5 h-5 group-hover:text-green-500 transition-colors" />
        </a>
      </div>
    </GlassCard>

  </div>
</template>