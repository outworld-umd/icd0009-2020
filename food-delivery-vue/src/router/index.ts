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

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
    {
        path: '/restaurants',
        name: 'Restaurants',
        component: RestaurantIndex,
        meta: {
            requiresAuth: true
        }
    },
    {
        path: '/restaurants/:id/menu',
        name: 'Restaurant',
        component: RestaurantMenu,
        props: true,
        meta: {
            requiresAuth: true
        }
    },
    {
        path: '/orders',
        name: 'Orders',
        component: OrderIndex,
        meta: {
            requiresAuth: true
        }
    },
    {
        path: '/orders/new',
        name: 'New Order',
        component: OrderCreate,
        meta: {
            requiresAuth: true
        }
    },
    {
        path: '/addresses',
        name: 'Addresses',
        component: AddressIndex,
        meta: {
            requiresAuth: true
        }
    },
    {
        path: '/account/login',
        name: 'AccountLogin',
        component: AccountLogin
    }
]

const router = new VueRouter({
    routes
})

router.beforeEach((to, from, next) => {
    if (to.matched.some(record => record.meta.requiresAuth)) {
        if (getModule(UserModule, store).jwt == null) {
            getModule(UserModule, store).setJwt("testing");
            next('/account/login')
        } else next()
    } else next()
})

export default router
