<template>
  <v-app>
    <v-app-bar
      app
      color="black"
      dark
    >
      leoneed's
    </v-app-bar>

    <v-main>
      <Repositories/>
    </v-main>
  </v-app>
</template>

<script>
import Repositories from './components/Repositories';
import Vuex from "vuex";

export default {
  name: 'App',

  components: {
    Repositories,
  },
};
</script>
