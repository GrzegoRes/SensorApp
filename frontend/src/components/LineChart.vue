<template>
  <div class = "size">
    <h1> {{name}} </h1>
    <canvas id="myChart" width="100" height="100"> </canvas>
  </div>
</template>

<script>
import Chart from 'chart.js/auto';

export default {
  name: "HelloWorld",
  props:{
    msg: String
  },
  mounted(){
    console.log("Component meounted.")
    const ctx = document.getElementById('myChart');

    const labels = [-10,10,1,2,3,4,5,6,7,8,9, 10,11,12,13,14,15];
    const data = {
    labels: labels,
    datasets: [{
      label: 'My First Dataset',
      data: [65,1,23,4, 59, 80, 81, 56, 55, 40],
      fill: false,
      borderColor: 'rgb(75, 192, 192)',
      tension: 0.3
  }]};

  const myChart = new Chart(ctx, {
    type: 'line',
    data: data,
    //tu juz nadto
    options: {
    plugins: {
      title: {
        text: 'Chart.js Time Scale',
        display: true
      }
    },

    scales: {

      y: {
        title: {
          display: true,
          text: 'value',
        },
          min: 0,
          max: 100,
      }
    }
  }});

  myChart;
  }
}
</script>

<style>
  .size{
    max-height: 300px;
    max-width: 500px;
  }
</style>
