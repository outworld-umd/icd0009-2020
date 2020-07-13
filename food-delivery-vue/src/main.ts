import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store, { UserModule } from './store'
import { getModule } from 'vuex-module-decorators'
import VueI18n from 'vue-i18n';
import { messages, defaultLocale } from '@/i18n';
import 'jquery';
import 'popper.js';
import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'font-awesome/css/font-awesome.min.css';

Vue.config.productionTip = false

Vue.use(VueI18n)

const i18n = new VueI18n({
    messages,
    locale: getModule(UserModule, store).lang,
    fallbackLocale: defaultLocale
});

new Vue({
    i18n,
    router,
    store,
    render: h => h(App)
}).$mount('#app')
