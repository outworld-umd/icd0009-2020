<template>
    <div class="container">
        <div class="input-group mb-4">
            <div class="input-group-prepend">
                <span class="input-group-text"><span class="fa fa-search"/></span>
            </div>
            <input id="search" v-model="searchByName" class="form-control font-weight-light" type="text" :placeholder="$t('restaurant.search')"/>
        </div>
        <div v-if="loading" class="text-center">
            <div class="spinner-border m-5 text-success" role="status">
                <span class="sr-only">Loading...</span>
            </div>
        </div>
        <div class="row">
            <RestaurantView v-for="restaurant in filteredRestaurants" :key="restaurant.id" :restaurant="restaurant"/>
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
    searchByName = '';

    get restaurants(): IRestaurantView[] {
        return store.state.restaurants;
    }

    get filteredRestaurants(): IRestaurantView[] {
        return this.restaurants.filter(r => {
            return r.name.toLowerCase().includes(this.searchByName.toLowerCase())
        })
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
