<template>
  <div class = "contener">
  <div class = "options">
  <div>
  <h1>Tabele</h1>
  <div>
    <button @click="downloadFile()">Save data</button>
    Format:<select v-model="typeDonwload">
            <option>CSV</option>
            <option>JSON</option>
            </select>
  </div>
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
  <p>Size page:<select @change="myCallback()" v-model="perPage" >
        <option>10</option>
        <option>25</option>
        <option>50</option>
        <option>100</option></select></p>
  </div> 
  </div> 
  <div class="divTabl">
  
  <table class= "table">
    <thead>
      <tr>
        <th>name</th>
        <th>type</th>
        <th>value</th>
        <th>unit</th>
        <th>dateGenerate</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="item in list_view" :key="item.id" :item= "item">
         <td>{{item.name}}</td>
         <td>{{item.type}}</td>
         <td>{{item.value}}</td>
         <td>{{item.unit}}</td>
         <td>{{item.dateGenerate.replace('T',' ').replace('Z','')}}</td>
      </tr>
    </tbody>
  </table>
      <pagination class="list-layout" v-model="page" :records="list.length" :per-page="perPage" @paginate="myCallback"/>
  </div>
  </div>
</template>

<script>
import axios from 'axios'
import exportFromJSON from "export-from-json";
import Pagination from 'v-pagination-3';
import MyPagination from './Pagination.vue'

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
      list: [],
      list_view: [],
      list3: [],
      typeDonwload: "JSON",
      page: 1,
      perPage: 10,
    }
  },
  options: {
        template: MyPagination    
    },   
  components: {
    'pagination': Pagination 
  },
  
  async mounted() {
      let result = await axios.get("https://localhost:44335/GetAll");
      this.list= result.data;
      this.myCallback();
    },
  methods: {
    myCallback(){
      this.list_view = this.list.slice((this.page-1)*this.perPage,this.page*this.perPage)
    },

    downloadFile() {
      const data = this.list;
      const fileName = "data";
      
      var exportType = exportFromJSON.types.json;
      
      if(this.typeDonwload === "CSV"){
          exportType = exportFromJSON.types.csv;
      }

      if (data) exportFromJSON({ data, fileName, exportType });
    },

    async update() 
    {
        let link = "https://localhost:44335/GetAll2";
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
        this.list3 = JSON.stringify(this.list);
        this.myCallback();
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
  width: 150px;
  padding: 15px;
  text-align: left;
  border-bottom: 1px solid #ddd;
  border-left: 1px solid #ddd;
  border-right: 1px solid #ddd;
  }

  .table tr:nth-child(even){
    background-color: rgb(240, 240, 240);
  }

  .options{
    width: 66%;
    text-align: left;
  }

</style>