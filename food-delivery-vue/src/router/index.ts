import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import RestaurantIndex from '@/views/restaurants/Index.vue'
import RestaurantMenu from '@/views/restaurants/Menu.vue'
import OrderIndex from '@/views/orders/Index.vue'
import OrderCreate from "@/views/orders/Сreate.vue";
import AccountLogin from "@/views/account/Login.vue";
import AddressIndex from "@/views/addresses/Index.vue";

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
    {
        path: '/restaurants',
        name: 'Restaurants',
        component: RestaurantIndex
    },
    {
        path: '/restaurants/:id/menu',
        name: 'Restaurant',
        component: RestaurantMenu,
        props: true
    },
    {
        path: '/orders',
        name: 'Orders',
        component: OrderIndex
    },
    {
        path: '/orders/new',
        name: 'New Order',
        component: OrderCreate
    },
    {
        path: '/addresses',
        name: 'Addresses',
        component: AddressIndex
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

export default router
