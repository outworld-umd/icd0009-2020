<template>
    <div class="position-relative">
        <button class="d-flex btn btn-outline-secondary position-absolute" @click="goBack">Back</button>
        <div>
            <div v-if="restaurant" class="text-success">
                <h4>{{ restaurant.name }}</h4>
            </div>
            <h4 v-else class="text-danger">Restaurant not found</h4>
        </div>
    </div>
</template>

<script lang="ts">
import router from '@/router';
import store from '@/store'
import { Component, Vue, Prop } from 'vue-property-decorator';
import { IRestaurant } from "@/domain/IRestaurant";

@Component
export default class RestaurantMenu extends Vue {
    @Prop() id!: string;

    get restaurant(): IRestaurant | null {
        return store.state.restaurant;
    }

    goBack(): void {
        router.go(-1);
    }

    mounted(): void {
        store.dispatch("getRestaurant", this.id)
    }
}
</script>

<style scoped>

</style>
