import Vue from "vue";
import Vuex from "vuex";
import axios from '../plugins/axios';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    currentSearch: {
      "ID": null,
      "Title": null,
    },
    searches: [],
    repositories: {}
  },
  mutations: {
    SET_SEARCHES(state, payload){
      state.searches = payload;
    }
  },
  actions: {
    async getLastSearch(){
      debugger;
      let data = await axios.getInstance().get('crud/getSearches');
      this.commit('SET_SEARCHES', data);
    }
  },
  modules: {}
});
