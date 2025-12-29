import { createApp } from 'vue'
import './assets/main.css'
import App from './App.vue'

// Toaster 
import { Toaster } from 'vue-sonner'

import { MotionPlugin } from '@vueuse/motion'

const app = createApp(App)

app.component('ToasterComponent', Toaster)

app.use(MotionPlugin)

app.mount('#app')
