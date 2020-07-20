<template>
    <div class="d-flex align-items-center">
        <div class="p-2 badge badge-success align-bottom mx-3">{{ amount }}</div>
        <div class="p-2 mx-3 text-left">
            <div>{{ item.name }} </div>
            <div class="font-weight-bold">{{ item.price.toFixed(2) }}€</div>
        </div>
        <button class="ml-auto p-2 btn btn-secondary px-4">Telli</button>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { IItemView } from "@/domain/IItem";
import store from '@/store'
import { IOrderRowCreate } from "@/domain/IOrderRow";

@Component
export default class ItemView extends Vue {
    @Prop() item!: IItemView;

    get amount(): number {
        return store.getters.amountOfItem(this.item.id);
    }
}
</script>

<style scoped>

</style>
