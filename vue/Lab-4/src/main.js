import { createApp } from 'vue'
import App from './App.vue'
import store from "@/assets/store";
import router from "@/assets/router";

createApp(App).use(store).use(router).mount('#app')
