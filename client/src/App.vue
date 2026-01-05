<script setup>
import { ref, onMounted, onUnmounted } from 'vue';
import Lenis from 'lenis';
import { ArrowUp } from 'lucide-vue-next';

// --- 1. IMPORTACIÓN DE COMPONENTES DE LAYOUT ---
import NavBar from './components/layout/NavBar.vue';
import AppFooter from './components/layout/AppFooter.vue';

// --- 2. IMPORTACIÓN DE SECCIONES ---
import HeroBento from './components/sections/HeroBento.vue';
import StackMarquee from './components/sections/StackMarquee.vue';
import ProjectsSection from './components/sections/ProjectsSection.vue';
import AboutSection from './components/sections/AboutSection.vue';
import ContactSection from './components/sections/ContactSection.vue';
import RoadmapSection from './components/sections/RoadmapSection.vue'; 

// --- VARIABLES Y ESTADOS ---
const API_URL = import.meta.env.VITE_API_URL;
const showBackToTop = ref(false);
const isLoading = ref(true);
const apiStatus = ref({ activity: "Cargando...", message: "Conectando...", color: "gray", icon: "loader" });
const techStack = ref([]);

let intervalId = null;
let lenis = null;

// --- FUNCIONES API (Backend communication) ---
const fetchStatus = async () => {
  try {
    const response = await fetch(`${API_URL}/api/status`);
    if (response.ok) {
      const data = await response.json();
      apiStatus.value = { activity: data.title, message: data.description, color: data.color, icon: data.icon };
    }
  } catch (error) { console.error("Error API Status", error); }
};

const fetchStack = async () => {
  try {
    const res = await fetch(`${API_URL}/api/stack`);
    if(res.ok) techStack.value = await res.json();
  } catch (e) { console.error("Error API Stack", e); }
};

// --- CONFIGURACIÓN DE SCROLL SUAVE (LENIS) ---
const scrollToTop = () => lenis?.scrollTo(0, { duration: 2 });
const scrollToSection = (id) => lenis?.scrollTo(id, { offset: -80, duration: 1.5 });

// --- CICLO DE VIDA (LIFECYCLE) ---
onMounted(() => {
  // 1. Simulación de carga inicial (Intro): 2 segundos
  setTimeout(() => {
    isLoading.value = false;
  }, 2000);

  // 2. Ejecución de llamadas a la API
  fetchStatus();
  fetchStack();
  intervalId = setInterval(fetchStatus, 60000);

  // 3. Inicialización de Lenis (Scroll suave)
  lenis = new Lenis({ duration: 1.2, smoothWheel: true });
  function raf(time) {
    lenis.raf(time);
    requestAnimationFrame(raf);
  }
  requestAnimationFrame(raf);

  // 4. Listener para mostrar/ocultar botón "Volver arriba"
  window.addEventListener('scroll', () => {
    showBackToTop.value = window.scrollY > 500;
  });
});

onUnmounted(() => {
  if (intervalId) clearInterval(intervalId);
  if (lenis) lenis.destroy();
});
</script>

<template>
  <div class="min-h-screen font-sans text-gray-100 bg-[#050505] selection:bg-emerald-500/30 selection:text-white">

    <Transition name="intro-fade">
      <div
        v-if="isLoading"
        class="fixed inset-0 z-[100] bg-black flex items-center justify-center"
      >
        <div class="relative">
          <div class="w-24 h-24 rounded-2xl bg-gradient-to-br from-emerald-900 to-black border border-emerald-500/30 flex items-center justify-center animate-pulse">
             <span class="text-4xl font-bold font-mono text-emerald-400 tracking-tighter">SL</span>
          </div>
          <div class="absolute inset-0 bg-emerald-500/20 blur-xl rounded-full -z-10 animate-ping"></div>
        </div>
      </div>
    </Transition>

    <NavBar
      @navigate="scrollToSection"
      @open-contact="scrollToSection('#contact')"
    />

    <main class="relative z-0">

      <section id="home" class="pt-32 pb-20 px-4 max-w-7xl mx-auto min-h-screen flex flex-col justify-center">
        <HeroBento :apiStatus="apiStatus" @open-bio="scrollToSection('#about')" />
      </section>

      <section id="stack" class="py-4 border-y border-white/5 bg-black/20 backdrop-blur-md overflow-hidden">
        <StackMarquee :techStack="techStack" />
      </section>

      <section id="projects" class="py-32 px-4 max-w-6xl mx-auto">
        <ProjectsSection />
      </section>

      <section id="about" class="py-32 px-4 max-w-4xl mx-auto">
        <AboutSection />
      </section>

      <section id="roadmap" class="py-32 px-4 max-w-7xl mx-auto">
        <RoadmapSection />
      </section>

      <section id="contact" class="py-32 px-4 max-w-5xl mx-auto">
        <ContactSection />
      </section>

    </main>

    <AppFooter />

    <ToasterComponent
      position="top-center"
      richColors
      :toastOptions="{
        style: { zIndex: 9999 }
      }"
    />

    <Transition name="fade">
      <button
        v-if="showBackToTop"
        @click="scrollToTop"
        class="fixed bottom-8 left-1/2 -translate-x-1/2 z-40 p-3 rounded-full bg-black/50 hover:bg-emerald-500 text-emerald-500 hover:text-white border border-emerald-500/20 backdrop-blur-md transition-all shadow-lg group active:scale-95"
      >
        <ArrowUp class="w-6 h-6 group-hover:-translate-y-1 transition-transform" />
      </button>
    </Transition>

  </div>
</template>

<style>
/* --- CONFIGURACIÓN GLOBAL CSS --- */

/* Lenis Scroll Smooth */
html.lenis { height: auto; }
.lenis.lenis-smooth { scroll-behavior: auto !important; }
.lenis.lenis-smooth [data-lenis-prevent] { overscroll-behavior: contain; }
.lenis.lenis-stopped { overflow: hidden; }

/* Transiciones Genéricas (Fade In/Out estándar) */
.fade-enter-active, .fade-leave-active { transition: opacity 0.3s ease; }
.fade-enter-from, .fade-leave-to { opacity: 0; }

/* Transición Específica para la Intro */
.intro-fade-leave-active { transition: opacity 1.5s ease-in-out; }
.intro-fade-leave-to { opacity: 0; }
</style>
