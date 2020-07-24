<template>
    <div class="position-relative">
        <button class="d-flex btn px-4 btn-secondary position-absolute" @click="goBack">{{ $t('buttons.back') }}</button>
        <div v-if="restaurant" class="container">
            <div>
                <h2 class="m-4 font-weight-bold">{{ restaurant.name }}</h2>
            </div>
            <div class="card my-5" v-for="itemType in restaurant.itemTypes" :key="itemType.id">
                <div class="card-header text-left" :class="{ 'alert-success': itemType.isSpecial }">
                    <h6 v-if="itemType.isSpecial" class="position-absolute m-3" style="top: 0; right: 0">{{ $t('restaurant.offer') }}</h6>
                    <h3>{{ itemType.name }}</h3>
                    <p>{{ itemType.description }}</p>
                </div>
                <ul class="list-group list-group-flush">
                    <ItemView class="list-group-item" v-for="item in itemType.items" :key="item.id" :itemView="item" :restaurant="restaurant"/>
                </ul>
            </div>
        </div>
        <h4 v-else class="text-danger">Restaurant not found</h4>
        <CurrentOrder/>
    </div>
</template>

<script lang="ts">
import router from '@/router';
import store from '@/store';
import OrderModule from '@/store/modules/OrderModule';
import { Component, Vue, Prop } from 'vue-property-decorator';
import { IRestaurant } from "@/domain/IRestaurant";
import ItemView from "@/components/ItemView.vue";
import { getModule } from "vuex-module-decorators";
import CurrentOrder from "@/components/CurrentOrder.vue";

@Component({
    components: { ItemView, CurrentOrder }
})
export default class RestaurantMenu extends Vue {
    @Prop() id!: string;
    orderHasItems = false;

    get restaurant(): IRestaurant | null {
        return store.state.restaurant;
    }

    goBack(): void {
        router.push({
            path: '/restaurants'
        })
    }

    mounted(): void {
        store.dispatch("getRestaurant", this.id)
    }

    updated(): void {
        this.orderHasItems = getModule(OrderModule, store).orderHasItems
    }
}
</script>

<style scoped>

</style>
