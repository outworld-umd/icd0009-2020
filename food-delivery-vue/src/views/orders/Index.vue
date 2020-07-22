<template>
    <div>
        order index
        <CurrentOrder/>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import CurrentOrder from "@/components/CurrentOrder.vue";

@Component({
    components: { CurrentOrder }
})
export default class OrderIndex extends Vue {
}
</script>

<style scoped>

</style>
