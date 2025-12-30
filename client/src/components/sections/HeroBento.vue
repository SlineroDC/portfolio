<script setup>
import { computed } from 'vue';
import {
  Activity, GitCommit, Linkedin, Github, Mail, Plus,
  Dumbbell, Utensils, Mic, Moon, Briefcase, Coffee, Terminal, Flag,
  ArrowDown
} from 'lucide-vue-next';

const props = defineProps({
  apiStatus: { type: Object, required: true }
});

const emit = defineEmits(['open-contact', 'open-bio']);

// --- LÓGICA DE STATUS---
const iconMap = {
  'code': Terminal, 'dumbbell': Dumbbell, 'flag': Flag, 'burger': Utensils,
  'mic': Mic, 'moon': Moon, 'briefcase': Briefcase, 'coffee': Coffee, 'default': Activity
};

const colorMap = {
  green:  { bg: 'bg-emerald-500', ping: 'bg-emerald-400', text: 'text-emerald-400' },
  red:    { bg: 'bg-red-500',     ping: 'bg-red-400',     text: 'text-red-400' },
  orange: { bg: 'bg-orange-500',  ping: 'bg-orange-400',  text: 'text-orange-400' },
  yellow: { bg: 'bg-yellow-400',  ping: 'bg-yellow-200',  text: 'text-yellow-400' },
  purple: { bg: 'bg-purple-500',  ping: 'bg-purple-400',  text: 'text-purple-400' },
  blue:   { bg: 'bg-blue-500',    ping: 'bg-blue-400',    text: 'text-blue-400' },
  gray:   { bg: 'bg-gray-500',    ping: 'bg-gray-400',    text: 'text-gray-300' }
};

const currentIcon = computed(() => iconMap[props.apiStatus.icon] || iconMap.default);
const currentStyle = computed(() => colorMap[props.apiStatus.color] || colorMap.gray);
</script>

<template>
  <div class="grid grid-cols-1 md:grid-cols-4 gap-4 md:gap-6 auto-rows-[auto]">

    <div
      class="md:col-span-2 md:row-span-2 p-8 md:p-10 rounded-[2rem] bg-[#0a0a0a]/60 backdrop-blur-md border border-white/10 flex flex-col justify-center relative overflow-hidden group hover:border-white/20 transition-all"
      v-motion-slide-visible-once
      :delay="100"
    >
      <div class="absolute top-6 right-6 flex gap-2">
         <span class="px-3 py-1 bg-emerald-500/10 text-emerald-400 text-xs font-bold uppercase rounded-full border border-emerald-500/20 animate-pulse">Open to Work</span>
      </div>

      <div class="relative z-10 mt-4">
        <h1 class="text-4xl md:text-6xl font-bold mb-6 leading-tight text-white tracking-tight">
          Código <br>
          <span class="text-transparent bg-clip-text bg-gradient-to-r from-emerald-400 to-cyan-500">
            Eficiente.
          </span>
        </h1>

        <p class="text-gray-400 text-lg leading-relaxed font-light max-w-md">
          De la <strong>Contabilidad</strong> al <strong>Desarrollo Full Stack</strong>.
          Transformo procesos complejos en arquitecturas de software robustas con .NET y Vue.
        </p>

        <button @click="emit('open-bio')" class="mt-8 text-white font-medium hover:text-emerald-400 flex items-center gap-2 transition-colors group/btn">
            Leer mi historia <Plus class="w-4 h-4 group-hover/btn:rotate-90 transition-transform"/>
        </button>
      </div>

      <div class="absolute -right-20 -top-20 w-96 h-96 bg-emerald-500/5 rounded-full blur-[100px] group-hover:bg-emerald-500/10 transition-colors duration-700 pointer-events-none"></div>
    </div>

    <div
      class="md:col-span-1 md:row-span-2 rounded-[2rem] border border-white/10 overflow-hidden relative group"
      v-motion-slide-visible-once
      :delay="200"
    >
      <img
        src="/images/profile.jpeg"
        alt="Sebastian Linero"
        class="w-full h-full object-cover opacity-80 group-hover:opacity-100 group-hover:scale-105 transition-all duration-700"
        onerror="this.src='https://ui-avatars.com/api/?name=Sebastian+Linero&background=0D8ABC&color=fff&size=512'"
      />
      <div class="absolute inset-0 bg-gradient-to-t from-black/90 via-transparent to-transparent"></div>
      <div class="absolute bottom-6 left-6">
        <p class="text-white text-lg font-bold">Sebastian Linero</p>
        <p class="text-gray-400 text-sm">Barranquilla, CO 🇨🇴</p>
      </div>
    </div>

    <div
      class="md:col-span-1 rounded-[2rem] p-6 bg-[#0a0a0a]/60 backdrop-blur-md border border-white/10 flex flex-col justify-between group hover:border-white/20 transition-all"
      v-motion-slide-visible-once
      :delay="300"
    >
      <div class="flex justify-between items-start">
        <div class="p-2 bg-white/5 rounded-full border border-white/10 text-gray-300">
           <component :is="currentIcon" class="w-5 h-5" />
        </div>
        <span class="flex h-3 w-3 relative">
          <span class="animate-ping absolute inline-flex h-full w-full rounded-full opacity-50" :class="currentStyle.ping"></span>
          <span class="relative inline-flex rounded-full h-3 w-3" :class="currentStyle.bg"></span>
        </span>
      </div>

      <div class="mt-4">
        <p class="text-[10px] text-gray-500 uppercase font-bold tracking-widest mb-1">Live Status</p>
        <p class="text-xl font-bold text-white truncate" :class="currentStyle.text">
          {{ apiStatus.activity }}
        </p>
        <div class="mt-2 flex items-center gap-2">
           <GitCommit class="w-3 h-3 text-gray-600"/>
           <span class="text-xs text-gray-500 truncate max-w-[120px]">{{ apiStatus.message }}</span>
        </div>
      </div>
    </div>

    <div
      class="md:col-span-1 rounded-[2rem] p-6 bg-[#0f0202] border border-red-900/30 flex flex-col justify-between relative overflow-hidden group shadow-lg hover:shadow-red-900/20 transition-all"
      v-motion-slide-visible-once
      :delay="400"
    >
       <div class="absolute inset-0 bg-gradient-to-br from-[#300a0a] to-transparent opacity-60"></div>
       <div class="absolute -right-4 -top-4 text-red-600/20 opacity-50 transform rotate-12 group-hover:rotate-0 transition-transform duration-700">
         <Flag class="w-32 h-32" />
       </div>
       <div class="relative z-10">
         <p class="text-[10px] text-red-400 font-bold uppercase tracking-widest">Passion</p>
         <div class="text-4xl font-black italic text-white mt-2">F1</div>
       </div>
       <p class="relative z-10 text-xs font-light text-red-200/80 mt-2">
         "Obsesionado con la optimización y la velocidad."
       </p>
    </div>

    <div
      class="md:col-span-4 rounded-[2rem] p-4 bg-white/5 border border-white/10 flex flex-col md:flex-row items-center justify-between gap-4 backdrop-blur-sm"
      v-motion-slide-visible-once
      :delay="500"
    >
       <a href="#projects" class="flex items-center gap-3 px-6 py-3 rounded-full bg-white/5 hover:bg-white/10 transition-colors text-sm text-gray-300 group/scroll">
          <div class="w-8 h-8 rounded-full border border-white/10 flex items-center justify-center">
            <ArrowDown class="w-4 h-4 group-hover/scroll:animate-bounce"/>
          </div>
          Explorar Proyectos
       </a>

       <div class="flex gap-3">
          <a href="https://linkedin.com/in/slinerodc" target="_blank" class="w-12 h-12 rounded-full bg-black/50 border border-white/10 flex items-center justify-center hover:text-[#0077b5] hover:border-[#0077b5]/50 transition-all text-gray-400">
            <Linkedin class="w-5 h-5"/>
          </a>
          <a href="https://github.com/slinerodc" target="_blank" class="w-12 h-12 rounded-full bg-black/50 border border-white/10 flex items-center justify-center hover:text-white hover:border-white transition-all text-gray-400">
            <Github class="w-5 h-5"/>
          </a>
          <button @click="emit('open-contact')" class="w-12 h-12 rounded-full bg-black/50 border border-white/10 flex items-center justify-center hover:text-emerald-400 hover:border-emerald-400/50 transition-all text-gray-400">
            <Mail class="w-5 h-5"/>
          </button>
       </div>
    </div>

  </div>
</template>
