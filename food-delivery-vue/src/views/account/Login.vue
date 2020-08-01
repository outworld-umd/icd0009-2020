<template>
    <div class="container w-25">
        <div>
            <h3 class="font-weight-light text-danger mt-5" :class="{ invisible: !(loginWasOk === false) }">{{ $t('account.badLogin') }}</h3>
            <div class="text-left">
                <label class="control-label w-100 form-group font-weight-light font-italic">
                    {{ $t('account.email') }}
                    <input v-model="loginInfo.email" class="form-control font-weight-light" type="email" :placeholder="$t('account.email')"/>
                </label>
                <label class="control-label w-100 form-group font-weight-light font-italic">
                    {{ $t('account.password') }}
                    <input v-model="loginInfo.password" class="form-control font-weight-light" type="password" :placeholder="$t('account.password')"/>
                </label>
            </div>
            <div class="form-group mt-5">
                <button @click="loginOnClick" class="btn px-4 btn-success font-weight-bold shadow-none" :disabled="!this.loginInfo.email.length || !this.loginInfo.password.length">{{ $t('account.login') }}</button>
            </div>
            <router-link to="/account/register" class="nav-link">{{ $t('account.dontHaveAccount') }}</router-link>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import store from "../../store";
import router from "../../router";
import { ILoginRequest } from "@/domain/identity/ILoginRequest";
import { getModule } from "vuex-module-decorators";
import UserModule from "@/store/modules/UserModule";

@Component
export default class AccountLogin extends Vue {
    private loginInfo: ILoginRequest = {
        email: "",
        password: ""
    };

    private loginWasOk: boolean | null = null;

    loginOnClick(): void {
        if (this.loginInfo.email.length > 0 && this.loginInfo.password.length > 0) {
            getModule(UserModule, store).authenticateUser(this.loginInfo).then((isLoggedIn: boolean) => {
                if (isLoggedIn) {
                    this.loginWasOk = true;
                    router.push("/");
                } else {
                    this.loginWasOk = false;
                }
            });
        }
    }
}
</script>
