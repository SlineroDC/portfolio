<script setup>
import { ref } from 'vue';
import { X, Send, Mail } from 'lucide-vue-next';

defineEmits(['close']);

// Datos del formulario
const form = ref({
    name: '',
    email: '',
    message: ''
});

// Función para abrir el cliente de correo
const sendEmail = () => {
    const subject = `Nuevo contacto de portafolio: ${form.value.name}`;
    const body = `Hola Sebastian,%0D%0A%0D%0A${form.value.message}%0D%0A%0D%0AAtentamente,%0D%0A${form.value.name}%0D%0A${form.value.email}`;
    // Reemplaza con tu email real
    window.location.href = `mailto:sebastianlinero15@gmail.com?subject=${subject}&body=${body}`;
};
</script>

<template>
  <div class="fixed inset-0 z-[100] flex items-center justify-center p-4 liquid-backdrop" @click="$emit('close')">
    <div class="bg-[#0a0a0a]/95 backdrop-blur-3xl w-full max-w-lg rounded-[2rem] shadow-2xl p-8 border border-white/10 relative overflow-hidden" @click.stop>
      
      <button @click="$emit('close')" class="absolute top-6 right-6 w-10 h-10 rounded-full bg-white/5 flex items-center justify-center hover:bg-white/10 transition-colors text-white">
        <X class="w-5 h-5" />
      </button>

      <div class="text-center mb-8">
          <div class="w-16 h-16 bg-emerald-500/10 rounded-full flex items-center justify-center mx-auto mb-4 text-emerald-500">
              <Mail class="w-8 h-8" />
          </div>
          <h2 class="text-2xl font-bold text-white">Contáctame</h2>
          <p class="text-gray-400 text-sm mt-2">¿Tienes un proyecto en mente? Hablemos.</p>
      </div>

      <form @submit.prevent="sendEmail" class="space-y-4">
          <div>
              <label class="block text-xs font-bold text-gray-500 uppercase mb-2">Tu Nombre</label>
              <input v-model="form.name" type="text" required class="w-full bg-white/5 border border-white/10 rounded-xl p-3 text-white focus:border-emerald-500 focus:outline-none transition-colors" placeholder="Ej. Juan Pérez">
          </div>
          
          <div>
              <label class="block text-xs font-bold text-gray-500 uppercase mb-2">Tu Email</label>
              <input v-model="form.email" type="email" required class="w-full bg-white/5 border border-white/10 rounded-xl p-3 text-white focus:border-emerald-500 focus:outline-none transition-colors" placeholder="juan@empresa.com">
          </div>
          
          <div>
              <label class="block text-xs font-bold text-gray-500 uppercase mb-2">Mensaje</label>
              <textarea v-model="form.message" rows="4" required class="w-full bg-white/5 border border-white/10 rounded-xl p-3 text-white focus:border-emerald-500 focus:outline-none transition-colors resize-none" placeholder="¿Cómo puedo ayudarte?"></textarea>
          </div>

          <button type="submit" class="w-full bg-emerald-500 hover:bg-emerald-600 text-black font-bold py-4 rounded-xl flex items-center justify-center gap-2 transition-transform active:scale-95">
              <span>Enviar Mensaje</span>
              <Send class="w-5 h-5" />
          </button>
      </form>

    </div>
  </div>
</template>