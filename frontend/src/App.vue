<script>
import Home from "./components/mainPage.vue"
import About from "./components/AboutView123.vue"
import Chart from "./components/ChartView123.vue"
import NotFound from "./components/AboutView123.vue"

const routes = {
  '/': Home,
  '/about': About,
  '/chart': Chart
}

export default {
  data() {
    return {
      currentPath: window.location.hash
    }
  },
  computed: {
    currentView() {
      return routes[this.currentPath.slice(1) || '/'] || NotFound
    }
  },

  mounted() {
    window.addEventListener('hashchange', () => {
      this.currentPath = window.location.hash
		})
  }

}
</script>

<template>
  <h1>SENSOR APP</h1>
  <a href="#/">Home</a> |
  <a href="#/about">Tabele</a> |
  <a href="#/chart">Chart</a> |
  <component :is="currentView" />
</template>
