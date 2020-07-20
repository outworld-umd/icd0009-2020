<template>
    <b-modal :id="id" hide-footer @show="beforeShown" @hide="beforeHidden">
        <template v-if="item" v-slot:modal-header>
            <h4>{{ item.name }}</h4>
        </template>
        <div v-if="item">
            <p class="my-4">Hello from modal {{ id + " " + restaurantId + " " + restaurantName }}</p>
            <p>amount: {{ amount }}</p>
            <p>{{ item.name }}</p>
            <div class="btn-group btn-group-justified mt-2" style="min-width: 100%">
                <b-button type="button" class="inner btn btn-danger" style="min-width: 50%" @click="deleteFromOrder">{{$t('buttons.orderDelete')}}</b-button>
                <b-button type="button" class="inner btn btn-success" style="min-width: 50%" @click="addToOrder">{{$t('buttons.orderAdd')}}</b-button>
            </div>
        </div>
    </b-modal>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import store from "@/store";
import { IItem } from "@/domain/IItem";

@Component
export default class Item extends Vue {
    @Prop() id!: string;
    @Prop() restaurantId!: string;
    @Prop() restaurantName!: string;

    get item(): IItem | null {
        return store.state.item;
    }

    get amount(): number {
        return store.getters.amountOfItem(this.id);
    }

    deleteFromOrder(): void {
        console.log("delete")
        this.$bvModal.hide(this.id)
    }

    addToOrder(): void {
        console.log("add")
        this.$bvModal.hide(this.id)
    }

    beforeShown(): void {
        store.dispatch('getItem', this.id)
    }

    beforeHidden(): void {
        store.commit('setItem', null)
    }
}
</script>

<style scoped>

</style>
