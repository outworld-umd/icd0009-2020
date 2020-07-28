<template>
    <b-modal size="lg" :id="`order${id}`" hide-footer @show="beforeShown" @hide="beforeHidden" style="overflow-y:scroll;">
        <template v-if="order" v-slot:modal-header>
            <div class="my-3 mx-5">
                <h5 class="h5 font-weight-light">{{ $t('order.newOrderAt') }}:</h5>
                <h2 class="font-weight-bold">{{ order.restaurantName }}</h2>
            </div>
            <div class="my-3 mx-5 h5 font-weight-light">
                <div>{{ $d(new Date(order.createdAt), 'long') }}</div>
            </div>
        </template>
        <div v-if="order" class="m-4">
            <div class="container justify-content-center w-75">
                <table class="table table-borderless font-weight-light h5">
                    <tr>
                        <td>{{ $t('order.address') }}:</td>
                        <td class="h6 font-weight-light font-italic">
                            <div>{{ order.address }}</div>
                            <div>{{ $t('address.apartment') }}: {{ order.apartment }}</div>
                            <div v-if="order.comment">{{ $t('address.comment') }}: {{ order.comment }}</div>
                        </td>
                    </tr>
                    <tr>
                        <td>{{ $t('order.paymentMethod') }}:</td>
                        <td class="h6 font-weight-light font-italic">
                            <div v-if="order.paymentMethod === 0">{{ this.$t('paymentMethods.cash') }}</div>
                            <div v-if="order.paymentMethod === 1">{{ this.$t('paymentMethods.card') }}</div>
                            <div v-if="order.paymentMethod === 2">{{ this.$t('paymentMethods.app') }}</div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="container">
                <table class="table mt-5">
                    <tr v-for="(row, i) in order.orderRows" :key="i">
                        <td class="h5" style="width: 10%">{{ row.amount }}x</td>
                        <td class="h5">
                            {{ row.name }} <span style="float: right">{{ row.cost.toFixed(2) }}€</span>
                            <div class="font-weight-light h6 text-secondary">
                                <div v-for="(choice, i) in row.choices" :key="i">
                                    {{ choice.name }}<span v-if="choice.cost" style="float: right">+{{ choice.cost.toFixed(2) }}€</span>
                                </div>
                            </div>
                            <div v-if="row.comment" class="small text-secondary font-weight-light font-italic">"{{ row.comment }}"</div>
                        </td>
                        <td class="px-4 h3 w-25 text-right">{{ rowCost(row).toFixed(2) }}€</td>
                    </tr>
                </table>
                <hr class="mb-5"/>
                <div class="d-flex bd-highlight my-5">
                    <div class="p-2 flex-fill d-flex w-50 bd-highlight align-items-center">
                        <div class="mr-auto text-left">
                            <h5>{{ $t('order.foodCost') }}</h5>
                            <h5>{{ $t('order.deliveryCost') }}</h5>
                            <h3>{{ $t('order.total') }}</h3>
                        </div>
                        <div class="pl-4 text-right">
                            <h5>{{ order.foodCost.toFixed(2) }}€</h5>
                            <h5>{{ order.deliveryCost.toFixed(2) }}€</h5>
                            <h3>{{ (order.foodCost + order.deliveryCost).toFixed(2) }}€</h3>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </b-modal>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import store from "@/store";
import { IOrder } from "@/domain/IOrder";
import { IOrderRow } from "@/domain/IOrderRow";
import { IOrderItemChoice } from "@/domain/IOrderItemChoice";

@Component
export default class Item extends Vue {
    @Prop() id!: string;

    get order(): IOrder | null {
        return store.state.order;
    }

    beforeShown(): void {
        store.dispatch('getOrder', this.id)
    }

    beforeHidden(): void {
        store.commit('setOrder', null)
    }

    rowCost(row: IOrderRow): number {
        return row.cost * row.amount + row.choices.reduce((a: number, b: IOrderItemChoice) => a + b.amount * b.cost, 0)
    }
}
</script>

<style scoped>

</style>
