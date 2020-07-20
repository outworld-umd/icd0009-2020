<template>
    <div class="d-flex align-items-center">
        <div class="p-2 badge badge-success align-bottom mx-3">{{ amount }}</div>
        <div class="p-2 mx-3 text-left">
            <div>{{ itemView.name }} </div>
            <div class="font-weight-bold">{{ itemView.price.toFixed(2) }}€</div>
        </div>
        <b-button class="ml-auto p-2 btn btn-secondary px-4" @click="showModal(itemView.id)" v-b-modal="itemView.id">{{$t('buttons.order')}}</b-button>
        <Item :id="itemView.id" :restaurant-id="restaurantId" :restaurant-name="restaurantName"/>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { IItem, IItemView } from "@/domain/IItem";
import store from '@/store'

@Component({
    components: {
        Item: () => import('@/components/Item.vue')
    }
})
export default class ItemView extends Vue {
    @Prop() itemView!: IItemView;
    @Prop() restaurantId!: string;
    @Prop() restaurantName!: string;

    get amount(): number {
        return store.getters.amountOfItem(this.itemView.id);
    }

    showModal(id: string) {
        this.$bvModal.show(id);
    }
}
</script>

<style scoped>

</style>
