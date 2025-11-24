
<template>

  <footer
    class="w-full border-t border-white/5 bg-black/40 backdrop-blur-sm py-8 mt-auto z-10 relative"
  >
    <div
      class="max-w-6xl mx-auto px-6 flex flex-col md:flex-row justify-between items-center gap-4 text-xs text-gray-500 font-medium"
    >
      <p>&copy; {{ new Date().getFullYear() }} SlineroDC. Design by You.</p>

      <div class="flex items-center gap-6">
        <span class="flex items-center gap-2">
          <span
            class="w-1.5 h-1.5 rounded-full bg-emerald-500 shadow-[0_0_8px_rgba(16,185,129,0.6)]"
          ></span>
          Barranquilla, CO
        </span>
        <span class="hidden md:inline text-gray-700">•</span>
        <span class="opacity-70 hover:opacity-100 transition-opacity cursor-default">
          Vue 3 + Tailwind
        </span>
      </div>
    </div>
  </footer>
</template>
