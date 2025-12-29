<script setup>
import { ref } from 'vue';
import { Send, Loader2, Mail, MapPin } from 'lucide-vue-next';
import { toast } from 'vue-sonner';

// URL de la API
const API_URL = import.meta.env.VITE_API_URL;

const form = ref({ name: '', email: '', message: '' });
const isLoading = ref(false);

const handleSubmit = async () => {
  if (!form.value.name || !form.value.email || !form.value.message) {
    toast.warning('Por favor completa todos los campos');
    return;
  }

  isLoading.value = true;
  const toastId = toast.loading('Enviando mensaje...');

  try {
    const response = await fetch(`${API_URL}/api/contact`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(form.value)
    });

    if (response.ok) {
      toast.dismiss(toastId);
      toast.success('¡Mensaje enviado con éxito! 🚀');
      form.value = { name: '', email: '', message: '' };
    } else {
      throw new Error('Error en servidor');
    }
  } catch {
    toast.dismiss(toastId);
    toast.error('Hubo un error al enviar. Intenta de nuevo.');
  } finally {
    isLoading.value = false;
  }
};
</script>

<template>
  <div class="relative rounded-[3rem] bg-[#0a0a0a] border border-white/10 overflow-hidden" v-motion-slide-visible-once>

    <div class="grid md:grid-cols-5">

      <div class="md:col-span-2 p-10 bg-gradient-to-br from-emerald-900/20 to-black flex flex-col justify-between relative overflow-hidden">
        <div class="relative z-10 space-y-6">
          <h2 class="text-3xl font-bold text-white">¿Hablamos?</h2>
          <p class="text-gray-400 text-sm leading-relaxed">
            Actualmente estoy <strong>disponible</strong> para nuevos proyectos y oportunidades laborales.
            Si tienes una idea o necesitas un desarrollador comprometido, escríbeme.
          </p>

          <div class="space-y-4 pt-4">
            <div class="flex items-center gap-3 text-gray-300">
               <div class="p-2 bg-white/5 rounded-full"><Mail class="w-4 h-4 text-emerald-400"/></div>
               <span class="text-sm">sebastianlinero15@gmail.com</span>
            </div>
            <div class="flex items-center gap-3 text-gray-300">
               <div class="p-2 bg-white/5 rounded-full"><MapPin class="w-4 h-4 text-emerald-400"/></div>
               <span class="text-sm">Barranquilla, Colombia</span>
            </div>
          </div>
        </div>

        <div class="absolute bottom-0 left-0 w-64 h-64 bg-emerald-500/10 rounded-full blur-[80px] pointer-events-none"></div>
      </div>

      <div class="md:col-span-3 p-10 bg-[#0a0a0a]">
        <form @submit.prevent="handleSubmit" class="space-y-6">

          <div class="grid md:grid-cols-2 gap-6">
            <div class="space-y-2">
              <label class="text-xs font-bold text-gray-500 uppercase ml-1">Nombre</label>
              <input
                v-model="form.name" type="text" placeholder="Tu nombre"
                class="w-full bg-white/5 border border-white/10 rounded-xl px-4 py-3 text-white focus:outline-none focus:border-emerald-500 transition-colors"
                :disabled="isLoading"
              />
            </div>
            <div class="space-y-2">
              <label class="text-xs font-bold text-gray-500 uppercase ml-1">Email</label>
              <input
                v-model="form.email" type="email" placeholder="tu@email.com"
                class="w-full bg-white/5 border border-white/10 rounded-xl px-4 py-3 text-white focus:outline-none focus:border-emerald-500 transition-colors"
                :disabled="isLoading"
              />
            </div>
          </div>

          <div class="space-y-2">
            <label class="text-xs font-bold text-gray-500 uppercase ml-1">Mensaje</label>
            <textarea
              v-model="form.message" rows="4" placeholder="Cuéntame sobre tu proyecto..."
              class="w-full bg-white/5 border border-white/10 rounded-xl px-4 py-3 text-white focus:outline-none focus:border-emerald-500 transition-colors resize-none"
              :disabled="isLoading"
            ></textarea>
          </div>

          <button
            type="submit"
            :disabled="isLoading"
            class="w-full bg-emerald-500 hover:bg-emerald-400 text-black font-bold py-4 rounded-xl flex items-center justify-center gap-2 transition-transform active:scale-95 disabled:opacity-50 disabled:cursor-not-allowed"
          >
             <span v-if="isLoading" class="flex items-center gap-2"><Loader2 class="w-5 h-5 animate-spin"/> Enviando...</span>
             <span v-else class="flex items-center gap-2">Enviar Mensaje <Send class="w-5 h-5"/></span>
          </button>
        </form>
      </div>

    </div>
  </div>
</template>
