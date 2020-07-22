<template>
    <div class="fixed-bottom bg-success p-3" v-if="orderHasItems">
        <div class="d-flex align-items-center">
            <div class="ml-5 text-left">
                <h6 class="text-white">{{ $t('order.foodCost') }}</h6>
                <h6 class="text-white">{{ $t('order.deliveryCost') }}</h6>
                <h4 class="text-white">{{ $t('order.total') }}</h4>
            </div>
            <div class="ml-5 text-right">
                <h6 class="text-white">{{ foodCost.toFixed(2) }}€</h6>
                <h6 class="text-white">{{ deliveryCost.toFixed(2) }}€</h6>
                <h4 class="text-white">{{ orderTotal.toFixed(2) }}€</h4>
            </div>
            <div class="ml-auto">
                <button class="btn mr-3 px-4 btn-light text-danger font-weight-bold shadow-none" @click="clearOrder">{{ $t('buttons.clear') }}</button>
                <router-link to='/orders/new' class="btn mr-5 px-4 btn-light text-success font-weight-bold shadow-none">{{ $t('buttons.next') }}</router-link>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Watch } from "vue-property-decorator";
import store from '@/store'
import OrderModule from '@/store/modules/OrderModule'
import { getModule } from "vuex-module-decorators";

@Component
export default class CurrentOrder extends Vue {
    get currentRestaurantName(): string | null {
        return getModule(OrderModule, store).currentRestaurantName;
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

    clearOrder(): void {
        store.commit('clearOrders');
    }

    @Watch('orderTotal')
    onChildChanged(val: string, oldVal: string) {
        console.log(val, oldVal)
    }
}
</script>

<style scoped>
</style>
