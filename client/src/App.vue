<script setup>
import { ref, onMounted, onUnmounted } from 'vue';
import Lenis from 'lenis';
import { ArrowUp } from 'lucide-vue-next';

// --- LAYOUT ---
import NavBar from './components/layout/NavBar.vue';
import AppFooter from './components/layout/AppFooter.vue';

// --- SECCIONES (SCROLLING) ---
import HeroBento from './components/bento/HeroBento.vue';
import StackMarquee from './components/sections/StackMarquee.vue';
import ProjectsSection from './components/sections/ProjectsSection.vue';
import AboutSection from './components/sections/AboutSection.vue';
import ContactSection from './components/sections/ContactSection.vue';

// Variables
const API_URL = import.meta.env.VITE_API_URL;

// --- ESTADO ---
const showBackToTop = ref(false);
const apiStatus = ref({ activity: "Cargando...", message: "Conectando...", color: "gray", icon: "loader" });
const techStack = ref([]);

let intervalId = null;
let lenis = null;

// --- API ---
const fetchStatus = async () => {
  try {
    const response = await fetch(`${API_URL}/api/status`);
    if (response.ok) {
      const data = await response.json();
      apiStatus.value = { activity: data.title, message: data.description, color: data.color, icon: data.icon };
    }
  } catch (error) { console.error("Error API", error); }
};

const fetchStack = async () => {
  try {
    const res = await fetch(`${API_URL}/api/stack`);
    if(res.ok) techStack.value = await res.json();
  } catch (e) { console.error("Error Stack", e); }
};

// --- SCROLL LOGIC ---
const scrollToTop = () => lenis?.scrollTo(0, { duration: 2 });
const scrollToSection = (id) => lenis?.scrollTo(id, { offset: -80, duration: 1.5 });

// --- LIFECYCLE ---
onMounted(() => {
  fetchStatus();
  fetchStack();
  intervalId = setInterval(fetchStatus, 60000);

  // Inicializar Lenis (Scroll Suave)
  lenis = new Lenis({ duration: 1.2, smoothWheel: true });
  function raf(time) {
    lenis.raf(time);
    requestAnimationFrame(raf);
  }
  requestAnimationFrame(raf);

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

      <section id="contact" class="py-32 px-4 max-w-5xl mx-auto">
        <ContactSection />
      </section>

    </main>

    <AppFooter />

    <ToasterComponent position="top-center" richColors />

    <Transition name="fade">
      <button
        v-if="showBackToTop"
        @click="scrollToTop"
        class="fixed bottom-8 right-8 z-40 p-3 rounded-full bg-emerald-500/10 hover:bg-emerald-500 text-emerald-500 hover:text-white border border-emerald-500/20 backdrop-blur-md transition-all shadow-lg group"
      >
        <ArrowUp class="w-6 h-6 group-hover:-translate-y-1 transition-transform" />
      </button>
    </Transition>

  </div>
</template>

<style>
/* Estilos obligatorios para Lenis */
html.lenis { height: auto; }
.lenis.lenis-smooth { scroll-behavior: auto !important; }
.lenis.lenis-smooth [data-lenis-prevent] { overscroll-behavior: contain; }
.lenis.lenis-stopped { overflow: hidden; }

.fade-enter-active, .fade-leave-active { transition: opacity 0.3s ease; }
.fade-enter-from, .fade-leave-to { opacity: 0; }
</style>
