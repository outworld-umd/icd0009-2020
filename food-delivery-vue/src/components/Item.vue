<template>
    <b-modal :id="id" hide-footer @show="beforeShown" @hide="beforeHidden" style="overflow-y:scroll;">
        <template v-if="item" v-slot:modal-header>
            <h4>{{ item.name }}</h4>
        </template>
        <div v-if="item">
            <p class="my-4">{{ item.description }}</p>
            <div class="m-5 d-flex justify-content-center align-items-center">
                <button class="btn btn-secondary rounded-circle text-white shadow-none" :disabled="amount < 1" @click="decrement"><span class="fa fa-minus"></span></button>
                <div class="px-4 h3 w-25 text-center">{{ amount }}</div>
                <button class="btn btn-secondary rounded-circle text-white shadow-none" :disabled="(amount + totalAmount) > 19" @click="increment"><span class="fa fa-plus"></span></button>
            </div>
            <div class="btn-group btn-group-justified mt-2" style="min-width: 100%">
                <b-button type="button" class="inner btn btn-success" style="min-width: 50%" :disabled="amount < 1" @click="addToOrder">{{$t('buttons.orderAdd')}}</b-button>
            </div>
        </div>
    </b-modal>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import store from "@/store";
import OrderModule from '@/store/modules/OrderModule';
import { IItem } from "@/domain/IItem";
import { IOrderRowTemp } from "@/domain/IOrderRow";
import { IOrderItemChoiceTemp } from "@/domain/IOrderItemChoice";
import { getModule } from "vuex-module-decorators";

@Component
export default class Item extends Vue {
    @Prop() id!: string;
    @Prop() restaurantId!: string;
    @Prop() restaurantName!: string;
    @Prop() deliveryCost!: number;

    amount = 0;
    get totalAmount(): number {
        return getModule(OrderModule, store).totalAmount;
    }

    get item(): IItem | null {
        return store.state.item;
    }

    decrement(): void {
        this.amount--;
    }

    increment(): void {
        this.amount++;
    }

    addToOrder(): void {
        if (!getModule(OrderModule, store).currentRestaurantId || (getModule(OrderModule, store).currentRestaurantId !== this.restaurantId && confirm(this.$t('order.deleteOld').toString()))) {
            store.commit('setCurrentRestaurantId', this.restaurantId);
            store.commit('setCurrentRestaurantName', this.restaurantName);
            store.commit('setDeliveryCost', this.deliveryCost);
            store.commit('clearOrders')
        }
        if (getModule(OrderModule, store).currentRestaurantId === this.restaurantId) {
            const choices = [] as IOrderItemChoiceTemp[];
            const orderRow: IOrderRowTemp = {
                itemId: this.id,
                amount: this.amount,
                cost: this.item?.price ?? 0,
                choices: choices
            };
            store.commit('addOrderRow', orderRow)
        }
        this.$bvModal.hide(this.id)
        console.log(getModule(OrderModule, store).currentRestaurantId, getModule(OrderModule, store).currentRestaurantName)
    }

    beforeShown(): void {
        this.amount = 0;
        store.dispatch('getItem', this.id)
    }

    beforeHidden(): void {
        store.commit('setItem', null)
    }
}
</script>

<style scoped>
</style>
