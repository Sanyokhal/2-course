import {createRouter, createWebHashHistory} from "vue-router";
import Employees_List from "@/components/Employees_List.vue";
import Employee_Search from "@/components/Employee_Search.vue";
export default createRouter({
    history:createWebHashHistory(),
    routes: [
        {path: '/', component: Employees_List},
        {path: '/search', component: Employee_Search}
    ]
})