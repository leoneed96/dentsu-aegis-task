<template>
  <v-container>
    <div class="d-flex flex-row align-center">
       <v-autocomplete
      :loading="!state.searchesLoaded"
      v-model="selectedSearch"
      :items="state.searches"
      item-text="searchString"
      item-value="id"
      label="Saved search"
    ></v-autocomplete>
    <v-btn @click="refreshSearch" class="ma-2" color="info">Refresh selected search</v-btn>

    </div>

    <div class="d-flex flex-row align-center">
      <v-text-field v-model="newSearch"></v-text-field>
      <v-btn @click="initNewSearch" class="ma-2" color="info">Search</v-btn>

    </div>
   <div v-if="state.repositoriesLoaded">
    <div v-if="repositories.length">
      <v-card class="ma-1" v-for="repo in repositories" :key="repo.id">
        <v-card-title>
          <div class="flex-grow-1 mb-6 d-flex flex-row align-center">
            <div>{{ repo.title }}</div>
            <a target="blank" :href="repo.link">
              <v-icon class="pa-1 mdi mdi-link-variant" />
            </a>
          </div>
          <div class="mb-6 d-flex flex-row align-center">
            Stars
            <v-badge inline color="info" :content="repo.stars || '0'">
            </v-badge>

            Forks
            <v-badge inline color="info" :content="repo.forks || '0'">
            </v-badge>
          </div>
        </v-card-title>
        <v-card-subtitle>
          <div class="d-flex flex-row align-center">
            <span>Author:</span>
            <span class="font-weight-bold pl-2"> {{ repo.authorLogin }}</span>
            <v-avatar size="36px" class="ml-1">
              <v-img :src="repo.authorAvatar" />
            </v-avatar>
          </div>
          <div>
            <span>Language:</span>
            <span class="font-weight-bold"> {{ repo.codeLanguage }}</span>
          </div>
          <div>
            <span>Last updated:</span>
            <span class="font-weight-bold">
              {{ getReadableDateString(repo.lastUpdate) }}</span
            >
          </div>
        </v-card-subtitle>
        <v-card-text>
          <div>{{ repo.description || "No description" }}</div>
        </v-card-text>
      </v-card>
    </div>
    <div class="text-center" v-else-if="selectedSearch">No repositories found</div>
    <div class="text-center" v-else>No last search found. Init new search to see repositories.</div>
   </div>
   <div v-else>
     <v-progress-linear
      indeterminate
      color="primary"
    ></v-progress-linear>
   </div>
  </v-container>
</template>

<script>
import Vuex from "vuex";
export default {
  name: "Repositories",

  data: () => ({
    selectedSearch: null,
    newSearch: "",
    repositories: []
  }),
  computed: {
    state() {
      return this.$store.state;
    }
  },
  watch: {
    async selectedSearch(val, old) {
      this.repositories = await this.$store.dispatch("getRepositories", val);
    }
  },
  methods: {
    async initLastSearch() {
      await this.$store.dispatch("getLastSearch");
      if (this.state.lastSearch) this.selectedSearch = this.state.lastSearch.id;
    },
    getReadableDateString(isoDateString) {
      try {
        let date = new Date(isoDateString);
        return date.toLocaleString();
      } catch {
        return "N/A";
      }
    },
    async initNewSearch(){
       this.selectedSearch = await this.$store.dispatch('getNewSearch', this.newSearch);
       console.log(this.selectedSearch);
    },
    async refreshSearch(){
       await this.$store.dispatch('refreshSearch', this.selectedSearch);
    }
  },
  async mounted() {
    await this.initLastSearch();
  }
};
</script>
