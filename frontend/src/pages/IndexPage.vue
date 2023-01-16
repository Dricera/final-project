<template>
  <q-page
    class="flex flex-center"
  >
    <q-item to="/home">
      <img
        alt="Quasar logo"
        src="~assets/quasar-logo-vertical.svg"
        style="width: 200px; height: 200px"
        class="bg-grey-8"
      >
    </q-item>
  </q-page>
</template>

<script>
import { defineComponent } from 'vue'

export default defineComponent({
  name: 'IndexPage'
})
</script>
