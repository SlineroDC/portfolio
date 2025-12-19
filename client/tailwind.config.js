/** @type {import('tailwindcss').Config} */
export default {
  // 1. Aquí le decimos a Tailwind dónde buscar nuestras clases
  content: [
    "./index.html",
    "./src/**/*.{vue,js,ts,jsx,tsx}",
  ],
  darkMode: 'class', 
  theme: {
    extend: {
      // 2. Tipografía estilo Apple
      fontFamily: {
        sans: ['Inter', 'sans-serif'], 
        mono: ['Fira Code', 'monospace'],
      },
      // 3. Colores personalizados para el efecto cristal
      colors: {
        glass: {
          surface: 'rgba(255, 255, 255, 0.03)', // Fondo casi transparente
          border: 'rgba(255, 255, 255, 0.08)',  // Borde sutil
          highlight: 'rgba(255, 255, 255, 0.1)', // Brillo al pasar el mouse
        }
      },
      // 4. Animaciones personalizadas
      animation: {
        'marquee': 'marquee 25s linear infinite', // Para el carrusel de stack
        'pulse-slow': 'pulse 4s cubic-bezier(0.4, 0, 0.6, 1) infinite', // Para el status
      },
      keyframes: {
        marquee: {
          '0%': { transform: 'translateX(0%)' },
          '100%': { transform: 'translateX(-50%)' },
        }
      }
    },
  },
  plugins: [],
}