<template>
  <h1>Sensor: {{id}}</h1>
  <p>Data: <input type="datetime-local" v-model="date_from2"> 
      <input type="datetime-local" v-model="date_to"></p>
      <button @click="update123">Search</button>
  <div class = "size">
    <canvas id="myChart" width="100" height="100"> </canvas>

    
  </div>
</template>

<script>
import Chart from 'chart.js/auto';
import axios from 'axios'

export default {
  name: "HelloWorld",
  myChart: 0,
  values: 0,
  date_from2: null, 
  date_to: null,
  sensor_name: null,
  props:{
    msg: String
  },

  data(){
    return{
      id: this.$route.params.id,
  }},

  mounted(){
    const ctx = document.getElementById('myChart');
    const labels=  [];
    const data = {
    labels: labels,
    datasets: [{
      label: '',  /* Nazwa */
      data: [],
      fill: false,
      borderColor: 'rgb(75, 192, 192)',
      tension: 0.3
  }]};

  this.myChart = new Chart(ctx, {
    type: 'line',
    data: data,
    
    //tu juz nadto
    options: {
    scales: {

      y: {
        title: {
          display: true,
          text: 'value',
        },
          min: -300,
          max: 300,
      }
    },
    ticks: {
                  autoSkip: false,
                  maxTicksLimit: 10
                }
  }});

  this.myChart;

  this.update123()
  },

  methods:{
    async update123() 
    {
      let result = await axios.post("http://localhost:80/Chart",
      {
        name:this.id,
        date_from: this.date_from2,
        date_to: this.date_to
      });
      var valueX = result.data.dateGeneration;
      var valueY = result.data.valueGeneration;
      const data2 = {
            labels: valueX,
            datasets: [{
              label: this.id,
              data: valueY,
              fill: false,
              borderColor: 'rgb(75, 192, 192)',
              tension: 0.3
          }]};
      this.myChart.options.scales.y.max = Math.max.apply(this, valueY) + 20;
      this.myChart.options.scales.y.min = Math.min.apply(this, valueY) - 20;
      this.myChart.config.data = data2;
      this.myChart.update()
    },
  }
}
</script>

<style>
  .size{
    max-height: 300px;
    max-width: 500px;
  }
</style>
