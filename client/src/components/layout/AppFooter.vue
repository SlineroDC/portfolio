<script setup>
import { Database } from 'lucide-vue-next';

const API_URL = import.meta.env.VITE_API_URL;
</script>

<template>
  <footer
    class="w-full border-t border-white/5 bg-black/40 backdrop-blur-sm py-8 mt-auto z-10 relative"
  >
    <div
      class="max-w-6xl mx-auto px-6 flex flex-col md:flex-row justify-between items-center gap-4 text-xs text-gray-500 font-medium"
    >
      <p>&copy; {{ new Date().getFullYear() }} SlineroDC.</p>

      <div class="flex items-center gap-4 md:gap-6">

        <span class="flex items-center gap-2">
          <span
            class="w-1.5 h-1.5 rounded-full bg-emerald-500 shadow-[0_0_8px_rgba(16,185,129,0.6)]"
          ></span>
          Barranquilla, CO
        </span>

        <span class="hidden md:inline text-gray-800">•</span>

        <span class="opacity-70 cursor-default">
          Vue 3 + .NET 10
        </span>

        <span class="hidden md:inline text-gray-800">•</span>

        <a
          :href="`${API_URL}/swagger`"
          target="_blank"
          class="flex items-center gap-1.5 opacity-70 hover:opacity-100 hover:text-emerald-400 transition-all cursor-pointer group"
          title="Ver documentación Swagger del Backend"
        >
          <Database class="w-3 h-3 group-hover:scale-110 transition-transform" />
          <span>API Docs</span>
        </a>

      </div>
    </div>
  </footer>
</template>
