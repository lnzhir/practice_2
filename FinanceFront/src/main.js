import { createApp } from 'vue';
import './style.css';
import App from './App.vue';
import createStore from './store/index.js';






const app = createApp(App);
//app.config.globalProperties.$rest = rest;

const store = createStore(app.config.globalProperties);

app.use(store);

app.mount('#app')
