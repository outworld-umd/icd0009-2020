<template>
    <div>
        <h2 class="font-weight-bold">{{ $t('account.yourOrders') }}</h2>
        <div v-if="loading" class="text-center">
            <div class="spinner-border m-5 text-success" role="status">
                <span class="sr-only">Loading...</span>
            </div>
        </div>
        <ul class="list-group list-group-flush">
            <OrderView class="list-group-item" v-for="order in orders" :key="order.id" :orderView="order"/>
        </ul>
        <CurrentOrder/>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import CurrentOrder from "@/components/CurrentOrder.vue";
import OrderView from "@/components/OrderView.vue";
import { IOrderView } from "@/domain/IOrder";
import store from '@/store'

@Component({
    components: { OrderView, CurrentOrder }
})
export default class OrderIndex extends Vue {
    get orders(): IOrderView[] {
        return store.state.orders;
    }

    get loading(): boolean {
        return store.state.loading;
    }

    mounted(): void {
        store.dispatch('getOrders')
    }

    destroyed(): void {
        store.commit('setOrders', null)
    }
}
</script>

<style scoped>

</style>
