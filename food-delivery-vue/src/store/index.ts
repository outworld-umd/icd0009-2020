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
        restaurant: null as IRestaurant | null
    },
    getters: {},
    mutations: {
        setRestaurants(state, restaurants: IRestaurantView[]) {
            state.restaurants = restaurants;
        },
        setRestaurant(state, restaurant: IRestaurant) {
            state.restaurant = restaurant;
        }
    },
    actions: {
        async getRestaurants(context): Promise<void> {
            // const restaurants = await RestaurantAPI.getAll();
            const restaurants: IRestaurantView[] = [
                {
                    id: "1",
                    name: "Restaurant 1",
                    isOpen: true,
                    categories: ["Mexican", "Burgers"],
                    deliveryCost: 2.34
                },
                {
                    id: "2",
                    name: "Restaurant 2",
                    isOpen: true,
                    categories: ["Sushi", "Burgers"],
                    deliveryCost: 0.15
                },
                {
                    id: "3",
                    name: "Restaurant 3",
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
                name: "Restaurant 1",
                itemTypes: [
                    {
                        id: "1",
                        name: "Today's offers",
                        isSpecial: true,
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
                        name: "Other",
                        isSpecial: false,
                        items: [
                            {
                                id: "4",
                                name: "Large Soda",
                                price: 2.00
                            },
                            {
                                id: "2",
                                name: "Condoms",
                                price: 9.69
                            },
                            {
                                id: "3",
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
        }
    },
    modules: {
        user: UserModule
    },
    plugins: [createPersistedState({
        paths: ['user']
    })]
})
