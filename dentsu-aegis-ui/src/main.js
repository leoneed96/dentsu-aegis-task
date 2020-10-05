import Vue from "vue";
import App from "./App.vue";
import store from "./store";
import vuetify from './plugins/vuetify';
import axios from './plugins/axios';
import 'roboto-fontface/css/roboto/roboto-fontface.css'
import '@mdi/font/css/materialdesignicons.css'

Vue.config.productionTip = false;

new Vue({
  store,
  axios,
  vuetify,
  render: h => h(App)
}).$mount("#app");
