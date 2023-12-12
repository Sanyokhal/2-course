<template>
<div class="list">
  <div class="list-item" v-for="item in this.$store.state.employees" :key="item.passport.series">
    <h3>{{item.passport.name}} {{item.passport.surname}} {{item.passport.series}}</h3>
    <p>Освіта: {{item.education}}</p>
    <p>Спеціальність: {{item.speciality}}</p>
    <p>Посада: {{item.position}}</p>
    <p>Оклад: {{item.revenue}}₴</p>
  </div>
</div>
</template>

<script>
export default {
  name: "Employees_List"
}
</script>

<style>
.list{
  display: flex;
  flex-direction: row;
  width: 90vw;
  margin-left: 5vw;
  align-items: center;
  gap: 20px;
  justify-content: space-between;
  flex-wrap: wrap;
}
</style>