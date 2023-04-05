<!-- eslint-disable vue/require-default-prop -->
<script setup>
import { ref } from 'vue'
import REST from './REST.vue'
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
      showCreateTicketModal: ref(false),
      fetched: localStorage.getItem('fetched') ? JSON.parse(localStorage.getItem('fetched')) : [],
      tableData: []
      // initialze data component for fetching and populating the table
    }
  },
  created () {
    this.fetchData()
  },
  methods: {
    notify (message, type) {
      this.$q.notify({
        message,
        type,
        position: 'bottom',
        progress: true,
        timeout: 2000
      })
    },
    handleClose (event) {
      this.showCreateTicketModal = false
    },
    async deleteTicket (ticketID) {
    // eslint-disable-next-line no-undef
      await REST.methods.deleteTicket(ticketID)
        .then(this.notify('Ticket Deleted', 'warning'))
    },
    // asynchronously fetch the ticket data from the API server
    async fetchData () {
    // define the content type for the response data from the API, in this case we want to get response as JSON data
    // Using the GET method as per the OpenAPI specification defined in https://localhost:5001/swagger
      const payload = await REST.methods.getTickets()
        .then(this.notify('Ticket Data Fetched', 'positive'))
      // .then(resp => resp.json())
      // store the api call response as JSON in local object 'payload'
      this.fetched = await payload
      // filter the dates to be in a more readable format
      this.fetched.forEach((ticket) => {
        ticket.createdDate = new Date(ticket.createdDate).toLocaleString()
      })
      // populate the tableData with the fetched data
      this.fetched.forEach((ticket) => {
      // filter tableData with ticket's id, subject, description and status
        this.tableData.push({
          ticketId: ticket.id,
          subject: ticket.subject,
          description: ticket.description,
          status: ticket.status,
          created: ticket.createdDate
        })
      })
    }
  }
}

</script>
<template>
  <q-card>
    <h4 class="text-grey-5 ">
      {{ msg }}
    </h4>

    <q-btn
      icon="add"
      label="Add Ticket"
      color="positive"
      @click="showCreateTicketModal = true"
    />
  </q-card>
  <!-- add refresh button to q-table -->
  <q-table
    title="Ticket List"
    :rows="tableData"
    row-key="ticketId"
  />
  <!-- add form asking for ticket id to delete ticket -->
  <q-form>
    <q-input
      v-model="ticketId"
      label="Enter Ticket ID to delete"
      filled
      type="text"
    >
      <template #after>
        <q-btn
          label="Delete Ticket"
          color="negative"
          icon="delete"
          @click="deleteTicket(ticketId)"
        />
      </template>
    </q-input>
  </q-form>
  <q-dialog v-model="showCreateTicketModal">
    <!-- eslint-disable-next-line vue/v-on-event-hyphenation -->
    <CreateTicketModal @createDone="handleClose" />
  </q-dialog>
  <!-- we populate the data in the table by specifying :rows to get from data component 'fetched'
            row-key acts like a primary key and needs to be defined for future data interactions
            -->
</template>
