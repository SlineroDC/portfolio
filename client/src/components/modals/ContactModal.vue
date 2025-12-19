<script setup>
import { ref } from 'vue';
import { X, Send, Mail, Loader2, CheckCircle } from 'lucide-vue-next';

const emit = defineEmits(['close']);

// Leemos la URL desde el archivo .env (o Vercel en producción)
const API_URL = import.meta.env.VITE_API_URL;

// Datos del formulario
const form = ref({
    name: '',
    email: '',
    message: ''
});

// Estados para la UI
const isLoading = ref(false);
const showSuccess = ref(false);
const errorMessage = ref('');

// Función para enviar a la API .NET
const submitForm = async () => {
    isLoading.value = true;
    errorMessage.value = '';

    try {
        // Usamos la variable API_URL en lugar de localhost fijo
        const response = await fetch(`${API_URL}/api/contact`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(form.value)
        });

        if (response.ok) {
            // Éxito: Mostramos mensaje y limpiamos
            showSuccess.value = true;
            form.value = { name: '', email: '', message: '' };
            
            // Cerramos el modal automáticamente después de 2.5 segundos
            setTimeout(() => {
                emit('close');
                // Resetear estado visual por si vuelve a abrir (pequeño delay para que no se vea el cambio brusco)
                setTimeout(() => { showSuccess.value = false; }, 300); 
            }, 2500);
        } else {
            throw new Error('Error en el servidor');
        }
    } catch (error) {
        console.error(error);
        errorMessage.value = "Hubo un error al enviar el mensaje. Inténtalo de nuevo.";
    } finally {
        isLoading.value = false;
    }
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
              <CheckCircle v-if="showSuccess" class="w-8 h-8 animate-bounce" />
              <Mail v-else class="w-8 h-8" />
          </div>
          <h2 class="text-2xl font-bold text-white">
              {{ showSuccess ? '¡Mensaje Enviado!' : 'Contáctame' }}
          </h2>
          <p class="text-gray-400 text-sm mt-2">
              {{ showSuccess ? 'Gracias por escribirme. Te responderé pronto.' : '¿Tienes un proyecto en mente? Hablemos.' }}
          </p>
      </div>

      <div v-if="showSuccess" class="py-10 flex justify-center">
          <p class="text-emerald-400 font-medium animate-pulse">Redirigiendo al portafolio...</p>
      </div>

      <form v-else @submit.prevent="submitForm" class="space-y-4">
          <div>
              <label class="block text-xs font-bold text-gray-500 uppercase mb-2">Tu Nombre</label>
              <input 
                v-model="form.name" 
                type="text" 
                required 
                class="w-full bg-white/5 border border-white/10 rounded-xl p-3 text-white focus:border-emerald-500 focus:outline-none transition-colors" 
                placeholder="Ej. Juan Pérez"
                :disabled="isLoading"
              >
          </div>
          
          <div>
              <label class="block text-xs font-bold text-gray-500 uppercase mb-2">Tu Email</label>
              <input 
                v-model="form.email" 
                type="email" 
                required 
                class="w-full bg-white/5 border border-white/10 rounded-xl p-3 text-white focus:border-emerald-500 focus:outline-none transition-colors" 
                placeholder="juan@empresa.com"
                :disabled="isLoading"
              >
          </div>
          
          <div>
              <label class="block text-xs font-bold text-gray-500 uppercase mb-2">Mensaje</label>
              <textarea 
                v-model="form.message" 
                rows="4" 
                required 
                class="w-full bg-white/5 border border-white/10 rounded-xl p-3 text-white focus:border-emerald-500 focus:outline-none transition-colors resize-none" 
                placeholder="¿Cómo puedo ayudarte?"
                :disabled="isLoading"
              ></textarea>
          </div>

          <p v-if="errorMessage" class="text-red-400 text-xs text-center font-medium">
              {{ errorMessage }}
          </p>

          <button 
            type="submit" 
            :disabled="isLoading"
            class="w-full bg-emerald-500 hover:bg-emerald-600 disabled:bg-emerald-500/50 disabled:cursor-not-allowed text-black font-bold py-4 rounded-xl flex items-center justify-center gap-2 transition-transform active:scale-95"
          >
              <span v-if="isLoading" class="flex items-center gap-2">
                  <Loader2 class="w-5 h-5 animate-spin" /> Enviando...
              </span>
              <span v-else class="flex items-center gap-2">
                  Enviar Mensaje <Send class="w-5 h-5" />
              </span>
          </button>
      </form>

    </div>
  </div> 
</template>