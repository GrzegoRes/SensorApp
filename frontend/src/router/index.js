import { createRouter, createWebHistory } from 'vue-router'

import Home from "../components/mainPage.vue"
import About from "../components/AboutView123.vue"
import Chart from "../components/ChartView123.vue"
import sensorId from "../components/LineChart.vue"


const routes = [
  {
    path: '/',
    name: 'home',
    component: Home
  },
  {
    path: '/about',
    name: 'about',
    component: About
  },
  {
    path: '/chart',
    name: 'chart',
    component: Chart
  },
  {
    path: '/sensor/:id',
    name: 'sensorId',
    component: sensorId
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
