<template>
    <ul class="navbar-nav">
        <template v-if="isAuthenticated">
            <li class="nav-item dropdown mx-3">
                <a class="nav-link dropdown-toggle" data-toggle="dropdown" href="#" role="button" aria-haspopup="true" aria-expanded="false">{{ userEmail }}</a>
                <div class="dropdown-menu">
                    <router-link class="dropdown-item" to="/orders">{{ $t('account.yourOrders') }}</router-link>
                    <router-link class="dropdown-item" to="/addresses">{{ $t('account.yourAddresses') }}</router-link>
                    <div class="dropdown-divider"></div>
                    <a class="dropdown-item" href="#">{{ $t('account.account') }}</a>
                </div>
            </li>
            <li class="nav-item">
                <a @click="logoutOnClick" class="nav-link" href>{{ $t('account.logoutButton') }}</a>
            </li>
        </template>
        <li v-else class="nav-item mx-3">
            <router-link to="/account/login" class="nav-link">{{ $t('account.login') }}</router-link>
        </li>
    </ul>
</template>
<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import store from "../store";
import router from "../router";
import UserModule from "@/store/modules/UserModule";
import { getModule } from "vuex-module-decorators";
import JwtDecode from "jwt-decode";

@Component
export default class Account extends Vue {
    get isAuthenticated(): boolean {
        return getModule(UserModule, store).isAuthenticated;
    }

    get userEmail(): string {
        const jwt = getModule(UserModule, store).jwt
        if (jwt) {
            const decoded = JwtDecode(jwt) as Record<string, string>;
            console.log(decoded)
            return decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
        }
        return 'null';
    }

    logoutOnClick(): void {
        getModule(UserModule, store).clearJwt();
        router.push("/");
    }
}
</script>
