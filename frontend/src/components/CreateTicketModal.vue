<template>
  <div>
    <q-card
      class="q-pa-md text-white-5"
      style="width: 500px; max-width: 80vw;"
    >
      <q-title>
        <q-toolbar>
          <q-toolbar-title align="center">
            Create Ticket
          </q-toolbar-title>
        </q-toolbar>
      </q-title>
      <q-card-section>
        <q-form
          class="q-gutter-md"
          @submit="createTicket"
        >
          <q-input
            v-model="ticket.subject"
            label="Ticket Subject"
            standout="bg-green text-white"
            :rules="[ val => val && val.length > 0 || 'This field is required']"
          />

          <q-input
            v-model="ticket.description"
            type="textarea"
            filled
            label="Ticket Description"
            :rules="[ val => val && val.length > 0 || 'This field is required']"
          />
          <div>
            <q-btn
              class="q-px-md q-py-sm"
              label="Create"
              type="submit"
              color="primary"
            />
          </div>
        </q-form>
      </q-card-section>
    </q-card>
  </div>
  <q-form>
    <q-card-section>
      <q-btn
        v-close-popup
        class="absolute-top-right"
        icon="close"
        flat
        round
        dense
      />
    </q-card-section>
  </q-form>
</template>

<script>

export default {
  emits: ['createDone'],
  setup () {
    return {
      //
    }
  },
  data () {
    return {
    //
      ticket: {
        subject: '',
        description: ''
      }
    }
  },
  methods: {
    async createTicket () {
      const API_URL = '/api/ticket'
      const fetchOptions = {
        headers: {
          'Content-Type': 'application/json'
          // 'Content-Type': 'application/x-www-form-urlencoded',
        },
        method: 'POST',
        body: JSON.stringify(this.ticket)
      }
      // Using the GET method as per the OpenAPI specification defined in https://localhost:5001/swagger
      const payload = await fetch(API_URL, fetchOptions)
        .then(resp => resp.json()).then(this.$emit('createDone', 'false'))
      // store the api call response as JSON in local object 'payload'
      console.log(payload)
    }
  }

}
</script>
