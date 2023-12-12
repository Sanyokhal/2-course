<template>
  <div class="links">
    <router-link to="/">Список працівників</router-link>
    <router-link to="/search">Пошук працівника</router-link>
  </div>
  <router-view></router-view>
</template>

<script>

export default {
  name: 'App',
  components: {},
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  padding: 0;
  margin: 0;
}

.links {
  margin-bottom: 20px;
  display: flex;
  flex-direction: row;
  gap: 20px;
  align-items: center;
  justify-content: center;
}
</style>
