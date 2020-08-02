<template>
    <div class="position-relative">
        <button class="d-flex btn px-4 btn-secondary position-absolute shadow-none" @click="goBack">{{ $t('buttons.back') }}</button>
        <div v-if="!restaurant">
            <div class="spinner-border m-5 text-success" role="status">
                <span class="sr-only">Loading...</span>
            </div>
        </div>
        <div v-if="restaurant" class="container">
            <div>
                <h2 class="m-4 font-weight-bold">{{ restaurant.name }}</h2>
            </div>
            <table class="table table-borderless table-sm w-25 text-center mx-auto small">
                <tbody>
                <tr v-for="wh in workingHours" :key="wh.id">
                    <td>{{ $t(getWeekdayName(wh.weekDay)) }}</td>
                    <td>{{ getTime(wh.openingTime) }} - {{ getTime(wh.closingTime) }}</td>
                </tr>
                </tbody>
            </table>
            <div class="card my-5" v-for="itemType in restaurant.itemTypes" :key="itemType.id">
                <div class="card-header text-left" :class="{ 'alert-success': itemType.isSpecial }">
                    <h6 v-if="itemType.isSpecial" class="position-absolute m-3" style="top: 0; right: 0">{{ $t('restaurant.offer') }}</h6>
                    <h3>{{ itemType.name }}</h3>
                    <p>{{ itemType.description }}</p>
                </div>
                <ul class="list-group list-group-flush">
                    <ItemView class="list-group-item" v-for="item in itemType.items" :key="item.id" :itemView="item" :type-id="itemType.id" :restaurant="restaurant"/>
                </ul>
            </div>
        </div>
        <CurrentOrder/>
    </div>
</template>

<script lang="ts">
import router from '@/router';
import { DayNames, DayOfWeek, IWorkingHours } from '@/domain/IWorkingHours';
import store from '@/store';
import OrderModule from '@/store/modules/OrderModule';
import { Component, Prop, Vue } from 'vue-property-decorator';
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

    get workingHours(): IWorkingHours[] {
        const workingHours = this.restaurant?.workingHourses ?? [];
        workingHours.filter(e => {
            if (this.$i18n.locale === 'en') {
                if (e.weekDay === DayOfWeek.SUNDAY2) e.weekDay = DayOfWeek.SUNDAY;
            } else {
                if (e.weekDay === DayOfWeek.SUNDAY) e.weekDay = DayOfWeek.SUNDAY2;
            }
        })
        return workingHours.sort((h1, h2) => h1.weekDay - h2.weekDay)
    }

    getWeekdayName(day: DayOfWeek): string {
        return DayNames[day];
    }

    getTime(dateString: string): string {
        const date = new Date(dateString);
        return this.$d(date, 'long').match(/\d+:\d+( [AP]M)?/)?.shift() ?? "";
    }

    goBack(): void {
        router.push({
            path: '/restaurants'
        })
        store.commit('setRestaurant', null)
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
