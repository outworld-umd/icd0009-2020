<template>
    <div class="container text-left">
        <div v-if="restaurantName">
            <div class="m-4">
                <h5>{{ $t('order.newOrderAt') }}:</h5>
                <h2 class="font-weight-bold">{{ restaurantName }}</h2>
            </div>
            <div class="container w-75 ">
                <table class="table mt-5">
                    <tr v-for="row in orderRows" :key="row.name">
                        <td class="h5 w-10">{{ row.amount }}x</td>
                        <td class="h5">
                            {{ row.name }} <span style="float: right">{{ row.cost.toFixed(2) }}€</span>
                            <div class="font-weight-light h6 text-secondary">
                                <div v-for="choice in row.choices" :key="choice.name">
                                    {{ choice.name }}<span v-if="choice.cost" style="float: right">+{{ choice.cost.toFixed(2) }}€</span>
                                </div>
                            </div>
                            <div v-if="row.comment" class="small text-secondary font-weight-light font-italic">"{{ row.comment }}"</div>
                        </td>
                        <td class="h3 text-right">{{ rowCost(row).toFixed(2) }}€</td>
                        <td>
                            <button class="btn btn-danger rounded-circle text-white shadow-none"  style="float: right" @click="deleteRowFromOrder(row)">
                                <span class="fa fa-trash"></span>
                            </button>
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
                                    <select class="custom-select font-weight-light">
                                        <option class="font-weight-light" selected disabled>{{ $t('buttons.choose') }}</option>
                                        <option class="font-weight-light" value="1">One</option>
                                        <option class="font-weight-light" value="2">Two</option>
                                        <option class="font-weight-light" value="3">Three</option>
                                    </select>
                                </label>
                            </div>
                            <div class="form-group">
                                <label class="control-label w-75 font-weight-light font-italic">
                                    {{ $t('order.paymentMethod') }}
                                    <select class="custom-select font-weight-light">
                                        <option class="font-weight-light" selected disabled>{{ $t('buttons.choose') }}</option>
                                        <option class="font-weight-light" value="1">One</option>
                                        <option class="font-weight-light" value="2">Two</option>
                                        <option class="font-weight-light" value="3">Three</option>
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
                        <router-link to='/orders/new' class="btn btn-success px-4 ml-4 shadow-none mr-auto font-weight-bold">{{ $t('buttons.next') }}</router-link>
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
import { IOrderRowTemp } from "@/domain/IOrderRow";
import { IOrderItemChoiceTemp } from "@/domain/IOrderItemChoice";
import router from "@/router";

@Component
export default class OrderCreate extends Vue {
    get restaurantName(): string | null {
        return getModule(OrderModule, store).currentRestaurantName;
    }

    get orderRows(): IOrderRowTemp[] {
        return getModule(OrderModule, store).orderRows;
    }

    get foodCost(): number {
        return getModule(OrderModule, store).foodCost;
    }

    get deliveryCost(): number {
        return getModule(OrderModule, store).deliveryCost;
    }

    get orderHasItems(): boolean {
        return getModule(OrderModule, store).orderHasItems;
    }

    get orderTotal(): number {
        return this.foodCost + this.deliveryCost
    }

    rowCost(row: IOrderRowTemp): number {
        return row.cost * row.amount + row.choices.reduce((a: number, b: IOrderItemChoiceTemp) => a + b.amount * b.cost, 0)
    }

    clearOrder(): void {
        store.commit('clearOrders');
        router.push({
            path: '/restaurants'
        })
    }

    deleteRowFromOrder(row: IOrderRowTemp) {
        getModule(OrderModule, store).deleteRowFromOrder(row)
    }
}
</script>
<style scoped>

</style>
