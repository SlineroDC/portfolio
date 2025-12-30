<script setup>
import { computed } from 'vue';

const props = defineProps({
  techStack: { type: Array, default: () => [] }
});

const getIconPath = (iconName) => `/icons/${iconName}.svg`;

const defaultStack = [
  { name: "Vue.js", icon: "vue" },
  { name: ".NET", icon: "dotnet" },
  { name: "C#", icon: "csharp" },
  { name: "MySQL", icon: "mysql" },
  { name: "Docker", icon: "docker" },
  { name: "Tailwind", icon: "tailwind" },
  { name: "Git", icon: "git" },
  { name: "Node.js", icon: "node" },
  { name: "Python", icon: "python" },
  { name: "MongoDB", icon: "mongo" }
];

const displayStack = computed(() => {
  return props.techStack.length > 0 ? props.techStack : defaultStack;
});
</script>

<template>
  <div class="max-w-6xl mx-auto my-10 bg-white/5 backdrop-blur-sm border border-white/10 rounded-3xl overflow-hidden relative">

    <div class="py-8 relative flex overflow-x-hidden group">

      <div class="absolute inset-y-0 left-0 w-32 bg-gradient-to-r from-[#050505] via-[#050505]/80 to-transparent z-10"></div>
      <div class="absolute inset-y-0 right-0 w-32 bg-gradient-to-l from-[#050505] via-[#050505]/80 to-transparent z-10"></div>

      <div class="animate-marquee whitespace-nowrap flex items-center">
        <div
          v-for="(tech, i) in displayStack"
          :key="i"
          class="mx-8 flex flex-col items-center gap-4 group/item cursor-default"
        >
          <div class="w-12 h-12 relative flex items-center justify-center transition-transform duration-300 group-hover/item:scale-110">
              <img
                :src="getIconPath(tech.icon)"
                :alt="tech.name"
                class="w-full h-full object-contain drop-shadow-lg filter grayscale opacity-50 group-hover/item:grayscale-0 group-hover/item:opacity-100 transition-all duration-300"
              />
          </div>

          <span class="text-[10px] font-bold text-gray-500 uppercase tracking-widest group-hover/item:text-white transition-colors">
              {{ tech.name }}
          </span>
        </div>
      </div>

      <div class="absolute top-0 py-8 animate-marquee2 whitespace-nowrap flex items-center">
        <div
          v-for="(tech, i) in displayStack"
          :key="i + 'dup'"
          class="mx-8 flex flex-col items-center gap-4 group/item cursor-default"
        >
           <div class="w-12 h-12 relative flex items-center justify-center transition-transform duration-300 group-hover/item:scale-110">
              <img
                :src="getIconPath(tech.icon)"
                :alt="tech.name"
                class="w-full h-full object-contain drop-shadow-lg filter grayscale opacity-50 group-hover/item:grayscale-0 group-hover/item:opacity-100 transition-all duration-300"
              />
          </div>

          <span class="text-[10px] font-bold text-gray-500 uppercase tracking-widest group-hover/item:text-white transition-colors">
              {{ tech.name }}
          </span>
        </div>
      </div>

    </div>
  </div>
</template>

<style scoped>
.animate-marquee { animation: marquee 40s linear infinite; }
.animate-marquee2 { animation: marquee2 40s linear infinite; }

.group:hover .animate-marquee,
.group:hover .animate-marquee2 {
    animation-play-state: paused;
}

@keyframes marquee {
  0% { transform: translateX(0%); }
  100% { transform: translateX(-100%); }
}
@keyframes marquee2 {
  0% { transform: translateX(100%); }
  100% { transform: translateX(0%); }
}
</style>
