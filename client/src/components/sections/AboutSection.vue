<script setup>
import { GraduationCap, Briefcase, Code2, Award } from 'lucide-vue-next';

// Datos de tu historia
const timeline = [
  {
    year: "2024 - Presente",
    title: "Full Stack Developer Jr.",
    company: "Freelance / Proyectos Propios",
    desc: "Desarrollo de arquitecturas escalables con .NET 10 y Vue 3. Creación de soluciones SaaS y optimización de bases de datos.",
    icon: Code2,
    color: "text-emerald-400",
    bg: "bg-emerald-500/10 border-emerald-500/20"
  },
  {
    year: "2023",
    title: "Transición Tecnológica",
    company: "Riwi Io",
    desc: "Decidí pivotar mi carrera al Desarrollo de Software con Riwi. Aprendizaje profundo de C#, Vue js, Express js, SQL y Arquitectura de Software. Construcción de proyectos MVP como 'Connect BQ'.",
    icon: GraduationCap,
    color: "text-blue-400",
    bg: "bg-blue-500/10 border-blue-500/20"
  },
  {
    year: "2020 - 2023",
    title: "Auxiliar Contable",
    company: "Soluciones Ambientales de Colombia S.A.S",
    desc: "Gestión de inventarios, ordenes de compra, nomina y análisis de costos. Aquí nació mi obsesión por la eficiencia y la automatización de procesos.",
    icon: Briefcase,
    color: "text-purple-400",
    bg: "bg-purple-500/10 border-purple-500/20"
  }
];
</script>

<template>
  <div class="grid md:grid-cols-2 gap-x-16 gap-y-12 items-start">

    <div class="space-y-8 md:sticky md:top-32" v-motion-slide-visible-once>
      <div class="inline-flex items-center gap-2 px-3 py-1 rounded-full bg-white/5 border border-white/10 text-xs font-mono text-emerald-400">
        <Award class="w-3 h-3" />
        <span>Mi Historia</span>
      </div>

      <h2 class="text-4xl md:text-5xl font-bold text-white leading-tight">
        Más que código, <br />
        <span class="text-gray-500">construyo soluciones.</span>
      </h2>

      <div class="space-y-6 text-lg text-gray-400 font-light leading-relaxed">
        <p>
          Mi viaje no empezó en una terminal, sino en almacenes y hojas de cálculo. Durante años, balances contables y procesos de nómina.
        </p>
        <p>
          Un día me di cuenta de que las herramientas que usábamos podían ser mejores. <strong class="text-white font-medium">Así que decidí construirlas yo mismo.</strong>
        </p>
        <p>
          Hoy, combino esa mentalidad analítica y orientada al negocio con la potencia del desarrollo Full Stack. No solo escribo código; entiendo el problema de negocio que hay detrás.
        </p>
      </div>

      <div class="grid grid-cols-3 gap-4 pt-4">
        <div class="p-4 rounded-2xl bg-white/5 border border-white/10 text-center">
            <div class="text-2xl font-bold text-white">+3</div>
            <div class="text-xs text-gray-500 uppercase">Años Exp. Laboral </div>
        </div>
        <div class="p-4 rounded-2xl bg-white/5 border border-white/10 text-center">
            <div class="text-2xl font-bold text-emerald-400">100%</div>
            <div class="text-xs text-gray-500 uppercase">Compromiso</div>
        </div>
        <div class="p-4 rounded-2xl bg-white/5 border border-white/10 text-center">
            <div class="text-2xl font-bold text-blue-400">Full</div>
            <div class="text-xs text-gray-500 uppercase">Stack DotNet</div>
        </div>
      </div>
    </div>

    <div class="relative space-y-8 pl-8 md:pl-0">
      <div class="absolute left-8 md:left-4 top-4 bottom-4 w-[2px] bg-gradient-to-b from-emerald-500/50 via-gray-800 to-transparent md:hidden"></div>

      <div
        v-for="(item, index) in timeline"
        :key="index"
        class="relative pl-8 md:pl-0 group"
        v-motion-slide-visible-once
        :delay="index * 200"
      >
        <div
          class="p-6 rounded-3xl border transition-all duration-300 hover:-translate-y-1 hover:shadow-2xl"
          :class="[item.bg, 'bg-opacity-50 backdrop-blur-sm border-opacity-50 hover:border-opacity-100']"
        >
          <div class="flex justify-between items-start mb-4">
            <div class="p-3 rounded-full bg-black/40 border border-white/5" :class="item.color">
              <component :is="item.icon" class="w-6 h-6" />
            </div>
            <span class="text-xs font-mono text-gray-500 py-1 px-2 rounded bg-black/20">{{ item.year }}</span>
          </div>

          <h3 class="text-xl font-bold text-white mb-1">{{ item.title }}</h3>
          <p class="text-sm font-medium text-gray-400 mb-3">{{ item.company }}</p>
          <p class="text-sm text-gray-400 leading-relaxed">
            {{ item.desc }}
          </p>
        </div>
      </div>
    </div>

  </div>
</template>
