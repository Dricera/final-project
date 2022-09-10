<!-- eslint-disable vue/require-default-prop -->
<script setup>
import { ref } from 'vue'

defineProps({
  msg: String
})

const count = ref(0)
</script>
<script>
export default {
  name: 'ContentComponent',
  data () {
    return {
      API_URL: 'https://localhost:5001/api/ticket',
      fetched: []
    }
  },
  created () {
    this.fetchData()
  },
  methods: {

    async fetchData () {
      const headers = {
        'Content-Type': 'application/json'
      }
      const fetchOptions = {
        method: 'GET',
        headers
      }
      const payload = await fetch(this.API_URL, fetchOptions)
        .then(resp => resp.json())
      this.fetched = payload
      console.log(this.fetched)
    }
  }
}

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
  </q-card>
  <q-table
    :rows="fetched"
    row-key="ticketId"
  />
</template>

<style scoped>
.read-the-docs {
  color: #888;
}
</style>
