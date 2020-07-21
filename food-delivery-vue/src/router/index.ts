import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import RestaurantIndex from '@/views/restaurants/Index.vue'
import RestaurantMenu from '@/views/restaurants/Menu.vue'
import OrderIndex from '@/views/orders/OrderIndex.vue'

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
    {
        path: '/',
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
    }
]

const router = new VueRouter({
    routes
})

export default router
