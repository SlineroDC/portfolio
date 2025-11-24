<script setup>
import { ref, onMounted, onUnmounted } from 'vue';
import { ArrowUpRight } from 'lucide-vue-next';

// DEFINIMOS EL EVENTO PARA COMUNICARNOS CON APP.VUE
defineEmits(['open-contact']);

// Lógica de scroll
const isScrolled = ref(false);
const handleScroll = () => {
  isScrolled.value = window.scrollY > 50;
};

onMounted(() => window.addEventListener('scroll', handleScroll));
onUnmounted(() => window.removeEventListener('scroll', handleScroll));
</script>

<template>
  <nav 
    class="fixed top-0 w-full z-50 transition-all duration-500 ease-in-out"
    :class="isScrolled ? 'bg-[#050505]/60 backdrop-blur-xl border-b border-white/5 py-3' : 'bg-transparent py-6'"
  >
    <div class="max-w-6xl mx-auto px-6 flex justify-between items-center">
      
      <!-- Firma / Logo -->
      <div class="flex items-center gap-3 group cursor-default select-none">
        <div class="w-8 h-8 rounded-lg bg-gradient-to-br from-gray-800 to-black border border-white/10 flex items-center justify-center font-bold font-mono text-xs text-white shadow-lg group-hover:border-emerald-500/50 transition-colors">
          SL
        </div>
        <span class="font-medium tracking-tight text-gray-300 group-hover:text-white transition-colors font-sans">
          Portfolio SL
        </span>
      </div>

      <button 
        @click="$emit('open-contact')" 
        class="bg-white/10 backdrop-blur-md border border-white/10 px-5 py-2 rounded-full transition-all duration-300 hover:bg-white/20 hover:scale-105 active:scale-95 flex items-center gap-2 text-sm font-medium text-white group cursor-pointer"
      >
        <span>Contáctame</span>
        <ArrowUpRight class="w-4 h-4 text-gray-400 group-hover:text-white transition-colors" />
      </button>

    </div>
  </nav>
</template>
```

### Paso 2: Conectar en `src/App.vue`

Ahora necesitamos decirle a `App.vue` que escuche ese clic. Solo tienes que cambiar **una línea** en tu `App.vue` (la línea del `<NavBar />`).

Busca donde dice `<NavBar />` y cámbialo por esto:

```html
<!-- En src/App.vue -->

<!-- 1. BARRA DE NAVEGACIÓN -->
<!-- Agregamos el "listener" @open-contact -->
<NavBar @open-contact="showContact = true" />