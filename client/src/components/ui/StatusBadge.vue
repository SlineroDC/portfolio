<script setup>
defineProps({
  color: {
    type: String,
    default: 'emerald' // 'emerald', 'blue', 'purple', 'red', 'amber'
  },
  pulse: {
    type: Boolean,
    default: false
  }
});

// Mapa de colores para evitar problemas con Tailwind
const colorClasses = {
  emerald: 'text-emerald-400 border-emerald-500/20',
  blue: 'text-blue-400 border-blue-500/20',
  purple: 'text-purple-400 border-purple-500/20',
  red: 'text-red-400 border-red-500/20',
  amber: 'text-amber-400 border-amber-500/20',
  gray: 'text-gray-400 border-white/10'
};

const bgClasses = {
  emerald: 'bg-emerald-500',
  blue: 'bg-blue-500',
  purple: 'bg-purple-500',
  red: 'bg-red-500',
  amber: 'bg-amber-500',
  gray: 'bg-gray-500'
};
</script>

<template>
  <div
    class="inline-flex items-center gap-2 px-3 py-1 text-[10px] font-bold tracking-widest uppercase rounded-full border bg-white/5"
    :class="colorClasses[color] || colorClasses.gray"
  >
    <span
      v-if="pulse"
      class="w-1.5 h-1.5 rounded-full animate-pulse"
      :class="bgClasses[color] || bgClasses.gray"
    ></span>
    <slot />
  </div>
</template>
