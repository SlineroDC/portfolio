<script setup>
import { X, Cpu, Terminal, Database, FileCode, Layout, Container, GitBranch, Users } from 'lucide-vue-next';
defineEmits(['close']);
</script>

<template>
  <div class="fixed inset-0 z-[100] flex items-center justify-center p-4 liquid-backdrop" @click="$emit('close')">
    <div class="bg-[#0a0a0a]/90 backdrop-blur-3xl w-full max-w-3xl rounded-[2rem] shadow-2xl p-8 border border-white/10 relative" @click.stop>
      
      <!-- Botón Cerrar Flotante -->
      <button @click="$emit('close')" class="absolute top-6 right-6 w-8 h-8 rounded-full bg-white/5 flex items-center justify-center hover:bg-white/10 transition-colors text-white">
        <X class="w-4 h-4" />
      </button>
      
      <h2 class="text-2xl font-semibold text-white mb-8">Arsen tecnológico</h2>
      
      <div class="space-y-8">
          
          <!-- Backend -->
          <div>
              <h3 class="text-xs font-bold text-gray-500 uppercase mb-4 tracking-widest flex items-center gap-2">
                  <Cpu class="w-4 h-4"/> Backend & Core
              </h3>
              <div class="grid grid-cols-2 md:grid-cols-3 gap-3">
                  <div class="tech-item"><Cpu class="text-purple-400"/> .NET Core / C#</div>
                  <div class="tech-item"><Terminal class="text-green-500"/> Node.js</div>
                  <div class="tech-item"><Database class="text-blue-400"/> SQL / MongoDB</div>
              </div>
          </div>
          
          <!-- Frontend -->
          <div>
              <h3 class="text-xs font-bold text-gray-500 uppercase mb-4 tracking-widest flex items-center gap-2">
                  <Layout class="w-4 h-4"/> Frontend
              </h3>
              <div class="grid grid-cols-2 md:grid-cols-3 gap-3">
                  <div class="tech-item"><FileCode class="text-emerald-400"/> Vue.js 3</div>
                  <div class="tech-item"><Layout class="text-cyan-400"/> Tailwind CSS</div>
                  <div class="tech-item"><FileCode class="text-yellow-400"/> JavaScript (ES6+)</div>
              </div>
          </div>

          <!-- Tools -->
          <div>
              <h3 class="text-xs font-bold text-gray-500 uppercase mb-4 tracking-widest flex items-center gap-2">
                  <Container class="w-4 h-4"/> DevOps & Tools
              </h3>
              <div class="grid grid-cols-2 md:grid-cols-3 gap-3">
                  <div class="tech-item"><Container class="text-blue-300"/> Docker</div>
                  <div class="tech-item"><GitBranch class="text-orange-400"/> Git / GitHub</div>
                  <div class="tech-item"><Users class="text-blue-500"/> Scrum / Agile</div>
              </div>
          </div>

      </div>
    </div>
  </div>
</template>

<style scoped>
/* Estilo local para los items de tecnología */
.tech-item {
    @apply p-4 bg-white/5 rounded-xl flex items-center gap-3 border border-white/5 hover:bg-white/10 transition-colors text-sm font-medium text-gray-200 cursor-default;
}
.tech-item svg {
    @apply w-5 h-5;
}
</style>