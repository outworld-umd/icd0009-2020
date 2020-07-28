<template>
    <div class="d-flex align-items-center">
        <h5>
            <span :class="{ invisible: !amount }" class="p-2 mx-3 badge badge-success align-bottom">{{ amount }}</span>
        </h5>
        <div class="p-2 mx-3 text-left">
            <div>{{ itemView.name }}</div>
            <div class="font-weight-bold">{{ itemView.price.toFixed(2) }}€</div>
        </div>
        <b-button class="ml-auto p-2 btn btn-secondary px-4" @click="showModal(typeId + itemView.id)" v-b-modal="typeId + itemView.id">{{$t('buttons.order')}}</b-button>
        <Item :id="itemView.id" :typeId="typeId" :restaurant-id="restaurant.id" :restaurant-name="restaurant.name" :delivery-cost="restaurant.deliveryCost" />
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { IItemView } from "@/domain/IItem";
import store from '@/store';
import OrderModule from '@/store/modules/OrderModule';
import { IRestaurant } from "@/domain/IRestaurant";
import { getModule } from "vuex-module-decorators";
import Item from "@/components/Item.vue";

@Component({
    components: {
        Item
    }
})
export default class ItemView extends Vue {
    @Prop() itemView!: IItemView;
    @Prop() typeId!: string;
    @Prop() restaurant!: IRestaurant;
    @Prop() deliveryCost!: number;

    get amount(): number {
        return getModule(OrderModule, store).amountOfItem(this.itemView.id);
    }

    showModal(id: string) {
        this.$bvModal.show(id);
    }
}
</script>

<style scoped>

</style>
