import Vue from 'vue'
import Vuex from 'vuex'
import { Locales } from "@/i18n/locales";
import createPersistedState from 'vuex-persistedstate';
import { defaultLocale } from "@/i18n";
import {Module, VuexModule, Mutation, Action} from 'vuex-module-decorators'
import {ILoginRequest} from "@/domain/identity/ILoginRequest";
import { AccountAPI } from "@/services/AccountAPI";

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
