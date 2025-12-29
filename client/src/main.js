import { createApp } from 'vue'

import './assets/main.css'

import App from './App.vue'

import { Toaster} from 'vue-sonner'

const app = createApp(App)
app.component('ToasterComponent', Toaster)
app.mount('#app')
