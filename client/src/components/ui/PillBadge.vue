<script setup>
defineProps({
  color: {
    type: String,
    default: 'emerald' // 'emerald', 'blue', 'purple', etc.
  },
  pulse: {
    type: Boolean,
    default: false
  }
});
</script>

<template>
  <div 
    class="inline-flex items-center gap-2 px-3 py-1 text-[10px] font-bold tracking-widest uppercase rounded-full border bg-white/5"
    :class="{
      'text-emerald-400 border-emerald-500/20': color === 'emerald',
      'text-blue-400 border-blue-500/20': color === 'blue',
      'text-purple-400 border-purple-500/20': color === 'purple',
      'text-gray-400 border-white/10': color === 'gray'
    }"
  >
    <span v-if="pulse" class="w-1.5 h-1.5 rounded-full animate-pulse" :class="`bg-${color}-500`"></span>
    <slot />
  </div>
</template>