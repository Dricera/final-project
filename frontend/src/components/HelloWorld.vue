<script setup>
import { ref } from 'vue'

defineProps({
  msg: String
})

const count = ref(0)
</script>

<template>
  <q-card>
    <h2 class="text-grey-5">
      {{ msg }}
    </h2>

    <q-chip
      icon="add"
      clickable
      @click="count++"
    >
      count is {{ count }}
    </q-chip>

    <div class="q-mt-sm">
      <code>components/HelloWorld.vue</code> test HMR
    </div>
  </q-card>
</template>

<style scoped>
.read-the-docs {
  color: #888;
}
</style>
