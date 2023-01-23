<!-- eslint-disable vue/require-default-prop -->
<script setup>
import { ref } from 'vue'
import CreateTicketModal from 'src/components/CreateTicketModal.vue'
defineProps({
  msg: String
})

</script>
<script>
export default {
  name: 'ContentComponent',
  data () {
    return {
      // for development we point the url to the endpoint of local development server
      API_URL: 'https://localhost:5001/api/ticket',
      showCreateTicketModal: ref(false),
      fetched: []
      // initialze data component for fetching and populating the table
    }
  },
  created () {
    this.fetchData()
  },
  methods: {
    handleClose (event) {
      this.showCreateTicketModal = false
    },
    // asynchronously fetch the ticket data from the API server
    async fetchData () {
      const headers = {
        'Content-Type': 'application/json'
      }
      // define the content type for the response data from the API, in this case we want to get response as JSON data
      const fetchOptions = {
        method: 'GET',
        headers
      }
      // Using the GET method as per the OpenAPI specification defined in https://localhost:5001/swagger
      const payload = await fetch(this.API_URL, fetchOptions)
        .then(resp => resp.json())
      // store the api call response as JSON in local object 'payload'
      this.fetched = payload
      console.log(ref(this.fetched))
      /*
      move the local data to the component data defined in the data() function
      this is type-sensitive, so if response is an Array the variable should be initialized in data() as array
      and if response is an Object the variable should be initialized as an Object like 'objectvariable: {}'
      */
      // DEBUG:
      // console.log(this.fetched)
    }
  }
}

</script>
<template>
  <q-card>
    <h2 class="text-grey-5">
      {{ msg }}
    </h2>

    <q-btn
      icon="add"
      @click="showCreateTicketModal=true"
    />
  </q-card>
  <q-table
    :rows="fetched"
    row-key="ticketId"
  />
  <q-dialog
    v-model="showCreateTicketModal"
  >
    <CreateTicketModal @createDone="handleClose" />
  </q-dialog>
  <!-- we populate the data in the table by specifying :rows to get from data component 'fetched'
  row-key acts like a primary key and needs to be defined for future data interactions
  -->
</template>
