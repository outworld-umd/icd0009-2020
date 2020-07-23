<template>
    <div>
        <table class="container table table-borderless">
            <tr class="h5">
                <th>{{ $t('address.nameSmall') }}</th>
                <th>{{ $t('address.address') }}</th>
                <th>{{ $t('address.apartment') }}</th>
                <th>{{ $t('address.comment') }}</th>
                <th></th>
            </tr>
            <tr v-for="address in addresses" :key="address.id" class="font-weight-lighter">
                <td>{{ address.name }}</td>
                <td>{{ `${address.street} ${address.buildingNumber}, ${address.city}, ${address.county}` }}</td>
                <td>{{ address.apartment }}</td>
                <td class="font-italic" style="width: 25%">{{ address.comment }}</td>
                <td :class="{ invisible: input }">
                    <button class="btn btn-info mr-2 btn-circle btn-circle-sm rounded-circle text-white shadow-none" @click="inputMode(true, address)"><span class="fa fa-pencil"></span></button>
                    <button class="btn btn-danger btn-circle btn-circle-sm rounded-circle text-white shadow-none"  @click="deleteAddress(address.id)"><span class="fa fa-trash"></span></button>
                </td>
            </tr>
        </table>
        <button :class="{ invisible: input }" class="btn btn-success btn-circle btn-circle-sm text-white shadow-none px-3" @click="inputMode(true, null)"><span class="fa fa-plus mr-2"></span>Add</button>
        <transition name="fade">
        <div v-if="input">
            <div class="d-flex">
                <label class="container w-25 text-left">
                    <span class="my-2 font-italic font-weight-light small required">{{ $t('address.name') }}</span>
                    <input type="text" class="form-control font-weight-light shadow-none" v-model="name" maxlength="100">
                </label>
                <label class="container w-50 text-left">
                    <span class="my-2 font-italic font-weight-light small required">{{ $t('address.street') }}</span>
                    <input type="text" class="form-control font-weight-light shadow-none" v-model="street" maxlength="100">
                </label>
                <div class="w-25 d-flex text-left">
                    <label class="container">
                        <span class="my-2 font-italic font-weight-light small required">{{ $t('address.buildingNumber') }}</span>
                        <input type="text" class="form-control font-weight-light shadow-none" v-model="buildingNumber" maxlength="10">
                    </label>
                    <label class="container">
                        <span class="my-2 font-italic font-weight-light small">{{ $t('address.apartment') }}</span>
                        <input type="text" class="form-control font-weight-light shadow-none" v-model="apartment" maxlength="4">
                    </label>
                </div>
            </div>
            <div class="d-flex text-left">
                <label class="container">
                    <span class="my-2 font-italic font-weight-light small required">{{ $t('address.city') }}</span>
                    <input type="text" class="form-control font-weight-light shadow-none"  v-model="city" maxlength="100">
                </label>
                <label class="container">
                    <span class="my-2 font-italic font-weight-light small required">{{ $t('address.county') }}</span>
                    <input type="text" class="form-control font-weight-light shadow-none" v-model="county" maxlength="100">
                </label>
            </div>
            <label class="container text-left">
                <span class="my-2 font-italic font-weight-light small">{{ $t('address.comment') }}</span>
                <input type="text" class="form-control font-weight-light shadow-none" v-model="comment" maxlength="100">
            </label>
            <div class="container w-50 my-4">
                <button type="button" class="btn btn-danger shadow-none font-weight-bold w-25 m-2" @click="inputMode(false, null)">{{ $t('buttons.cancel') }}</button>
                <button type="button" class="btn btn-success shadow-none font-weight-bold w-25 m-2" :disabled="!validateAddress()" @click="submit">{{ $t('buttons.save') }}</button>
            </div>
        </div>
        </transition>
        <CurrentOrder/>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import CurrentOrder from "@/components/CurrentOrder.vue";
import { IAddress, IAddressCreate } from "@/domain/IAddress";
import store from '@/store'

@Component({
    components: { CurrentOrder }
})
export default class AddressIndex extends Vue {
    input = false;
    addressId: string | null = null;
    name: string | null = null;
    street: string | null = null;
    buildingNumber: string | null = null;
    city: string | null = null;
    county: string | null = null;
    comment: string | null = null;
    apartment: string | null = null;

    get addresses(): IAddress[] {
        return store.state.addresses;
    }

    deleteAddress(id: string): void {
        store.dispatch('deleteAddress', id)
    }

    validateAddress(): boolean {
        return !!(this.street && this.buildingNumber && this.city && this.county && this.name)
    }

    submit(): void {
        if (this.street && this.buildingNumber && this.city && this.county && this.name) {
            if (this.addressId) {
                const address: IAddress = {
                    apartment: this.apartment ?? undefined,
                    buildingNumber: this.buildingNumber,
                    city: this.city,
                    comment: this.comment ?? undefined,
                    county: this.county,
                    name: this.name,
                    street: this.street,
                    id: this.addressId
                }
                store.dispatch('editAddress', address);
            } else {
                const address: IAddressCreate = {
                    apartment: this.apartment ?? undefined,
                    buildingNumber: this.buildingNumber,
                    city: this.city,
                    comment: this.comment ?? undefined,
                    county: this.county,
                    name: this.name,
                    street: this.street
                }
                store.dispatch('createAddress', address);
            }
        }
    }

    inputMode(turnOn: boolean, address: IAddress | null) {
        this.input = turnOn;
        this.addressId = address?.id ?? null;
        this.street = address?.street ?? null;
        this.buildingNumber = address?.buildingNumber ?? null;
        this.city = address?.city ?? null;
        this.county = address?.county ?? null;
        this.name = address?.name ?? null;
        this.comment = address?.comment ?? null;
        this.apartment = address?.apartment ?? null;
    }

    mounted(): void {
        store.dispatch('getAddresses');
    }
}
</script>
<style scoped>
    .required:after {
        content:" *";
        color: red;
    }

    .fade-enter-active, .fade-leave-active {
        transition: opacity .5s;
    }
    .fade-enter, .fade-leave-to /* .fade-leave-active below version 2.1.8 */ {
        opacity: 0;
    }
</style>
