<script>
// import { ref } from 'vue'

export default {
  name: 'REST',
  //
  methods: {
    async getTickets () {
      const API_URL = 'https://localhost:5001/api/ticket/'
      const fetchOptions = {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json'
        }
      }
      const response = await fetch(API_URL, fetchOptions)
      const respJson = await response.json()
      console.log(respJson)
      return respJson
    },
    async deleteTicket (ticketID) {
      const API_URL = 'https://localhost:5001/api/ticket/' + ticketID
      const fetchOptions = {
        method: 'DELETE',
        headers: {
          'Content-Type': 'application/json'
        }
      }
      const response = await fetch(API_URL, fetchOptions)
      await response.json()
      console.log('Deleted ticket id: ' + ticketID)
    },
    async patchTicket (ticketID, ticketPatch) {
      const API_URL = 'https://localhost:5001/api/ticket/' + ticketID
      const fetchOptions = {
        method: 'PATCH',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(ticketPatch)
      }
      const response = await fetch(API_URL, fetchOptions)
      const respJson = await response.json()
      console.log('Updated: \n' + respJson)
    }
  }
}

</script>
