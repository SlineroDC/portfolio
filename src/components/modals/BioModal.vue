<script setup>
import { X, TrendingUp, Search, Briefcase } from 'lucide-vue-next';
defineEmits(['close']);
</script>

<template>
  <div class="fixed inset-0 z-[100] flex items-center justify-center p-4 liquid-backdrop" @click="$emit('close')">
    <div class="bg-[#0a0a0a]/95 backdrop-blur-3xl w-full max-w-2xl rounded-[2rem] shadow-2xl p-8 border border-white/10 relative overflow-hidden" @click.stop>
      
      <button @click="$emit('close')" class="absolute top-6 right-6 w-10 h-10 rounded-full bg-white/5 flex items-center justify-center hover:bg-white/10 transition-colors text-white">
        <X class="w-5 h-5" />
      </button>

      <div class="space-y-6">
          <!-- Header -->
          <div class="border-b border-white/5 pb-6">
              <span class="text-emerald-500 font-bold tracking-widest text-xs uppercase mb-2 block">Mi Trayectoria</span>
              <h2 class="text-3xl font-bold text-white leading-tight">
                  La lógica contable aplicada a la <br> 
                  <span class="text-transparent bg-clip-text bg-gradient-to-r from-emerald-400 to-cyan-500">Arquitectura de Software.</span>
              </h2>
          </div>

          <!-- Contenido -->
          <div class="space-y-6 text-gray-300 leading-relaxed font-light">
              <p>
                  <strong class="text-white">Durante años, optimicé cadenas de suministro y balances contables.</strong> 
                  Aprendí que un pequeño error en un proceso puede costar millones, y que la eficiencia no es una opción, es una necesidad.
              </p>
              
              <div class="grid grid-cols-1 md:grid-cols-2 gap-4 my-6">
                  <div class="bg-white/5 p-4 rounded-xl border border-white/5">
                      <Briefcase class="w-6 h-6 text-blue-400 mb-2"/>
                      <h4 class="text-white font-bold text-sm">Logística</h4>
                      <p class="text-xs text-gray-500 mt-1">Gestión de recursos y optimización de flujos de trabajo complejos.</p>
                  </div>
                  <div class="bg-white/5 p-4 rounded-xl border border-white/5">
                      <Search class="w-6 h-6 text-emerald-400 mb-2"/>
                      <h4 class="text-white font-bold text-sm">Contabilidad</h4>
                      <p class="text-xs text-gray-500 mt-1">Atención meticulosa al detalle y rastreo de discrepancias (bugs).</p>
                  </div>
              </div>

              <p>
                  Hoy, traslado esa mentalidad al código. No solo escribo funciones; diseño sistemas que cuadran. 
                  Como <strong class="text-white">Scrum Master</strong> y Desarrollador, mi objetivo es que cada sprint entregue valor real y cada línea de código tenga un propósito claro.
              </p>

              <div class="flex items-center gap-2 text-emerald-400 font-medium pt-2">
                  <TrendingUp class="w-5 h-5" />
                  <span>Enfocado en resultados medibles y escalabilidad.</span>
              </div>
          </div>
      </div>

    </div>
  </div>
</template>