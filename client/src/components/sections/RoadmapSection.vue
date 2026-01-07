<script setup>
import { ref, onMounted, computed } from 'vue';
import { Loader2, CheckCircle2, BookOpen, Database, Cpu } from 'lucide-vue-next';

import GlassCard from '../ui/GlassCard.vue';
import StatusBadge from '../ui/StatusBadge.vue';

const API_URL = import.meta.env.VITE_API_URL;
const rawItems = ref([]);
const isLoading = ref(true);
const error = ref(false);

const columnsConfig = {
  backlog: { title: 'Futuro / Backlog', icon: BookOpen, color: 'text-pink-400' },
  doing:   { title: 'En Construcción', icon: Loader2, color: 'text-amber-400', spin: true },
  done:    { title: 'Desplegado', icon: CheckCircle2, color: 'text-emerald-400' }
};

const fetchRoadmap = async () => {
  try {
    const res = await fetch(`${API_URL}/api/roadmap`);
    if (!res.ok) throw new Error('Error API');
    rawItems.value = await res.json();
  } catch (e) { console.error(e); error.value = true; }
  finally { isLoading.value = false; }
};

const board = computed(() => ({
  backlog: rawItems.value.filter(i => i.status === 'backlog'),
  doing:   rawItems.value.filter(i => i.status === 'doing'),
  done:    rawItems.value.filter(i => i.status === 'done'),
}));

// Helper para mapear "High/Medium" a colores del componente StatusBadge
const getBadgeColor = (priority) => {
  const map = { 'High': 'red', 'Medium': 'blue', 'Low': 'gray', 'Done': 'emerald' };
  return map[priority] || 'gray';
};

onMounted(() => fetchRoadmap());
</script>

<template>
  <div class="space-y-12">

    <div class="text-center space-y-4" v-motion-slide-visible-once>
      <StatusBadge color="purple" :pulse="true">Transparencia Radical</StatusBadge>

      <h2 class="text-3xl md:text-5xl font-bold text-white tracking-tight">
        Mi Tablero <span class="text-transparent bg-clip-text bg-gradient-to-r from-purple-400 to-emerald-400">Kanban</span>
      </h2>
      <p class="text-gray-400 text-lg font-light">
        Datos en tiempo real desde SQLite.
      </p>
    </div>

    <div v-if="isLoading" class="flex justify-center py-20">
      <Loader2 class="w-10 h-10 text-emerald-500 animate-spin" />
    </div>

    <div v-else-if="error" class="flex flex-col items-center py-10 text-gray-500">
      <Database class="w-10 h-10 mb-2 opacity-50" />
      <p>Esperando conexión...</p>
    </div>

    <div v-else class="grid md:grid-cols-3 gap-6">

      <GlassCard
        v-for="(config, key) in columnsConfig"
        :key="key"
        class="p-6 h-full flex flex-col"
        v-motion-slide-visible-once
      >
        <div class="flex items-center gap-3 pb-4 border-b border-white/5 mb-4">
          <component :is="config.icon" class="w-5 h-5" :class="[config.color, config.spin && 'animate-spin']" />
          <h3 class="font-bold text-gray-200">{{ config.title }}</h3>
          <span class="ml-auto text-xs font-mono text-gray-500">{{ board[key]?.length || 0 }}</span>
        </div>

        <div class="space-y-3 flex-grow">
          <div
            v-for="item in board[key]" :key="item.id"
            class="p-4 rounded-xl bg-white/5 border border-white/5 hover:bg-white/10 transition-colors group/card"
          >
            <div class="flex justify-between items-start mb-3">
               <StatusBadge :color="getBadgeColor(item.priority)">
                 {{ item.priority }}
               </StatusBadge>

               <div class="flex items-center gap-2 opacity-50 group-hover/card:opacity-100 transition-opacity">
                 <span class="text-xs font-mono text-gray-500 font-bold">#{{ item.id }}</span>
                 <Cpu class="w-4 h-4 text-gray-600 group-hover/card:text-white transition-colors" />
               </div>
            </div>

            <h4 class="font-medium text-gray-200 text-sm">{{ item.title }}</h4>
            <p class="text-xs text-gray-500 mt-1 font-mono">#{{ item.tag }}</p>
          </div>

          <div v-if="!board[key]?.length" class="text-center py-8 text-xs text-gray-700 italic border border-dashed border-white/5 rounded-xl">
             Vacío
          </div>
        </div>

      </GlassCard>

    </div>
  </div>
</template>
