<template>
    <div class="container">
        <div class="row">
            <RestaurantView v-for="restaurant in restaurants" :key="restaurant.id" :restaurant="restaurant"/>
        </div>
    </div>
</template>

<script lang="ts">
import store from '../../store'
import { Component, Vue } from 'vue-property-decorator';
import { IRestaurantView } from "@/domain/IRestaurant";
import RestaurantView from '@/components/RestaurantView.vue';
@Component({
    components: { RestaurantView }
})
export default class RestaurantIndex extends Vue {
    get restaurants(): IRestaurantView[] {
        return store.state.restaurants;
    }

    mounted(): void {
        store.dispatch('getRestaurants')
    }
}
</script>

<style scoped>

</style>
