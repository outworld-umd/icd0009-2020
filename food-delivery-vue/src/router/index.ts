import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import RestaurantIndex from '@/views/restaurants/Index.vue'
import RestaurantMenu from '@/views/restaurants/Menu.vue'

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
    {
        path: '/',
        name: 'Restaurants',
        component: RestaurantIndex
    },
    {
        path: '/restaurants/:id',
        name: 'Restaurant',
        component: RestaurantMenu,
        props: true
    },
    {
        path: '/about',
        name: 'About',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
    }
]

const router = new VueRouter({
    routes
})

export default router
