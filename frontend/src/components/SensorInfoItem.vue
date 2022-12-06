<template>
    <div class="item shadow1">
        <div>Dane sensora</div>
        <div>NAME: {{item.name}}</div>
        <div>TYPE: {{item.type}}</div>
        <div>AVG: {{item.aveValueLastHundred.toFixed(2)}}</div>
        <div>LAST VALUE: {{item.lastValue}}</div>
        <button @click="$router.push({name:'sensorId',params: { id: item.name }})">Details</button>
    </div>
</template>

<script>
export default {
    props: ['item'],

    methods: {
        viewDetails(id){
            alert(id);
        }
    }
}
</script>

<style>
  .item {
    border: 1px;
    border-block-style: solid;
    border-block-color: #fff;
    border-radius: 20px;
    margin: 8px;
    padding: 10px;
    background: #fff;
  }

  .shadow1{
    box-shadow: 0 5px 10px rgba(154,160,185,.05), 0 15px 40px rgba(166,173,201,.2);
  }
</style>
