import Vue from "vue";
import Vuex from "vuex";
import axios from '../plugins/axios';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {},
  mutations: {},
  actions: {
    getLastSearch(){
      axios.getInstance().get('test');
    }
  },
  modules: {}
});
