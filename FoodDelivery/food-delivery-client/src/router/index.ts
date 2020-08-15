import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import RestaurantIndex from '@/views/restaurants/Index.vue'
import RestaurantMenu from '@/views/restaurants/Menu.vue'
import OrderIndex from '@/views/orders/Index.vue'
import OrderCreate from "@/views/orders/Сreate.vue";
import AccountLogin from "@/views/account/Login.vue";
import AddressIndex from "@/views/addresses/Index.vue";
import { getModule } from "vuex-module-decorators";
import store from '@/store'
import UserModule from "@/store/modules/UserModule";
import AccountRegister from "@/views/account/Register.vue";

Vue.use(VueRouter)

const appName = "Colt Food Client"

const routes: Array<RouteConfig> = [
    {
        path: '/restaurants',
        name: 'Restaurants',
        component: RestaurantIndex,
        meta: {
            requiresAuth: true,
            title: "Restaurants"
        }
    },
    {
        path: '/restaurants/:id/menu',
        name: 'Restaurant',
        component: RestaurantMenu,
        props: true,
        meta: {
            requiresAuth: true,
            title: "Menu"
        }
    },
    {
        path: '/orders',
        name: 'Orders',
        component: OrderIndex,
        meta: {
            requiresAuth: true,
            title: "Orders"
        }
    },
    {
        path: '/orders/new',
        name: 'New Order',
        component: OrderCreate,
        meta: {
            requiresAuth: true,
            title: "New Order"
        }
    },
    {
        path: '/addresses',
        name: 'Addresses',
        component: AddressIndex,
        meta: {
            requiresAuth: true,
            title: "Addresses"
        }
    },
    {
        path: '/account/login',
        name: 'AccountLogin',
        component: AccountLogin,
        meta: {
            title: "Login"
        }
    },
    {
        path: '/account/register',
        name: 'AccountRegister',
        component: AccountRegister,
        meta: {
            title: "Register"
        }
    },
    {
        path: '*'
    }
]

const router = new VueRouter({
    routes
})

router.beforeEach((to, from, next) => {
    if (to.matched.some(record => record.meta.requiresAuth)) {
        if (getModule(UserModule, store).jwt == null) {
            next('/account/login')
        } else next()
    } else next()
    document.title = (to.meta.title ? `${to.meta.title} | ` : "") + appName;
})

export default router
