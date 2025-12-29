<script setup>
import { ref, watch } from 'vue';
import { Menu, X, ArrowUpRight } from 'lucide-vue-next';

// Definimos eventos
const emit = defineEmits(['navigate', 'open-contact']);

const isMobileMenuOpen = ref(false);
const isScrolled = ref(false);

const navItems = [
  { label: 'Inicio', id: '#home' },   // Agregué el # para ser explícito
  { label: 'Stack', id: '#stack' },
  { label: 'Proyectos', id: '#projects' },
  { label: 'Sobre Mí', id: '#about' },
];

const handleScroll = () => {
  isScrolled.value = window.scrollY > 50;
};
window.addEventListener('scroll', handleScroll);

// Función unificada para navegar
const handleNav = (id) => {
  emit('navigate', id);
  isMobileMenuOpen.value = false;
};

// Bloqueo de scroll
watch(isMobileMenuOpen, (val) => {
  document.body.style.overflow = val ? 'hidden' : '';
});
</script>

<template>
  <nav
    class="fixed top-0 w-full z-50 transition-all duration-500 ease-in-out border-b"
    :class="isScrolled ? 'bg-[#050505]/80 backdrop-blur-xl border-white/10 py-3' : 'bg-transparent border-transparent py-6'"
  >
    <div class="max-w-7xl mx-auto px-6 flex justify-between items-center">

      <div @click="handleNav('#home')" class="flex items-center gap-3 group cursor-pointer select-none">
        <div class="w-8 h-8 rounded-lg bg-gradient-to-br from-gray-800 to-black border border-white/10 flex items-center justify-center font-bold font-mono text-xs text-white shadow-lg group-hover:border-emerald-500/50 transition-colors">
          SL
        </div>
        <span class="font-medium tracking-tight text-gray-300 group-hover:text-white transition-colors font-sans">
          Portfolio
        </span>
      </div>

      <div class="hidden md:flex items-center gap-8">
        <button
          v-for="item in navItems"
          :key="item.id"
          @click="handleNav(item.id)"
          class="text-sm font-medium text-gray-400 hover:text-white transition-colors hover:underline decoration-emerald-500 underline-offset-4"
        >
          {{ item.label }}
        </button>

        <button
          @click="$emit('open-contact')"
          class="bg-white/10 border border-white/10 px-5 py-2 rounded-full transition-all hover:bg-white/20 hover:scale-105 flex items-center gap-2 text-sm font-medium text-white group"
        >
          <span>Contáctame</span>
          <ArrowUpRight class="w-4 h-4 text-gray-400 group-hover:text-white transition-colors" />
        </button>
      </div>

      <button
        @click="isMobileMenuOpen = !isMobileMenuOpen"
        class="md:hidden p-2 text-white z-50 relative"
      >
        <component :is="isMobileMenuOpen ? X : Menu" class="w-6 h-6" />
      </button>

    </div>
  </nav>

  <Transition name="mobile">
    <div v-if="isMobileMenuOpen" class="fixed inset-0 z-40 bg-[#050505]/95 backdrop-blur-xl flex flex-col items-center justify-center gap-8 md:hidden">

        <button
          v-for="item in navItems"
          :key="item.id"
          @click="handleNav(item.id)"
          class="text-2xl font-bold text-gray-300 hover:text-emerald-400 transition-colors"
        >
          {{ item.label }}
        </button>

        <button
          @click="{ $emit('open-contact'); isMobileMenuOpen = false; }"
          class="mt-4 px-8 py-3 rounded-full bg-emerald-500 text-black font-bold text-lg hover:scale-105 transition-transform"
        >
          Contáctame
        </button>
    </div>
  </Transition>
</template>

<style scoped>
.mobile-enter-active, .mobile-leave-active { transition: opacity 0.3s, transform 0.3s; }
.mobile-enter-from, .mobile-leave-to { opacity: 0; transform: translateY(-20px); }
</style>
