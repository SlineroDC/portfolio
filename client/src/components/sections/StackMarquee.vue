<script setup>
import { computed } from 'vue';

// Recibimos la lista de tu API (.NET devuelve "vue", "csharp", "sql", etc.)
const props = defineProps({
  techStack: { type: Array, default: () => [] }
});

// Función para obtener la ruta de la imagen en public/icons
// Ejemplo: si llega "csharp" -> devuelve "/icons/csharp.svg"
const getIconPath = (iconName) => {
  // Manejo de casos especiales por si el nombre en la API no coincide exacto con el archivo
  const fileMap = {
    'csharp': 'csharp.svg',
    'dotnet': 'dotnet.svg',
    'vue': 'vue.svg',
    'tailwind': 'tailwind.svg',
    'javascript': 'javascript.svg',
    'html': 'html.svg',
    'mongo': 'mongo.svg',
    'mysql': 'mysql.svg',
    'node': 'node.svg',
    'python': 'python.svg',
    'sql': 'sql.svg',
    'docker': 'docker.svg',
    'git': 'git.svg',
    'github': 'github.svg'
  };

  const fileName = fileMap[iconName] || 'code.svg'; // Icono por defecto si falla
  return `/icons/${fileName}`;
};

// Stack por defecto para mostrar algo si la API tarda en cargar
const displayStack = computed(() => {
  return props.techStack.length > 0 ? props.techStack : [
    { name: "Vue.js", icon: "vue" },
    { name: ".NET", icon: "dotnet" },
    { name: "C#", icon: "csharp" },
    { name: "SQL", icon: "sql" },
    { name: "Docker", icon: "docker" },
    { name: "Tailwind", icon: "tailwind" }
  ];
});
</script>

<template>
  <div class="relative flex overflow-x-hidden group select-none py-6">

    <div class="absolute inset-y-0 left-0 w-32 bg-gradient-to-r from-[#050505] to-transparent z-10"></div>
    <div class="absolute inset-y-0 right-0 w-32 bg-gradient-to-l from-[#050505] to-transparent z-10"></div>

    <div class="animate-marquee whitespace-nowrap flex items-center">
      <div
        v-for="(tech, i) in displayStack"
        :key="i"
        class="mx-12 flex flex-col items-center gap-4 group/item cursor-default"
      >
        <div class="w-16 h-16 relative flex items-center justify-center transition-transform duration-300 group-hover/item:scale-110 group-hover/item:drop-shadow-[0_0_15px_rgba(255,255,255,0.2)]">
            <img
              :src="getIconPath(tech.icon)"
              :alt="tech.name"
              class="w-full h-full object-contain filter grayscale opacity-60 group-hover/item:grayscale-0 group-hover/item:opacity-100 transition-all duration-500"
            />
        </div>

        <span class="text-sm font-bold text-gray-600 uppercase tracking-widest group-hover/item:text-white transition-colors">
            {{ tech.name }}
        </span>
      </div>
    </div>

    <div class="absolute top-0 py-6 animate-marquee2 whitespace-nowrap flex items-center">
      <div
        v-for="(tech, i) in displayStack"
        :key="i + 'dup'"
        class="mx-12 flex flex-col items-center gap-4 group/item cursor-default"
      >
         <div class="w-16 h-16 relative flex items-center justify-center transition-transform duration-300 group-hover/item:scale-110 group-hover/item:drop-shadow-[0_0_15px_rgba(255,255,255,0.2)]">
            <img
              :src="getIconPath(tech.icon)"
              :alt="tech.name"
              class="w-full h-full object-contain filter grayscale opacity-60 group-hover/item:grayscale-0 group-hover/item:opacity-100 transition-all duration-500"
            />
        </div>

        <span class="text-sm font-bold text-gray-600 uppercase tracking-widest group-hover/item:text-white transition-colors">
            {{ tech.name }}
        </span>
      </div>
    </div>

  </div>
</template>

<style scoped>
/* Duración ajustada a 40s para que sea un paseo suave */
.animate-marquee { animation: marquee 40s linear infinite; }
.animate-marquee2 { animation: marquee2 40s linear infinite; }

@keyframes marquee {
  0% { transform: translateX(0%); }
  100% { transform: translateX(-100%); }
}
@keyframes marquee2 {
  0% { transform: translateX(100%); }
  100% { transform: translateX(0%); }
}
</style>
