import Vue from 'vue'
import Vuex from 'vuex'
import { Locales } from "@/i18n/locales";
import createPersistedState from 'vuex-persistedstate';
import { defaultLocale } from "@/i18n";
import { Module, VuexModule, Mutation, Action } from 'vuex-module-decorators'
import { ILoginRequest } from "@/domain/identity/ILoginRequest";
import { AccountAPI } from "@/services/AccountAPI";
import { IRestaurant, IRestaurantView } from "@/domain/IRestaurant";
import { DayOfWeek } from "@/domain/IWorkingHours";
import { IOrderRowTemp } from "@/domain/IOrderRow";
import { IItem } from "@/domain/IItem";

Vue.use(Vuex)

@Module({ name: 'user' })
export class UserModule extends VuexModule {
    jwt: string | null = null;
    role: string | null = null;
    lang: Locales = defaultLocale;

    get isAuthenticated(): boolean {
        return this.jwt !== null;
    }

    @Mutation
    setLanguage(lang: Locales) {
        this.lang = lang
    }

    @Mutation
    setJwt(jwt: string) {
        this.jwt = jwt;
    }

    @Mutation
    setRole(role: string) {
        this.role = role;
    }

    @Mutation
    clearJwt(): void {
        this.jwt = null;
    };

    @Action
    async authenticateUser(loginDTO: ILoginRequest): Promise<boolean> {
        const jwt = await AccountAPI.login(loginDTO);
        this.context.commit('setJwt', jwt);
        return jwt !== null;
    }
}

export default new Vuex.Store({
    state: {
        restaurants: [] as IRestaurantView[],
        restaurant: null as IRestaurant | null,
        currentRestaurantId: null as string | null,
        currentRestaurantName: null as string | null,
        item: null as IItem | null,
        deliveryCost: 0 as number,
        orderRows: [] as IOrderRowTemp[]
    },
    getters: {
        orderHasItems(state): boolean {
            return state.orderRows.length !== 0;
        },
        foodCost(state): number {
            return state.orderRows.reduce(
                (a: number, b: IOrderRowTemp) => a + b.amount * b.cost + b.choices.reduce(
                    (c, d) => c + d.amount * d.cost, 0), 0);
        },
        amountOfItem: (state) => (id: string) => {
            return state.orderRows.filter((r: IOrderRowTemp) => r.itemId === id).reduce((a: number, b: IOrderRowTemp) => a + b.amount, 0)
        },
        totalAmount(state): number {
            return state.orderRows.reduce((a: number, b: IOrderRowTemp) => a + b.amount, 0)
        }
    },
    mutations: {
        setRestaurants(state, restaurants: IRestaurantView[]) {
            state.restaurants = restaurants;
        },
        setRestaurant(state, restaurant: IRestaurant) {
            state.restaurant = restaurant;
        },
        setCurrentRestaurantId(state, id: string) {
            state.currentRestaurantId = id;
        },
        setCurrentRestaurantName(state, name: string) {
            state.currentRestaurantName = name;
        },
        setDeliveryCost(state, cost: number) {
            state.deliveryCost = cost;
        },
        clearOrders(state) {
            state.orderRows = [];
        },
        deleteFromOrder(state, id: string) {
            state.orderRows = state.orderRows.filter((o: IOrderRowTemp) => o.itemId !== id);
        },
        setItem(state, item: IItem) {
            state.item = item;
        },
        addOrderRow(state, orderRowTemp: IOrderRowTemp) {
            if (orderRowTemp.amount) {
                state.orderRows.push(orderRowTemp)
            }
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
                    deliveryCost: 0.15
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
            // const restaurant = RestaurantAPI.get(id);
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
            // const item = ItemAPI.get(id);
            const items: IItem[] = [
                {
                    id: "1",
                    name: "Burger Large Meal",
                    price: 5.20,
                    nutritionInfos: [],
                    options: [],
                    description: "Ох ебать как вкусно охуеть не встать =DDD"
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
        user: UserModule
    },
    plugins: [createPersistedState({
        paths: ['user']
    })]
})
