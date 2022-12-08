<template>
  <div>
    <h2>Pulpit sensor</h2>
    <div class = "wrapper">
        <SensorInfoItem 
            v-for="item in list" 
            :key="item.id" 
            :item= "item"
            />
    </div>
  </div>
</template>

<script>
import SensorInfoItem from "./SensorInfoItem.vue"
import * as signalR from "@aspnet/signalr"
import axios from 'axios'

export default {
  name: 'App',
  components: {
    SensorInfoItem
  },

  data() {
    return{
      test: "",
      connection: null,
      zmienna: "1",
      list:[],
      item: 1
    }
  },
  
  created: function ()
  {
    this.connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:44335/stream", {
          skipNegotiation: true,
          transport: signalR.HttpTransportType.WebSockets})
        .build();
  },

  async mounted() {
    let result = await axios.get("https://localhost:44335/Last", { crossdomain: true });
    this.list= result.data;

    await this.connection.start();
    this.connection.on("streaming");
    this.connection.stream('streaming').subscribe({next: (item) => this.list= item})
  
  },

  methods: {
    update(){
        const itemIndex = this.list.findIndex(x => x.sensorID === Number(this.test))
        this.list[itemIndex].lastValue = 20;
    }
  }
};
</script>

<style>
  .wrapper{
    background-color: #fff;
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
    grid-gap: 5px;
  }
</style>
