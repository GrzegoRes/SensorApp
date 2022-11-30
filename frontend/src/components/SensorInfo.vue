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
    <p> ID: {{test}}</p>
    <input type="text" placeholder="todo" v-model="test"/>
    <button @click="update">TATA</button>
    <p>
        {{zmienna}}
    </p>
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
        .withUrl("http://localhost:1000/stream", {
          skipNegotiation: true,
          transport: signalR.HttpTransportType.WebSockets})
        .build();
  },

  async mounted() {
    let result = await axios.get("http://localhost:1000/Last", { crossdomain: true });
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
