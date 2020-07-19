<template>
    <div class="col-sm-4 pb-4">
        <div class="card text-center">
            <div class="card-body">
                <h5 class="card-title">{{ restaurant.name }}</h5>
                <router-link v-if="restaurant.isOpen" :to="`restaurants/${restaurant.id}`">
                    <button class="btn btn-success">{{ $t('restaurant.showMenu') }}</button>
                </router-link>
                <div v-else class="btn text-muted">
                    <div>{{ $t('restaurant.closed') }}</div>
                </div>
            </div>
            <div class="row m-auto">
                <div class="fa fa-shopping-basket m-1"></div>
                <div class="m-1">{{ restaurant.deliveryCost }}€</div>
            </div>
            <div class="card-footer text-muted mt-2">{{ restaurant.categories.join("  ") }}</div>
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
