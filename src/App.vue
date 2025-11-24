<!-- Ejemplo visual de App.vue -->
<script setup>
  // ... imports
  import ProjectModal from './components/modals/ProjectModal.vue';
  import StackModal from './components/modals/StackModal.vue';
  // ...
</script>

<template>
  <!-- ... navbar, main, footer ... -->
  
  <Teleport to="body">
    <Transition name="fade">
      <ProjectModal v-if="showProjects" @close="showProjects = false" />
    </Transition>
  </Teleport>

  <Teleport to="body">
    <Transition name="fade">
      <StackModal v-if="showStack" @close="showStack = false" />
    </Transition>
  </Teleport>
</template>