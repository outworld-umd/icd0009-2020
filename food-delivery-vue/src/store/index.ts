import Vue from 'vue'
import Vuex from 'vuex'
import { Locales } from "@/i18n/locales";
import createPersistedState from 'vuex-persistedstate';
import { defaultLocale } from "@/i18n";
import { Module, VuexModule, Mutation } from 'vuex-module-decorators'

Vue.use(Vuex)

@Module({ name: 'user' })
export class UserModule extends VuexModule {
    jwt: string | null = null;
    role: string | null = null;
    lang: Locales = defaultLocale

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
}

export default new Vuex.Store({
    state: {},
    getters: {},
    mutations: {},
    modules: {
        user: UserModule
    },
    plugins: [createPersistedState({
        paths: ['user']
    })]
})
