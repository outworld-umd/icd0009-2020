<template>
    <b-modal :id="typeId + id" hide-footer @show="beforeShown" @hide="beforeHidden" style="overflow-y:scroll;">
        <template v-if="item" v-slot:modal-header>
            <h3>{{ item.name }}</h3>
        </template>
        <div v-if="item">
            <p class="mb-5 font-weight-light">{{ item.description }}</p>
            <div class="container" >
                <hr v-if="item.nutritionInfos.length"/>
                <table v-if="item.nutritionInfos" class="table table-borderless table-sm w-75 text-center mx-auto small">
                    <tbody>
                    <tr v-for="info in item.nutritionInfos" :key="info.id">
                        <td>{{ info.name }}</td>
                        <td>{{ info.amount }} {{ info.unit }}</td>
                    </tr>
                    </tbody>
                </table>
                <hr v-if="item.options"/>
                <div v-for="option in item.options" :key="option.id">
                    <div class="mb-2">
                        <h4 style="display: inline-block">{{ option.name }}</h4>
                        <span v-if="option.isRequired" style="vertical-align: center" class="badge badge-danger mx-2">{{$t('order.required')}}</span>
                    </div>
                    <div v-if="option.isSingle">
                        <div v-for="choice in option.choices" :key="choice.id">
                            <label class="font-weight-light">
                                <input :id="choice.id" :value='choice' v-model="chosenRadios[option.id]" type="radio">
                                {{ choice.name }} <span v-if="choice.additionalPrice" class="font-weight-light">+{{ choice.additionalPrice.toFixed(2) }}€</span>
                            </label>
                        </div>
                    </div>
                    <div v-else>
                        <div v-for="choice in option.choices" :key="choice.id">
                            <label class="font-weight-light">
                                <input :value='choice' v-model="chosenBoxes" type="checkbox">
                                {{ choice.name }} <span v-if="choice.additionalPrice">+{{ choice.additionalPrice.toFixed(2) }}€</span>
                            </label>
                        </div>
                    </div>
                    <hr/>
                </div>
            </div>
            <div class="mt-5 d-flex justify-content-center align-items-center">
                <button class="btn btn-secondary rounded-circle text-white shadow-none" :disabled="amount < 1" @click="decrement"><span class="fa fa-minus"></span></button>
                <div class="px-4 h3 w-25 text-center">{{ amount }}</div>
                <button class="btn btn-secondary rounded-circle text-white shadow-none" :disabled="(amount + totalAmount) > 19" @click="increment"><span class="fa fa-plus"></span></button>
            </div>
            <label class="my-5 container">
                <span class="my-2 font-italic font-weight-light">{{ $t('order.comment') }}</span>
                <input type="text" class="form-control" v-model="comment" maxlength="100">
            </label>
            <div class="btn-group btn-group-justified mt-2" style="min-width: 100%">
                <b-button type="button" class="inner btn btn-success" style="min-width: 50%" :disabled="!validateOrder()" @click="addToOrder">{{$t('buttons.orderAdd')}}</b-button>
            </div>
        </div>
    </b-modal>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import store from "@/store";
import OrderModule from '@/store/modules/OrderModule';
import { IItem } from "@/domain/IItem";
import { IOrderRowCreate } from "@/domain/IOrderRow";
import { IOrderItemChoiceTemp } from "@/domain/IOrderItemChoice";
import { getModule } from "vuex-module-decorators";
import { IChoice } from "@/domain/IChoice";
import { IRadioChoice } from "@/types/IRadioChoice";

@Component
export default class Item extends Vue {
    @Prop() id!: string;
    @Prop() typeId!: string;
    @Prop() restaurantId!: string;
    @Prop() restaurantName!: string;
    @Prop() deliveryCost!: number;
    chosenRadios: IRadioChoice = {};
    chosenBoxes: IChoice[] = [];
    amount = 0;
    comment: string | null = null;

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

    validateOrder(): boolean {
        if (this.item) {
            for (const option of this.item.options) {
                if (option.isRequired) {
                    if (option.isSingle && !(Object.prototype.hasOwnProperty.call(this.chosenRadios, option.id))) return false;
                    if (!option.isSingle && !(this.intersect(option.choices.map(d => d.id), this.chosenBoxes.map((d: IChoice) => d.id)))) return false;
                }
            }
        }
        return this.amount > 0
    }

    intersect(array1: string[], array2: string[]): number {
        return array1.filter(value => array2.includes(value)).length;
    }

    addToOrder(): void {
        if (!getModule(OrderModule, store).currentRestaurantId || (getModule(OrderModule, store).currentRestaurantId !== this.restaurantId && confirm(this.$t('order.deleteOld').toString()))) {
            store.commit('clearOrders')
            store.commit('setCurrentRestaurantId', this.restaurantId);
            store.commit('setCurrentRestaurantName', this.restaurantName);
            store.commit('setDeliveryCost', this.deliveryCost);
        }
        if (getModule(OrderModule, store).currentRestaurantId === this.restaurantId) {
            const choices: IOrderItemChoiceTemp[] = [];
            if (this.item) {
                for (const option of this.item.options) {
                    if (option.isRequired && option.isSingle) {
                        console.log(option.id, this.chosenRadios)
                        const choice = this.chosenRadios[option.id] as IChoice;
                        const otherChoice: IOrderItemChoiceTemp = {
                            itemChoiceId: choice.id,
                            amount: this.amount,
                            cost: choice.additionalPrice,
                            name: choice.name
                        }
                        choices.push(otherChoice)
                    }
                }
            }
            for (const choice of this.chosenBoxes) {
                choices.push({
                    itemChoiceId: choice.id,
                    amount: this.amount,
                    cost: choice.additionalPrice,
                    name: choice.name
                })
            }
            const orderRow: IOrderRowCreate = {
                itemId: this.id,
                amount: this.amount,
                name: this.item?.name ?? "",
                cost: this.item?.price ?? 0,
                choices: choices,
                comment: this.comment ? this.comment : undefined
            };
            store.commit('addOrderRow', orderRow)
        }
        this.$bvModal.hide(this.typeId + this.id)
    }

    beforeShown(): void {
        this.amount = 0;
        this.chosenRadios = {};
        this.chosenBoxes = [];
        this.comment = null;
        store.dispatch('getItem', this.id)
    }

    beforeHidden(): void {
        store.commit('setItem', null)
    }
}
</script>

<style scoped>
</style>
