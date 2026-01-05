<script setup>
import { ref, onMounted, computed } from 'vue';
import {
  Loader2, CheckCircle2, BookOpen, Trophy, Database
} from 'lucide-vue-next';

const API_URL = import.meta.env.VITE_API_URL;
const rawItems = ref([]);
const isLoading = ref(true);
const error = ref(false);

// Configuración Visual de Columnas
const columnsConfig = {
  backlog: { title: 'Futuro / Backlog', icon: BookOpen, color: 'text-pink-400' },
  doing:   { title: 'En Construcción', icon: Loader2, color: 'text-amber-400', spin: true },
  done:    { title: 'Desplegado', icon: CheckCircle2, color: 'text-emerald-400' }
};

// 1. Fetch API .NET
const fetchRoadmap = async () => {
  try {
    const res = await fetch(`${API_URL}/api/roadmap`);
    if (!res.ok) throw new Error('Error API');
    rawItems.value = await res.json();
  } catch (e) {
    console.error(e);
    error.value = true;
  } finally {
    isLoading.value = false;
  }
};

// 2. Clasificar items en columnas
const board = computed(() => ({
  backlog: rawItems.value.filter(i => i.status === 'backlog'),
  doing:   rawItems.value.filter(i => i.status === 'doing'),
  done:    rawItems.value.filter(i => i.status === 'done'),
}));

// Colores de prioridad
const getPriorityStyle = (p) => {
  const styles = {
    'High': 'bg-red-500/10 text-red-400 border-red-500/20',
    'Medium': 'bg-blue-500/10 text-blue-400 border-blue-500/20',
    'Low': 'bg-gray-500/10 text-gray-400 border-gray-500/20',
    'Done': 'bg-emerald-500/10 text-emerald-400 border-emerald-500/20'
  };
  return styles[p] || styles['Low'];
};

onMounted(() => fetchRoadmap());
</script>

<template>
  <div class="space-y-12">

    <div class="text-center space-y-4" v-motion-slide-visible-once>
      <div class="inline-flex items-center gap-2 px-3 py-1 rounded-full bg-white/5 border border-white/10 text-xs font-mono text-purple-400 mb-2">
        <Trophy class="w-3 h-3" />
        <span>Roadmap en Vivo</span>
      </div>
      <h2 class="text-3xl md:text-5xl font-bold text-white">Mi Tablero</h2>
      <p class="text-gray-400 text-lg font-light">
        Sincronizado en tiempo real con mi base de datos.
      </p>
    </div>

    <div v-if="isLoading" class="flex justify-center py-20">
      <Loader2 class="w-10 h-10 text-emerald-500 animate-spin" />
    </div>

    <div v-else-if="error" class="flex flex-col items-center py-10 text-gray-500 gap-2">
      <Database class="w-10 h-10 opacity-50" />
      <p>Conectando con el servidor...</p>
    </div>

    <div v-else class="grid md:grid-cols-3 gap-6">
      <div
        v-for="(config, key) in columnsConfig"
        :key="key"
        class="rounded-3xl bg-[#0a0a0a] border border-white/5 p-5 flex flex-col h-full"
        v-motion-slide-visible-once
      >
        <div class="flex items-center gap-3 pb-4 border-b border-white/5 mb-4">
          <component :is="config.icon" class="w-5 h-5" :class="[config.color, config.spin && 'animate-spin']" />
          <h3 class="font-bold text-gray-200">{{ config.title }}</h3>
          <span class="ml-auto text-xs bg-white/5 px-2 py-1 rounded text-gray-500">{{ board[key]?.length || 0 }}</span>
        </div>

        <div class="space-y-3 flex-grow">
          <div
            v-for="item in board[key]" :key="item.id"
            class="p-4 rounded-xl bg-white/5 border border-white/5 hover:border-white/20 transition-all group relative overflow-hidden"
          >
             <div class="absolute left-0 top-0 bottom-0 w-1 bg-gradient-to-b from-transparent via-white/20 to-transparent"></div>

             <div class="flex justify-between items-start mb-2">
                <span class="text-[10px] font-bold uppercase px-2 py-0.5 rounded border" :class="getPriorityStyle(item.priority)">
                  {{ item.priority }}
                </span>
             </div>
             <h4 class="font-medium text-gray-200 text-sm">{{ item.title }}</h4>
             <p class="text-xs text-gray-500 mt-1">#{{ item.tag }}</p>
          </div>

          <div v-if="!board[key]?.length" class="text-center py-8 text-xs text-gray-700 italic border border-dashed border-white/5 rounded-xl">
            Vacío
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
