<script setup>
import { ref, onMounted, onUnmounted } from 'vue';

// --- IMPORTACIÓN DE COMPONENTES ---
import NavBar from './components/layout/NavBar.vue';
import AppFooter from './components/layout/AppFooter.vue';
import BentoGrid from './components/bento/BentoGrid.vue';

// Modales (Ventanas emergentes)
import ProjectModal from './components/modals/ProjectModal.vue';
import StackModal from './components/modals/StackModal.vue';
import ContactModal from './components/modals/ContactModal.vue'; 
import BioModal from './components/modals/BioModal.vue';

// --- ESTADO REACTIVO ---
// Variables para controlar qué modal está abierto
const showProjects = ref(false);
const showStack = ref(false);
const showContact = ref(false); 
const showBio = ref(false); 

// Estado para el widget de "Live Status"
const apiStatus = ref({ activity: "Offline", message: "Connecting..." });
let intervalId = null;

// --- LÓGICA DE SIMULACIÓN API ---
// Cambia el estado aleatoriamente cada 5 segundos
const fetchStatus = () => {
  const states = [
      { activity: "Coding C#", message: "Refactoring API Middleware" },
      { activity: "F1 Sim", message: "Sector 3 Purple Time" },
      { activity: "Debugging", message: "Fixing Async Bug in Node" },
      { activity: "Deploying", message: "Pushing to Docker Hub" },
      { activity: "Deep Work", message: "Do Not Disturb" }
  ];
  const random = states[Math.floor(Math.random() * states.length)];
  apiStatus.value = random;
};

// --- CICLO DE VIDA ---
onMounted(() => {
  fetchStatus(); 
  intervalId = setInterval(fetchStatus, 5000);
});

onUnmounted(() => {
  if (intervalId) clearInterval(intervalId);
});
</script>

<template>
  <div class="min-h-screen flex flex-col font-sans text-gray-100 selection:bg-emerald-500/30 selection:text-white">
    
    <!-- Navegación Superior -->
    <NavBar @open-contact="showContact = true"/>
    
    <!-- Contenido Principal -->
    <main class="flex-grow w-full max-w-6xl mx-auto px-4 md:px-6 pt-32 pb-20">
      <!-- BentoGrid: Escucha los eventos para abrir los modales -->
      <BentoGrid 
        :apiStatus="apiStatus"
        @open-projects="showProjects = true"
        @open-stack="showStack = true"
        @open-contact="showContact = true" 
        @open-bio="showBio = true"
      />
    </main>

    <!-- Pie de Página -->
    <AppFooter />

    <!-- --- SECCIÓN DE MODALES (Teleportados) --- -->
    
    <!-- Modal de Proyectos -->
    <Teleport to="body">
      <Transition name="fade">
        <ProjectModal v-if="showProjects" @close="showProjects = false" />
      </Transition>
    </Teleport>

    <!-- Modal de Tech Stack -->
    <Teleport to="body">
      <Transition name="fade">
        <StackModal v-if="showStack" @close="showStack = false" />
      </Transition>
    </Teleport>

    <!-- Modal de Contacto (Nuevo) -->
    <Teleport to="body">
      <Transition name="fade">
        <ContactModal v-if="showContact" @close="showContact = false" />
      </Transition>
    </Teleport>

    <!-- Modal de Biografía Extendida (Nuevo) -->
    <Teleport to="body">
      <Transition name="fade">
        <BioModal v-if="showBio" @close="showBio = false" />
      </Transition>
    </Teleport>

  </div>
</template>

<style>
/* Animación suave (Fade In/Out) para todos los modales */
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.3s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
</style>