<script setup>
import { ref, onMounted, onUnmounted } from 'vue';

// --- IMPORTACIÓN DE COMPONENTES ---
import NavBar from './components/layout/NavBar.vue';
import AppFooter from './components/layout/AppFooter.vue';
import BentoGrid from './components/bento/BentoGrid.vue';

// Modales
import ProjectModal from './components/modals/ProjectModal.vue';
import StackModal from './components/modals/StackModal.vue';
import ContactModal from './components/modals/ContactModal.vue'; 
import BioModal from './components/modals/BioModal.vue';

// --- VARIABLES DE ENTORNO ---
// Vite detecta automáticamente si estás en local o prod según el archivo .env
const API_URL = import.meta.env.VITE_API_URL;

// --- ESTADO REACTIVO ---
const showProjects = ref(false);
const showStack = ref(false);
const showContact = ref(false); 
const showBio = ref(false); 

const apiStatus = ref({ 
  activity: "Cargando...", 
  message: "Conectando...", 
  color: "gray", 
  icon: "loader" 
});

const techStack = ref([]);
let intervalId = null;

// --- LÓGICA DE API (DINÁMICA) ---

// 1. Obtener Live Status
const fetchStatus = async () => {
  try {
    // Usamos la variable de entorno aquí
    const response = await fetch(`${API_URL}/api/status`);

    if (response.ok) {
      const data = await response.json();
      apiStatus.value = {
        activity: data.title,       
        message: data.description,  
        color: data.color,          
        icon: data.icon,            
        isManual: data.isManual     
      };
    }
  } catch (error) {
    console.error("Error API Status:", error);
    apiStatus.value = { 
      activity: "Offline", 
      message: "API no disponible", 
      color: "gray",
      icon: "wifi-off"
    };
  }
};

// 2. Obtener Stack
const fetchStack = async () => {
  try {
    // Usamos la variable de entorno aquí también
    const res = await fetch(`${API_URL}/api/stack`);
    if(res.ok) {
      techStack.value = await res.json();
    }
  } catch (e) {
    console.error("Error cargando stack", e);
  }
};

// --- CICLO DE VIDA ---
onMounted(() => {
  // Verificación rápida en consola para que sepas a dónde está apuntando
  console.log("Conectando a API en:", API_URL);
  
  fetchStatus(); 
  fetchStack(); 
  intervalId = setInterval(fetchStatus, 60000); 
});

onUnmounted(() => {
  if (intervalId) clearInterval(intervalId);
});
</script>

<template>
  <div class="min-h-screen flex flex-col font-sans text-gray-100 selection:bg-emerald-500/30 selection:text-white">
    
    <NavBar @open-contact="showContact = true"/>
    
    <main class="flex-grow w-full max-w-6xl mx-auto px-4 md:px-6 pt-32 pb-20">
      <BentoGrid 
        :apiStatus="apiStatus"
        :techStack="techStack" 
        @open-projects="showProjects = true"
        @open-stack="showStack = true"
        @open-contact="showContact = true" 
        @open-bio="showBio = true"
      />
    </main>

    <AppFooter />

    <Teleport to="body">
      <Transition name="fade"><ProjectModal v-if="showProjects" @close="showProjects = false" /></Transition>
    </Teleport>
    
    <Teleport to="body">
      <Transition name="fade">
        <StackModal v-if="showStack" :techStack="techStack" @close="showStack = false" />
      </Transition>
    </Teleport>

    <Teleport to="body">
      <Transition name="fade"><ContactModal v-if="showContact" @close="showContact = false" /></Transition>
    </Teleport>
    
    <Teleport to="body">
      <Transition name="fade"><BioModal v-if="showBio" @close="showBio = false" /></Transition>
    </Teleport>

  </div>
</template>

<style>
.fade-enter-active, .fade-leave-active { transition: opacity 0.3s ease; }
.fade-enter-from, .fade-leave-to { opacity: 0; }
</style>