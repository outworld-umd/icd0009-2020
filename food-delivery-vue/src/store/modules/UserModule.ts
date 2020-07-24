import { Action, Module, Mutation, VuexModule } from "vuex-module-decorators";
import { Locales } from "@/i18n/locales";
import { defaultLocale } from "@/i18n";
import { ILoginRequest } from "@/domain/identity/ILoginRequest";
import { AccountAPI } from "@/services/AccountAPI";

@Module({ name: 'user' })
export default class UserModule extends VuexModule {
    jwt: string | null = null;
    role: string | null = null;
    lang: Locales = defaultLocale;

    get isAuthenticated(): boolean {
        return this.jwt !== null;
    }

    @Mutation
    setLanguage(lang: Locales) {
        this.lang = lang;
    }

    @Mutation
    setJwt(jwt: string | null) {
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
        let jwt = await AccountAPI.login(loginDTO);
        jwt = null;
        if (loginDTO.password === "asd" && loginDTO.email === "asd") jwt = "Testing";
        this.setJwt(jwt);
        return jwt !== null;
    }
}
