<template>
    <div class="container w-50">
        <div>
            <h4 class="font-weight-light text-danger mt-5" :class="{ invisible: !(registerWasOk === false) }">{{ $t('account.badLogin') }}</h4>
            <h5 class="font-weight-light text-danger mt-5" :class="{ invisible: validatePassword() }">{{ $t('account.passwordMatch') }}</h5>
            <div class="text-left">
                <div class="d-flex">
                    <label class="control-label w-100 form-group font-weight-light font-italic m-2">
                        {{ $t('account.firstName') }}
                        <input v-model="registerInfo.firstName" class="form-control font-weight-light" type="text" :placeholder="$t('account.firstName')"/>
                    </label>
                    <label class="control-label w-100 form-group font-weight-light font-italic m-2">
                        {{ $t('account.lastName') }}
                        <input v-model="registerInfo.lastName" class="form-control font-weight-light" type="text" :placeholder="$t('account.lastName')"/>
                    </label>
                </div>
                <div class="d-flex">
                    <label class="control-label w-100 form-group font-weight-light font-italic m-2">
                        {{ $t('account.email') }}
                        <input v-model="registerInfo.email" class="form-control font-weight-light" type="email" :placeholder="$t('account.email')"/>
                    </label>
                    <label class="control-label w-100 form-group font-weight-light font-italic m-2">
                        {{ $t('account.phone') }}
                        <input v-model="registerInfo.phone" class="form-control font-weight-light" type="text" :placeholder="$t('account.phone')"/>
                    </label>
                </div>
                <div class="d-flex">
                    <label class="control-label w-100 form-group font-weight-light font-italic m-2">
                        {{ $t('account.password') }}
                        <input v-model="registerInfo.password" class="form-control font-weight-light" type="password" :placeholder="$t('account.password')"/>
                    </label>
                    <label class="control-label w-100 form-group font-weight-light font-italic m-2">
                        {{ $t('account.confirmPassword') }}
                        <input v-model="confirmPassword" class="form-control font-weight-light" type="password" :placeholder="$t('account.confirmPassword')"/>
                    </label>
                </div>
            </div>
            <div class="form-group mt-5">
                <button @click="registerOnClick" class="btn px-4 btn-success font-weight-bold shadow-none" :disabled="!validate()">{{ $t('account.register') }}</button>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import store from "../../store";
import router from "../../router";
import { getModule } from "vuex-module-decorators";
import UserModule from "@/store/modules/UserModule";
import { IRegisterRequest } from "@/domain/identity/IRegisterRequest";

@Component
export default class AccountRegister extends Vue {
    private registerInfo: IRegisterRequest = {
        email: "",
        password: "",
        firstName: "",
        lastName: "",
        phone: "",
        roles: ["Customer"]
    };

    private confirmPassword = "";
    private registerWasOk: boolean | null = null;

    validate(): boolean {
        return this.registerInfo.email.length > 0 && this.registerInfo.phone.length > 0 &&
            this.registerInfo.firstName.length > 0 && this.registerInfo.lastName.length > 0 &&
            this.registerInfo.password.length > 0 && this.confirmPassword.length > 0 && this.validatePassword();
    }

    validatePassword(): boolean {
        return this.confirmPassword.length === 0 || this.confirmPassword === this.registerInfo.password
    }

    registerOnClick(): void {
        if (this.validate()) {
            getModule(UserModule, store).registerUser(this.registerInfo).then((isLoggedIn: boolean) => {
                if (isLoggedIn) {
                    this.registerWasOk = true;
                    router.push("/");
                } else {
                    this.registerWasOk = false;
                }
            });
        }
    }
}
</script>
