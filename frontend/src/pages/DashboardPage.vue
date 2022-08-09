<template>
  <q-page>
    <q-card class="q-pa-md bg-image text-white-5">
      <q-card-section>
        <div class="text-center q-pt-lg">
          <div class="col text-h6 ellipsis">
            Log in
          </div>
        </div>
      </q-card-section>
      <q-card-section>
        <q-form
          class="q-gutter-md"
        >
          <q-input
            v-model="username"
            filled
            label="Username"
            lazy-rules
          />

          <q-input
            v-model="password"
            type="password"
            filled
            label="Password"
            lazy-rules
          />

          <div>
            <q-btn
              class="q-px-md q-py-sm"
              label="Login"
              to="/"
              type="button"
              color="primary"
            />
          </div>
        </q-form>
      </q-card-section>
    </q-card>
  </q-page>
</template>

<script>
import { defineComponent, ref } from 'vue'

export default defineComponent({
  name: 'DashboardPage',
  setup () {
    return {
      username: ref('Pratik'),
      password: ref('12345')
    }
  }
})
</script>

<style>

.bg-image {
  background-image: linear-gradient(135deg, #7028e4 0%, #e5b2ca 100%);
}
</style>
