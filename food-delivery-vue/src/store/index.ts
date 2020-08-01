import Vue from 'vue'
import Vuex from 'vuex'
import createPersistedState from 'vuex-persistedstate';
import { IRestaurant, IRestaurantView } from "@/domain/IRestaurant";
import { IItem } from "@/domain/IItem";
import UserModule from "@/store/modules/UserModule";
import OrderModule from "@/store/modules/OrderModule";
import { IAddress, IAddressCreate } from "@/domain/IAddress";
import { IOrder, IOrderView } from "@/domain/IOrder";
import { AddressAPI } from "@/services/AddressAPI";
import { RestaurantAPI } from "@/services/RestaurantAPI";
import { ItemAPI } from "@/services/ItemAPI";
import { OrderAPI } from "@/services/OrderAPI";

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        restaurants: [] as IRestaurantView[],
        restaurant: null as IRestaurant | null,
        item: null as IItem | null,
        addresses: [] as IAddress[],
        orders: [] as IOrderView[],
        order: null as IOrder | null
    },
    getters: {},
    mutations: {
        setOrders(state, orders: IOrderView[]) {
            state.orders = orders;
        },
        setOrder(state, order: IOrder | null) {
            state.order = order;
        },
        setRestaurants(state, restaurants: IRestaurantView[]) {
            state.restaurants = restaurants;
        },
        setRestaurant(state, restaurant: IRestaurant) {
            state.restaurant = restaurant;
        },
        setItem(state, item: IItem) {
            state.item = item;
        },
        setAddresses(state, addresses: IAddress[]) {
            state.addresses = addresses;
        }
    },
    actions: {
        async getOrders(context): Promise<void> {
            const response = await OrderAPI.getAll();
            context.commit('setOrders', response.data)
        },
        async getOrder(context, id: string): Promise<void> {
            const response = await OrderAPI.get(id);
            console.log(response)
            context.commit('setOrder', response.data)
        },
        async getRestaurants(context): Promise<void> {
            const response = await RestaurantAPI.getAll()
            context.commit('setRestaurants', response.data);
        },
        async getRestaurant(context, id: string): Promise<void> {
            const response = await RestaurantAPI.get(id);
            console.log(response.data)
            context.commit('setRestaurant', response.data);
        },
        async getItem(context, id: string): Promise<void> {
            const response = await ItemAPI.get(id);
            context.commit('setItem', response.data)
        },
        async getAddresses(context): Promise<void> {
            const response = await AddressAPI.getAll();
            context.commit('setAddresses', response.data);
        },
        async deleteAddress(context, id: string): Promise<void> {
            await AddressAPI.delete(id);
            await context.dispatch('getAddresses');
        },
        async createAddress(context, address: IAddressCreate): Promise<void> {
            await AddressAPI.post(address);
            await context.dispatch('getAddresses');
        },
        async editAddress(context, address: IAddress): Promise<void> {
            await AddressAPI.put(address.id, address);
            await context.dispatch('getAddresses');
        }
    },
    modules: {
        user: UserModule,
        currentOrder: OrderModule
    },
    plugins: [createPersistedState({
        paths: ['user']
    })]
})
