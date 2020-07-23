<template>
    <div>
        <table class="container table table-bordered">
            <tr v-for="address in addresses" :key="address.id">
                <td>{{ address.street }}</td>
                <td>{{ address.buildingNumber }}</td>
                <td>{{ address.city }}</td>
                <td>{{ address.county }}</td>
                <td>{{ address.comment }}</td>
            </tr>
        </table>
        <CurrentOrder/>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import CurrentOrder from "@/components/CurrentOrder.vue";
import { IAddress } from "@/domain/IAddress";
import store from '@/store'

@Component({
    components: { CurrentOrder }
})
export default class AddressIndex extends Vue {
    get addresses(): IAddress[] {
        return store.state.addresses;
    }

    mounted(): void {
        store.dispatch('getAddresses');
    }
}
</script>
<style scoped>

</style>
