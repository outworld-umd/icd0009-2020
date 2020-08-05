<template>
    <div class="col-md-4 pb-4">
        <div class="card text-center">
            <div class="card-body">
                <h5 class="card-title">{{ restaurant.name }}</h5>
                <router-link v-if="restaurant.isOpen" :to="`restaurants/${restaurant.id}/menu`">
                    <button class="btn btn-success my-3 w-75 shadow-none">{{ $t('restaurant.showMenu') }}</button>
                </router-link>
                <div v-else class="btn text-muted">
                    <div>{{ $t('restaurant.closed') }}</div>
                </div>
                <div class="row d-flex align-items-center font-weight-light justify-content-center mx-auto my-2">
                    <div class="fa fa-shopping-basket mx-1"></div>
                    <div class="mx-1">{{ restaurant.deliveryCost.toFixed(2) }}€</div>
                </div>
            </div>
            <div class="card-footer text-muted mt-2">{{ restaurant.categories.map(c => c.name).join("  ") }}</div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { IRestaurantView } from "@/domain/IRestaurant";

@Component
export default class RestaurantView extends Vue {
    @Prop() restaurant!: IRestaurantView;
}
</script>

<style scoped>

</style>
