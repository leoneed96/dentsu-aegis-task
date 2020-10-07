import Vue from "vue";
import Vuex from "vuex";
import axios from '../plugins/axios';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    lastSearch: {
      "id": null,
      "searchString": null,
    },
    searchesLoaded: false,
    searches: [],
    searchRepositories: {}
  },
  mutations: {
    SET_SEARCHES(state, payload) {
      if (payload && payload.length) {
        state.searches = payload;
        state.lastSearch = payload[0];
        state.searchesLoaded = true;
      }
    },
    ADD_SEARCH(state, payload) {
      let search = {
        id: payload.id,
        searchString: payload.searchString
      };
      state.searches.unshift(search);
      state.searchRepositories[search.id] = payload.searchRequestAndRepositories.map(a => a.repository);
    },
    ADD_REPOS(state, payload){
      state.searchRepositories[payload.searchId] = payload.data;
    }
  },
  actions: {
    async getNewSearch(state, query) {
      var duplicate = this.state.searches.find(a => a.searchString == query);
      if(duplicate){
        return duplicate.id;
      }
      let response = await axios.getInstance().get(`/crud/search?query=${query}`);
      this.commit('ADD_SEARCH', response.data);
      return response.data.id;
    },
    async getLastSearch() {
      let response = await axios.getInstance().get('/crud/getSearches');
      this.commit('SET_SEARCHES', response.data);
    },
    async getRepositories(state, searchId){
      if(!this.state.searchRepositories[searchId])
      {      
        let response = await axios.getInstance().get(`/crud/getReposForSearch/${searchId}`);
        this.commit('ADD_REPOS', {data: response.data, searchId: searchId});
      }
      return this.state.searchRepositories[searchId];
    }
  },
  getters:{

  }
});
