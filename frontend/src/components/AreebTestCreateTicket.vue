<!-- <template>
  <div>
    <q-card
      class="q-pa-md text-white-5"
      style="width: 500px; max-width: 80vw;"
    >
      <q-card-section horizontal>
        <q-card-section>
          <div class="text-center q-pt-lg">
            <div class="col text-h5 ellipsis">
              Create Ticket
            </div>
          </div>
          <q-form
            class="q-gutter-md"
            @submit="createTicket"
          >
            <q-input
              v-model="Text_e_id"
              label="Employee ID"
              standout="bg-green text-white"
              :rules="[ val => val && val.length > 0 || 'This field is required']"
            />

            <q-input
              v-model="Text_e_name"
              label="Employee Name"
              standout="bg-green text-white"
              :rules="[ val => val && val.length > 0 || 'This field is required']"
            />

            <q-input
              v-model="Text_e_dep"
              label="Department"
              standout="bg-green text-white"
              :rules="[ val => val && val.length > 0 || 'This field is required']"
            />

            <q-input
              v-model="Text_des"
              label="Designation"
              standout="bg-green text-white"
              :rules="[ val => val && val.length > 0 || 'This field is required']"
            />

            <q-select
              v-model="Select_type"
              filled
              :options="type_options"
              label="Type"
            />

            <q-select
              v-model="Select_priority"
              filled
              :options="priority_options"
              label="Priority"
            />
          </q-form>
        </q-card-section>

        <q-separator vertical />

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
            <div>
              <q-btn
                round
                color=""
                icon="shopping_cart"
              />
            </div>
          </q-form>
        </q-card-section>
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
import { ref } from 'vue'

export default {
  emits: ['createDone'],
  setup () {
    return {
      Text_e_id: ref(''),
      Text_e_name: ref(''),
      Text_dep: ref(''),
      Text_des: ref(''),
      Select_type: ref(null),
      type_options: [
        '1', '2', '3', '4'
      ],
      Select_priority: ref(null),
      priority_options: [
        '1', '2', '3,', '4'
      ]
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
      const API_URL = 'https://localhost:5001/api/ticket'
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
</script> -->
