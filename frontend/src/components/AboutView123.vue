<template>
  <div>
  <h1>Tabele</h1>
  <button @click="viewOption">Option Search</button>
  <div v-if="see">
    <p>Name: <input type="text" v-model="name123" placeholder = "Name"></p>
    <p>Value: <input type="number" v-model="value_from" placeholder = "od">
       <input type="number" v-model="value_to" placeholder = "do"></p>
    <p>Type:<select v-model="selected">
            <option>TEMPERATURE</option>
            <option>HUMIDITY</option>
            <option>LIGHT</option>
            <option>CARBON_DIOXIDE</option></select></p>
    <p>Data: <input type="datetime-local" v-model="date_from2"> 
            <input type="datetime-local" v-model="date_to"></p>
    <p>SortBy:<select v-model="sortBy">
            <option>Name</option>
            <option>Value</option>
            <option>Data</option>
            <option>Type</option></select>
            </p>
    <input type="radio" v-model="type" value="ascending">ascending
    <input type="radio" v-model="type" value="descending">descending
    <button @click="update">Search</button>
    <button @click="clear">Clear</button>
  </div>
    
  <table class= "table">
    <thead>
      <tr>
        <th>name</th>
        <th>type</th>
        <th>value</th>
        <th>unit</th>
        <th>dateGenerate</th>
        <th>timeGenerate</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="item in list" :key="item.id" :item= "item">
         <td>{{item.name}}</td>
         <td>{{item.type}}</td>
         <td>{{item.value}}</td>
         <td>{{item.unit}}</td>
         <td>{{item.dateGenerate}}</td>
         <td>{{item.timeGenerate}}</td>
      </tr>
    </tbody>
  </table>
  </div>
</template>

<script>
import axios from 'axios'

export default {

  data() {
    return{
      see: false,
      selected: null,
      name123: null,
      value_to: null, 
      value_from: null,
      date_from2: null, 
      date_to: null,
      type: null,
      sortBy: null,
      test: "Test1",
      list:[],
    }
  },

  async mounted() {
      let result = await axios.get("http://localhost:1000/GetAll");
      this.list= result.data;
    },
  methods: {
    async update() 
    {
        let link = "http://localhost:1000/GetAll2";
        var list2 = await axios.post(link, 
        {
           name:this.name123,
           value_to: this.value_to,
           value_from: this.value_from,
           type: this.selected,
           sortBy: this.sortBy,
           typSort: this.type,

           date_from: this.date_from2,
           date_to: this.date_to
        })
        this.list = list2.data;
    },

    clear() 
    {
       this.name123 = null,
       this.value_to = null,
       this.value_from = null,
       this.selected = null,
       
       this.type = null,
       this.sortBy = null,

       this.date_from2 = null,
       this.date_to = null
    },

    viewOption()
    {
        if(this.see === true){
          this.see = false;
        }
        else{
          this.see = true;
        }
    }
  }
}
</script>

<style>
  .table {
    border-collapse: collapse;
  }

  .table thead{
    background-color: rgb(163, 163, 163);
  }

  .table th, td {
  padding: 15px;
  text-align: left;
  border-bottom: 1px solid #ddd;
  border-left: 1px solid #ddd;
  border-right: 1px solid #ddd;
  }

  .table tr:nth-child(even){
    background-color: rgb(240, 240, 240);
  }
</style>