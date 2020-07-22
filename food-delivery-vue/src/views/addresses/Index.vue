<template>
    <div>
        address
        <CurrentOrder/>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import CurrentOrder from "@/components/CurrentOrder.vue";

@Component({
    components: { CurrentOrder }
})
export default class AddressIndex extends Vue {
}
</script>
<style scoped>

</style>
