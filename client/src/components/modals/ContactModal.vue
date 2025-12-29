<script setup>
// Importamos 'ref' para crear variables reactivas (que actualizan el HTML al cambiar)
import { ref } from 'vue';

// Importamos los iconos visuales de la librería Lucide
import { X, Send, Mail, Loader2, CheckCircle } from 'lucide-vue-next';

// Importamos la función 'toast' para disparar las notificaciones emergentes
import { toast } from 'vue-sonner';

// Definimos los eventos que este componente puede emitir hacia el padre (App.vue)
const emit = defineEmits(['close']);

// Leemos la URL de la API desde las variables de entorno (.env)
// Esto asegura que funcione tanto en local como en producción (Railway)
const API_URL = import.meta.env.VITE_API_URL;

// Objeto reactivo que almacena los datos que el usuario escribe
const form = ref({
    name: '',
    email: '',
    message: ''
});

// Estados para controlar la Interfaz de Usuario (UI)
const isLoading = ref(false);   // Controla si mostramos el spinner de carga
const showSuccess = ref(false); // Controla si mostramos la pantalla de "Gracias"

// --- LÓGICA DE ENVÍO ---
const submitForm = async () => {
    // 1. Validación simple: Si algún campo está vacío, mostramos alerta amarilla y paramos.
    if (!form.value.name || !form.value.email || !form.value.message) {
        toast.warning('Por favor completa todos los campos');
        return;
    }

    // 2. Iniciamos el proceso de carga (bloquea inputs y muestra spinner)
    isLoading.value = true;

    // 3. Mostramos notificación de "Cargando..." y guardamos su ID para cerrarla luego
    const toastId = toast.loading('Enviando mensaje...');

    try {
        // 4. Hacemos la petición POST a tu Backend (.NET/Railway)
        const response = await fetch(`${API_URL}/api/contact`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(form.value) // Convertimos el objeto JS a texto JSON
        });

        if (response.ok) {
            // --- CASO ÉXITO ---

            // Quitamos la notificación de carga
            toast.dismiss(toastId);

            // Mostramos la notificación verde de éxito
            toast.success('¡Mensaje enviado correctamente!');

            // Cambiamos la vista del modal a la pantalla de "Check verde gigante"
            showSuccess.value = true;

            // Limpiamos los campos del formulario
            form.value = { name: '', email: '', message: '' };

            // Temporizador: Esperamos 2.5 segundos para que el usuario lea el éxito y cerramos el modal
            setTimeout(() => {
                emit('close'); // Avisamos a App.vue que cierre el modal

                // Pequeño truco: reseteamos la vista interna un poco después para que la próxima vez se vea el formulario
                setTimeout(() => { showSuccess.value = false; }, 300);
            }, 2500);

        } else {
            // Si el servidor responde con error (ej. 500 o 400), forzamos ir al bloque catch
            throw new Error('Error en el servidor');
        }

    } catch (error) {
        // --- CASO ERROR ---
        console.error(error);

        // Quitamos la notificación de carga
        toast.dismiss(toastId);

        // Mostramos notificación roja explicando el error
        toast.error('Hubo un error al conectar con el servidor.');

    } finally {
        // 5. Finalizamos: Sea éxito o error, siempre desbloqueamos el botón
        isLoading.value = false;
    }
};
</script>

<template>
  <div class="fixed inset-0 z-[100] flex items-center justify-center p-4 liquid-backdrop" @click="$emit('close')">

    <div class="bg-[#0a0a0a]/95 backdrop-blur-3xl w-full max-w-lg rounded-[2rem] shadow-2xl p-8 border border-white/10 relative overflow-hidden" @click.stop>

      <button @click="$emit('close')" class="absolute top-6 right-6 w-10 h-10 rounded-full bg-white/5 flex items-center justify-center hover:bg-white/10 transition-colors text-white z-10">
        <X class="w-5 h-5" />
      </button>

      <div class="text-center mb-8 relative">
          <div class="w-16 h-16 bg-emerald-500/10 rounded-full flex items-center justify-center mx-auto mb-4 text-emerald-500 transition-all duration-500">
              <CheckCircle v-if="showSuccess" class="w-8 h-8 animate-bounce" />
              <Mail v-else class="w-8 h-8" />
          </div>
          <h2 class="text-2xl font-bold text-white transition-all">
              {{ showSuccess ? '¡Mensaje Enviado!' : 'Contáctame' }}
          </h2>
          <p class="text-gray-400 text-sm mt-2">
              {{ showSuccess ? 'Gracias por escribirme. Te responderé pronto.' : '¿Tienes un proyecto en mente? Hablemos.' }}
          </p>
      </div>

      <div v-if="showSuccess" class="py-10 flex justify-center">
          <p class="text-emerald-400 font-medium animate-pulse">Cerrando ventana...</p>
      </div>

      <form v-else @submit.prevent="submitForm" class="space-y-4">

          <div>
              <label class="block text-xs font-bold text-gray-500 uppercase mb-2">Tu Nombre</label>
              <input
                v-model="form.name"
                type="text"
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
                class="w-full bg-white/5 border border-white/10 rounded-xl p-3 text-white focus:border-emerald-500 focus:outline-none transition-colors resize-none"
                placeholder="¿Cómo puedo ayudarte?"
                :disabled="isLoading"
              ></textarea>
          </div>

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
