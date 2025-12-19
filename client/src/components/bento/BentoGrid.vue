<script setup>
import { computed } from 'vue';
import GlassCard from '../ui/GlassCard.vue';
import PillBadge from '../ui/PillBadge.vue';
import { 
  Activity, GitCommit, FileCode, Cpu, Database, Container, 
  Terminal, Layout, Flag, FolderOpen, ArrowUpRight, 
  Linkedin, Github, Mail, MessageCircle, Plus,
  // Nuevos iconos para los estados de la API:
  Dumbbell, Utensils, Mic, Moon, Briefcase, Coffee
} from 'lucide-vue-next';

const props = defineProps({
  apiStatus: { type: Object, required: true }
});

const emit = defineEmits(['open-projects', 'open-stack', 'open-contact', 'open-bio']);

// --- 1. MAPA DE ICONOS (String API -> Componente Lucide) ---
const iconMap = {
  'code': Terminal,
  'dumbbell': Dumbbell,
  'flag': Flag,
  'burger': Utensils,
  'mic': Mic,
  'moon': Moon,
  'briefcase': Briefcase,
  'coffee': Coffee,
  'default': Activity
};

// --- 2. MAPA DE COLORES (String API -> Clases Tailwind) ---
// Definimos background (bg) y shadow/glow para el efecto neon
const colorMap = {
  green:  { bg: 'bg-emerald-500', ping: 'bg-emerald-400', text: 'group-hover:text-emerald-400' },
  red:    { bg: 'bg-red-500',     ping: 'bg-red-400',     text: 'group-hover:text-red-400' },
  orange: { bg: 'bg-orange-500',  ping: 'bg-orange-400',  text: 'group-hover:text-orange-400' },
  yellow: { bg: 'bg-yellow-400',  ping: 'bg-yellow-200',  text: 'group-hover:text-yellow-400' },
  purple: { bg: 'bg-purple-500',  ping: 'bg-purple-400',  text: 'group-hover:text-purple-400' },
  blue:   { bg: 'bg-blue-500',    ping: 'bg-blue-400',    text: 'group-hover:text-blue-400' },
  gray:   { bg: 'bg-gray-500',    ping: 'bg-gray-400',    text: 'group-hover:text-gray-300' }
};

// Computed properties para obtener el icono y color actual de forma segura
const currentIcon = computed(() => iconMap[props.apiStatus.icon] || iconMap.default);
const currentStyle = computed(() => colorMap[props.apiStatus.color] || colorMap.green);

</script>

<template>
  <div class="grid grid-cols-1 md:grid-cols-4 gap-4 md:gap-5 auto-rows-[auto]">
    
    <GlassCard 
      @click="emit('open-bio')"
      class="md:col-span-2 md:row-span-2 p-8 flex flex-col justify-center relative overflow-hidden group cursor-pointer"
    >
      <div class="absolute top-4 right-4 bg-white/5 p-2 rounded-full opacity-0 group-hover:opacity-100 transition-opacity">
          <Plus class="w-4 h-4 text-white" />
      </div>

      <div class="relative z-10">
        <div class="mb-6">
          <PillBadge color="emerald" :pulse="true">Open to Work</PillBadge>
        </div>
        
        <h1 class="text-3xl md:text-5xl font-semibold mb-6 leading-[1.1] text-white tracking-tight">
          De Contabilidad y Logística a <br>
          <span class="text-transparent bg-clip-text bg-gradient-to-r from-gray-100 to-gray-500">
            Código Eficiente.
          </span>
        </h1>
        
        <p class="text-gray-400 text-lg leading-relaxed font-light max-w-md">
          Desarrollador Full Stack. Transformo mi experiencia en Contabilidad y Logística en arquitecturas de software robustas.
          <br>
          <span class="text-emerald-400/80 text-sm mt-2 block font-medium group-hover:text-emerald-300 transition-colors">
            Click para ver mi historia completa.
          </span>
        </p>
      </div>
      <div class="absolute -right-20 -top-20 w-80 h-80 bg-emerald-500/10 rounded-full blur-[80px] group-hover:bg-emerald-500/15 transition-colors duration-700 pointer-events-none"></div>
    </GlassCard>

    <GlassCard class="md:col-span-1 p-6 flex flex-col justify-between relative overflow-hidden group">
      <div class="flex justify-between items-start">
        <div class="p-2 bg-white/5 rounded-full border border-white/10 text-gray-400 transition-colors duration-300"
             :class="currentStyle.text"> <component :is="currentIcon" class="w-5 h-5" />
        </div>
        
        <span class="flex h-3 w-3 relative">
          <span class="animate-ping absolute inline-flex h-full w-full rounded-full opacity-50" 
                :class="currentStyle.ping"></span>
          <span class="relative inline-flex rounded-full h-3 w-3" 
                :class="currentStyle.bg"></span>
        </span>
      </div>
      
      <div class="mt-4">
        <p class="text-[10px] text-gray-500 uppercase font-bold tracking-widest mb-1">Live Status</p>
        
        <p class="text-lg font-semibold text-white truncate transition-colors duration-300"
           :class="currentStyle.text">
          {{ apiStatus.activity }}
        </p>
        
        <div class="mt-3 py-1.5 px-3 rounded-lg bg-black/40 border border-white/5 flex items-center gap-2">
          <GitCommit class="w-3 h-3 text-gray-500" />
          <span class="text-xs text-gray-400 font-mono truncate">
            {{ apiStatus.message }}
          </span>
        </div>
      </div>
    </GlassCard>

    <GlassCard class="md:col-span-1 p-0 overflow-hidden relative group border-0">
      <img 
        src="/images/profile.jpeg" 
        alt="Sebastian Linero" 
        class="w-full h-full object-cover opacity-50 group-hover:opacity-100 group-hover:scale-105 transition-all duration-700 hover:grayscale-0" 
        onerror="this.src='https://ui-avatars.com/api/?name=Sebastian+Linero&background=0D8ABC&color=fff&size=512'"
      />
      <div class="absolute inset-0 bg-gradient-to-t from-black/90 via-transparent to-transparent opacity-60"></div>
      <div class="absolute bottom-4 left-4">
          <p class="text-white text-sm font-medium">Sebastian Linero</p>
          <p class="text-gray-400 text-xs">Full Stack Dev</p>
      </div>
    </GlassCard>

    <GlassCard 
      @click="emit('open-stack')"
      class="md:col-span-2 p-0 relative overflow-hidden cursor-pointer group"
    >
      <div class="absolute inset-0 bg-gradient-to-r from-[#050505] via-transparent to-[#050505] z-20 pointer-events-none"></div>
      <div class="h-full flex flex-col justify-center py-6 relative z-10">
        <p class="text-center text-[10px] font-bold text-gray-600 uppercase mb-5 tracking-[0.3em] group-hover:text-white transition-colors">
          Tech Stack
        </p>
        <div class="relative flex overflow-hidden w-full opacity-60 group-hover:opacity-100 transition-opacity duration-500">
          <div class="flex animate-marquee whitespace-nowrap gap-12 group-hover:[animation-play-state:paused] items-center px-4">
            <span v-for="n in 2" :key="n" class="flex gap-12 items-center text-lg font-medium text-gray-400">
              <span class="flex items-center gap-3  text-green-700"><FileCode class="w-5 h-5"/> Vue.js</span>
              <span class="flex items-center gap-3 text-blue-400"><Cpu class="w-5 h-5"/> .NET Core</span>
              <span class="flex items-center gap-3 text-yellow-400"><Database class="w-5 h-5"/> SQL</span>
              <span class="flex items-center gap-3 text-blue-700"><Container class="w-5 h-5"/> Docker</span>
              <span class="flex items-center gap-3 text-emerald-500"><Terminal class="w-5 h-5"/> Node.js</span>
              <span class="flex items-center gap-3 text-blue-900"><Layout class="w-5 h-5"/> Tailwind</span>
            </span>
          </div>
        </div>
      </div>
    </GlassCard>

    <article class="md:col-span-1 rounded-[2rem] p-6 flex flex-col justify-between relative overflow-hidden group border border-red-900/30 shadow-lg bg-[#0f0202] transition-all duration-500 hover:-translate-y-1 hover:shadow-red-900/20">
      <div class="absolute inset-0 bg-gradient-to-br from-[#300a0a] to-transparent opacity-60"></div>
      <div class="absolute -right-4 -top-4 text-red-600/20 opacity-50 transform rotate-12 group-hover:rotate-0 transition-all duration-700">
        <Flag class="w-32 h-32" />
      </div>
      <div class="relative z-10">
        <p class="text-[10px] text-red-400 font-bold uppercase tracking-widest">Passion</p>
      </div>
      <div class="relative z-10 mt-2">
        <div class="text-3xl font-bold italic mb-2 text-red-100 tracking-tighter">F1</div>
        <p class="text-xs font-light leading-relaxed text-red-200/80">
          "Obsesionado con la optimización y velocidad. Buscando la vuelta perfecta en el código."
        </p>
      </div>
    </article>

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
      
      <div class="space-y-3 mt-auto">
        <div class="flex items-center gap-3 p-3 rounded-xl bg-white/5 border border-white/5 group-hover:border-white/10 transition-colors">
          <div class="w-1.5 h-1.5 rounded-full bg-blue-500 shadow-[0_0_8px_rgba(59,130,246,0.5)]"></div>
          <div>
            <p class="text-sm font-medium text-gray-200">Connect BQ</p>
            <p class="text-[10px] text-gray-500">Mobility MVP</p>
          </div>
        </div>
        <div class="flex items-center gap-3 p-3 rounded-xl bg-white/5 border border-white/5 group-hover:border-white/10 transition-colors">
          <div class="w-1.5 h-1.5 rounded-full bg-emerald-500 shadow-[0_0_8px_rgba(16,185,129,0.5)]"></div>
          <div>
            <p class="text-sm font-medium text-gray-200">Bill Up</p>
            <p class="text-[10px] text-gray-500">FinTech App</p>
          </div>
        </div>
      </div>
    </GlassCard>

    <GlassCard class="md:col-span-2 p-6 flex flex-col md:flex-row items-center justify-between flex-wrap">
      <div class="text-center md:text-left">
        <p class="text-[10px] text-gray-500 uppercase font-bold tracking-widest mb-2">Contacto Directo</p>
        <button @click="emit('open-contact')" class="text-lg font-medium text-white hover:text-emerald-400 transition-colors flex items-center gap-2 font-mono group min-w-0">
          <Mail class="w-4 h-4" /> sebastianlinero15@gmail.com
          <ArrowUpRight class="w-3 h-3 opacity-0 group-hover:opacity-100 transition-opacity transform group-hover:translate-x-1" />
        </button>
      </div>
      
      <div class="flex gap-2 gap-4 mt-4 md:mt-0 flex-shrink-0">
        <a href="https://linkedin.com/in/slinerodc" target="_blank" rel="noopener noreferrer" class="w-12 h-12 rounded-full bg-white/5 border border-white/10 flex items-center justify-center hover:bg-white/10 hover:scale-110 transition-all text-gray-300 hover:text-white group">
          <Linkedin class="w-5 h-5 group-hover:text-[#0077b5] transition-colors" />
        </a>
        <a href="https://github.com/slinerodc" target="_blank" rel="noopener noreferrer" class="w-12 h-12 rounded-full bg-white/5 border border-white/10 flex items-center justify-center hover:bg-white/10 hover:scale-110 transition-all text-gray-300 hover:text-white group">
          <Github class="w-5 h-5 group-hover:text-white transition-colors" />
        </a>
         <button @click="emit('open-contact')" class="w-12 h-12 rounded-full bg-white/5 border border-white/10 flex items-center justify-center hover:bg-white/10 hover:scale-110 transition-all text-gray-300 hover:text-white group">
          <MessageCircle class="w-5 h-5 group-hover:text-green-500 transition-colors" />
        </button>
      </div>
    </GlassCard>

  </div>
</template>