<template>
    <div class="container text-left">
        <div v-if="restaurantName">
            <div class="m-4">
                <h5>{{ $t('order.newOrderAt') }}:</h5>
                <h2 class="font-weight-bold">{{ restaurantName }}</h2>
            </div>
            <div class="container w-75 ">
                <table class="table mt-5">
                    <tr v-for="(row, i) in orderRows" :key="i">
                        <td class="h5">
                            {{ row.name }} <span style="float: right">{{ row.cost.toFixed(2) }}€</span>
                            <div class="font-weight-light h6 text-secondary">
                                <div v-for="(choice, i) in row.choices" :key="i">
                                    {{ choice.name }}<span v-if="choice.cost" style="float: right">+{{ choice.cost.toFixed(2) }}€</span>
                                </div>
                            </div>
                            <div v-if="row.comment" class="small text-secondary font-weight-light font-italic">"{{ row.comment }}"</div>
                        </td>
                        <td style="width: 10%;" class=" px-4 h3 text-right">{{ rowCost(row).toFixed(2) }}€</td>
                        <td style="width: 25%;">
                            <div class="d-flex justify-content-between align-items-center">
                                <button class="btn btn-secondary btn-circle btn-circle-sm text-white shadow-none" :disabled="row.amount < 2" @click="decrement(row)"><span class="fa fa-minus"></span></button>
                                <div class="h3 text-center">{{ row.amount }}</div>
                                <button class="btn btn-secondary btn-circle btn-circle-sm rounded-circle text-white shadow-none" :disabled="totalAmount > 19" @click="increment(row)"><span class="fa fa-plus"></span></button>
                                <button class="btn btn-danger btn-circle btn-circle-sm rounded-circle text-white shadow-none"  style="float: right" @click="deleteRowFromOrder(row)"><span class="fa fa-trash"></span></button>
                            </div>
                        </td>
                    </tr>
                </table>
                <hr class="mb-5"/>
                <div v-if="orderHasItems">
                    <div class="d-flex bd-highlight">
                        <div class="p-2 flex-fill w-50 pr-4 py-4 bd-highlight">
                            <div class="form-group">
                                <label class="control-label w-75 font-weight-light font-italic">
                                    {{ $t('order.address') }}
                                    <select class="custom-select font-weight-light shadow-none" v-model="address">
                                        <option class="font-weight-light" :value="null" selected disabled>{{ $t('buttons.choose') }}</option>
                                        <option class="font-weight-light" v-for="a in addresses" :value="a" :key="a.id">{{ a.name }}</option>
                                    </select>
                                </label>
                            </div>
                            <div class="form-group">
                                <label class="control-label w-75 font-weight-light font-italic">
                                    {{ $t('order.paymentMethod') }}
                                    <select class="custom-select font-weight-light shadow-none" v-model="paymentMethod">
                                        <option class="font-weight-light" :value="null" selected disabled>{{ $t('buttons.choose') }}</option>
                                        <option class="font-weight-light" :value="cash">{{ $t('order.payCash') }}</option>
                                        <option class="font-weight-light" :value="card">{{ $t('order.payCard') }}</option>
                                    </select>
                                </label>
                            </div>
                        </div>
                        <div class="p-2 flex-fill d-flex w-50 bd-highlight align-items-center">
                            <div class="mr-auto text-left">
                                <h5>{{ $t('order.foodCost') }}</h5>
                                <h5>{{ $t('order.deliveryCost') }}</h5>
                                <h3>{{ $t('order.total') }}</h3>
                            </div>
                            <div class="pl-4 text-right">
                                <h5>{{ foodCost.toFixed(2) }}€</h5>
                                <h5>{{ deliveryCost.toFixed(2) }}€</h5>
                                <h3>{{ orderTotal.toFixed(2) }}€</h3>
                            </div>
                        </div>
                    </div>
                    <div class="pull-right mt-4">
                        <button class="btn btn-danger px-4 font-weight-bold shadow-none" @click="clearOrder">{{ $t('buttons.clear') }}</button>
                        <button :disabled="!address || paymentMethod === null" :class="{ disabled: loading }" class="btn btn-success px-4 ml-4 shadow-none mr-auto font-weight-bold" data-loading-text="Loa." @click="createOrder">
                            {{ $t('buttons.next') }}
                        </button>
                        <span :class="{ invisible: !loading }" class="spinner-border ml-3 spinner-border-sm text-success" role="status"></span>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import store from '@/store';
import OrderModule from "@/store/modules/OrderModule";
import { Component, Vue } from "vue-property-decorator";
import { getModule } from "vuex-module-decorators";
import { IOrderRowCreate } from "@/domain/IOrderRow";
import { IOrderItemChoiceTemp } from "@/domain/IOrderItemChoice";
import router from "@/router";
import { IAddress } from "@/domain/IAddress";
import { IOrderTemp, OrderStatus, PaymentMethod } from "@/domain/IOrder";

@Component
export default class OrderCreate extends Vue {
    address: IAddress | null = null;
    paymentMethod: PaymentMethod | null = null;
    cash = PaymentMethod.CASH;
    card = PaymentMethod.CARD;

    get loading(): boolean {
        return getModule(OrderModule, store).loading;
    }

    get addresses(): IAddress[] {
        return store.state.addresses;
    }

    get restaurantName(): string | null {
        return getModule(OrderModule, store).currentRestaurantName;
    }

    get orderRows(): IOrderRowCreate[] {
        return getModule(OrderModule, store).orderRows;
    }

    get foodCost(): number {
        return getModule(OrderModule, store).foodCost;
    }

    get totalAmount(): number {
        return getModule(OrderModule, store).totalAmount;
    }

    get deliveryCost(): number {
        return getModule(OrderModule, store).deliveryCost;
    }

    get orderHasItems(): boolean {
        return getModule(OrderModule, store).orderHasItems;
    }

    get orderTotal(): number {
        return this.foodCost + this.deliveryCost;
    }

    increment(row: IOrderRowCreate): void {
        getModule(OrderModule, store).increment(row);
    }

    decrement(row: IOrderRowCreate): void {
        getModule(OrderModule, store).decrement(row);
    }

    rowCost(row: IOrderRowCreate): number {
        return row.cost * row.amount + row.choices.reduce((a: number, b: IOrderItemChoiceTemp) => a + b.amount * b.cost, 0)
    }

    clearOrder(): void {
        store.commit('clearOrders');
        router.push({
            path: '/restaurants'
        })
    }

    createOrder(): void {
        const order: IOrderTemp = {
            address: this.address ? `${this.address.street} ${this.address.buildingNumber}, ${this.address.city}, ${this.address.county}` : '',
            apartment: this.address?.apartment,
            comment: this.address?.comment,
            orderStatus: OrderStatus.SENT,
            paymentMethod: this.paymentMethod ?? PaymentMethod.CASH
        }
        getModule(OrderModule, store).createOrder(order).then(r => {
            if (r) router.push('/orders');
            else (this.makeToast())
        });
    }

    makeToast() {
        this.$bvToast.toast(this.$t('order.error').toString(), {
            title: this.$t('order.warning').toString(),
            variant: 'danger',
            toaster: 'b-toaster-top-center',
            solid: true,
            autoHideDelay: 3000
        })
    }

    deleteRowFromOrder(row: IOrderRowCreate) {
        getModule(OrderModule, store).deleteRowFromOrder(row)
    }

    mounted(): void {
        store.dispatch('getAddresses');
    }
}
</script>
<style scoped>
    .btn-circle {
        width: 45px;
        height: 45px;
        line-height: 45px;
        text-align: center;
        padding: 0;
        border-radius: 50%;
    }

    .btn-circle i {
        position: relative;
        top: -1px;
    }

    .btn-circle-sm {
        width: 35px;
        height: 35px;
        line-height: 35px;
        font-size: 0.9rem;
    }

    .btn-circle-lg {
        width: 55px;
        height: 55px;
        line-height: 55px;
        font-size: 1.1rem;
    }

    .btn-circle-xl {
        width: 70px;
        height: 70px;
        line-height: 70px;
        font-size: 1.3rem;
    }
</style>
