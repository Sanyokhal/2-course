<template>
  <div class="comp">
    <div class="search-group">
      <input type="text" placeholder="Пошук" v-model="search_val">
      <select v-model="selector">
        <option value="name">Іменем</option>
        <option value="surname">Прізвищем</option>
        <option value="passport">Номером паспорта</option>
        <option value="education">Освітою</option>
        <option value="position">Посадою</option>
        <option value="revenue">Окладом</option>
      </select>
      <button @click="search()">Знайти</button>
    </div>
    <h2>Список знайдених працівників</h2>
    <div class="list" v-if="employees.length > 0">
      <div class="list-item" v-for="item in employees" :key="item.passport.series">
        <h3>{{ item.passport.name }} {{ item.passport.surname }} {{ item.passport.series }}</h3>
        <p>Освіта: {{ item.education }}</p>
        <p>Спеціальність: {{ item.speciality }}</p>
        <p>Посада: {{ item.position }}</p>
        <p>Оклад: {{ item.revenue }}₴</p>
      </div>
    </div>
    <h2 v-else>Такого працівника не знайдено</h2>
  </div>
</template>

<script>
export default {
  name: "Employee_Search",
  data() {
    return {
      selector: 'name',
      search_val: '',
      employees: []
    }
  },
  mounted() {
    this.search();
  },
  methods: {
    search() {
      this.employees = this.$store.state.employees.filter((employee) => {
        return employee.passport.name == this.search_val || employee.passport.surname == this.search_val ||
            employee.passport.series.includes(this.search_val) || employee.education == this.search_val || employee.position == this.search_val || employee.revenue >= this.search_val
      })
    }
  }
}
</script>

<style>

</style>