import Vue from 'vue'
import Vuex from 'vuex'
import createPersistedState from 'vuex-persistedstate';
import { IRestaurant, IRestaurantView } from "@/domain/IRestaurant";
import { DayOfWeek } from "@/domain/IWorkingHours";
import { IItem } from "@/domain/IItem";
import UserModule from "@/store/modules/UserModule";
import OrderModule from "@/store/modules/OrderModule";

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        restaurants: [] as IRestaurantView[],
        restaurant: null as IRestaurant | null,
        item: null as IItem | null
    },
    getters: {},
    mutations: {
        setRestaurants(state, restaurants: IRestaurantView[]) {
            state.restaurants = restaurants;
        },
        setRestaurant(state, restaurant: IRestaurant) {
            state.restaurant = restaurant;
        },
        setItem(state, item: IItem) {
            state.item = item;
        }
    },
    actions: {
        async getRestaurants(context): Promise<void> {
            // const restaurants = await RestaurantAPI.getAll();
            const restaurants: IRestaurantView[] = [
                {
                    id: "1",
                    name: "Testing Food",
                    isOpen: true,
                    categories: ["Mexican", "Burgers"],
                    deliveryCost: 2.34
                },
                {
                    id: "2",
                    name: "Not a Restaurant",
                    isOpen: true,
                    categories: ["Sushi", "Burgers"],
                    deliveryCost: 0.36
                },
                {
                    id: "3",
                    name: "Always Closed",
                    isOpen: false,
                    categories: ["Candies"],
                    deliveryCost: 7.34
                }
            ]
            context.commit('setRestaurants', restaurants);
        },
        async getRestaurant(context, id: string): Promise<void> {
            // const restaurant = await RestaurantAPI.get(id);
            const restaurant: IRestaurant = {
                id: "1",
                deliveryCost: 2.34,
                name: "Testing Food",
                itemTypes: [
                    {
                        id: "1",
                        name: "Today's offers",
                        isSpecial: true,
                        description: "Today we have some special offers just for you!",
                        items: [
                            {
                                id: "1",
                                name: "Burger Large Meal",
                                price: 5.20
                            },
                            {
                                id: "2",
                                name: "Caesar Salad",
                                price: 4.00
                            },
                            {
                                id: "3",
                                name: "Happy Meal",
                                price: 5.20
                            }
                        ]
                    },
                    {
                        id: "2",
                        name: "Drinks",
                        isSpecial: false,
                        description: "Let's drink today!",
                        items: [
                            {
                                id: "4",
                                name: "Large Soda",
                                price: 2.00
                            }
                        ]
                    },
                    {
                        id: "3",
                        name: "Other",
                        isSpecial: false,
                        description: "Some other products you might find interesting!",
                        items: [
                            {
                                id: "5",
                                name: "Condoms",
                                price: 9.69
                            },
                            {
                                id: "6",
                                name: "Shaurma",
                                price: 3.50
                            }
                        ]
                    }
                ],
                address: "Akadeemia tee 15, Tallinn",
                phone: "+372 582 69 420",
                workingHours: [
                    {
                        id: "1",
                        weekday: DayOfWeek.MONDAY,
                        openingTime: "10:00",
                        closingTime: "19:00"
                    },
                    {
                        id: "2",
                        weekday: DayOfWeek.TUESDAY,
                        openingTime: "10:00",
                        closingTime: "19:00"
                    }
                ]
            }
            context.commit('setRestaurant', id === "1" ? restaurant : null);
        },
        async getItem(context, id: string): Promise<void> {
            // const item = await ItemAPI.get(id);
            const items: IItem[] = [
                {
                    id: "1",
                    name: "Burger Large Meal",
                    price: 5.20,
                    nutritionInfos: [
                        {
                            id: "1",
                            name: "Sugar",
                            amount: 25,
                            unit: "g"
                        },
                        {
                            id: "2",
                            name: "Calories",
                            amount: 600,
                            unit: "kcal"
                        }
                    ],
                    options: [
                        {
                            id: "1",
                            name: "Fries",
                            isRequired: true,
                            isSingle: true,
                            choices: [
                                {
                                    id: "1",
                                    name: "Small french fries",
                                    additionalPrice: 0
                                },
                                {
                                    id: "2",
                                    name: "Medium french fries",
                                    additionalPrice: 0.2
                                },
                                {
                                    id: "3",
                                    name: "Big french fries",
                                    additionalPrice: 0.5
                                }
                            ]
                        },
                        {
                            id: "2",
                            name: "Drink",
                            isRequired: true,
                            isSingle: true,
                            choices: [
                                {
                                    id: "4",
                                    name: "Coca-Cola",
                                    additionalPrice: 0
                                },
                                {
                                    id: "5",
                                    name: "Coca-Cola Zero",
                                    additionalPrice: 0
                                },
                                {
                                    id: "6",
                                    name: "Coca-Cola Vanilla",
                                    additionalPrice: 0.3
                                }
                            ]
                        },
                        {
                            id: "3",
                            name: "Other",
                            isRequired: false,
                            isSingle: false,
                            choices: [
                                {
                                    id: "7",
                                    name: "Plastic Bag",
                                    additionalPrice: 20
                                },
                                {
                                    id: "8",
                                    name: "Condoms",
                                    additionalPrice: 0.69
                                }
                            ]
                        }
                    ],
                    description: "This Burger large meal is our best!"
                },
                {
                    id: "2",
                    name: "Caesar Salad",
                    price: 4.00,
                    nutritionInfos: [],
                    options: []
                },
                {
                    id: "3",
                    name: "Happy Meal",
                    price: 5.20,
                    nutritionInfos: [],
                    options: []
                }
            ]
            context.commit('setItem', items.find(i => i.id === id) ?? null)
        }
    },
    modules: {
        user: UserModule,
        order: OrderModule
    },
    plugins: [createPersistedState({
        paths: ['user']
    })]
})
