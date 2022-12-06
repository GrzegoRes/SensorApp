<template>
  <h1>Home {{id}}</h1>
  <h1>Home {{id}}</h1>
  <h1>Home {{id}}</h1>
</template>

<script>

export default {
  data(){
    return{
      id: this.$route.params.id,
    }
  }

}

</script>