<template>
    <div class="d-flex align-items-center">
        <div class="p-2 mx-3 text-left w-75">
            <div>{{ orderView.restaurantName }}</div>
            <div class="font-weight-bold">{{ orderView.foodCost.toFixed(2) }}€</div>
        </div>
        <h4 class="w-25 mr-4">
            <span v-if="orderView.orderStatus === 0" class="badge badge-secondary w-100">{{ $t('order.sent') }}</span>
            <span v-if="orderView.orderStatus === 1" class="badge badge-info w-100">{{ $t('order.cooking') }}</span>
            <span v-if="orderView.orderStatus === 2" class="badge badge-primary w-100">{{ $t('order.delivering') }}</span>
            <span v-if="orderView.orderStatus === 3" class="badge badge-success w-100">{{ $t('order.delivered') }}</span>
        </h4>
        <b-button class="ml-auto p-2 btn btn-secondary px-4" @click="showModal(orderView.id)" v-b-modal="`order${orderView.id}`">{{ $t('buttons.more') }}</b-button>
        <Order :id="orderView.id" />
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { IOrderView } from "@/domain/IOrder";
import Order from "@/components/Order.vue";

@Component({
    components: { Order }
})
export default class OrderView extends Vue {
    @Prop() orderView!: IOrderView;

    showModal(id: string) {
        this.$bvModal.show(id);
    }
}
</script>

<style scoped>

</style>
