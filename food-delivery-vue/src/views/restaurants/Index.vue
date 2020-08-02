<template>
    <div class="container">
        <div v-if="loading" class="text-center">
            <div class="spinner-border m-5 text-success" role="status">
                <span class="sr-only">Loading...</span>
            </div>
        </div>
        <div class="row">
            <RestaurantView v-for="restaurant in restaurants" :key="restaurant.id" :restaurant="restaurant"/>
        </div>
        <CurrentOrder/>
    </div>
</template>

<script lang="ts">
import store from '../../store'
import { Component, Vue } from 'vue-property-decorator';
import { IRestaurantView } from "@/domain/IRestaurant";
import RestaurantView from '@/components/RestaurantView.vue';
import CurrentOrder from "@/components/CurrentOrder.vue";

@Component({
    components: { RestaurantView, CurrentOrder }
})
export default class RestaurantIndex extends Vue {
    get restaurants(): IRestaurantView[] {
        return store.state.restaurants;
    }

    get loading(): boolean {
        return store.state.loading && !store.state.restaurants;
    }

    mounted(): void {
        store.dispatch('getRestaurants')
    }
}
</script>

<style scoped>

</style>
