<template>
    <label>
        <select class="form-control" v-model="$i18n.locale" @change="updateLanguage($event.target.value)">
            <option v-for="(o, i) in locales" :key="i" :value="o.value" :selected="o.value === defaultLocale">{{ o.caption }}</option>
        </select>
    </label>
</template>

<script lang="ts">
import { LOCALES, Locales } from "@/i18n/locales";
import store, { UserModule } from "@/store";
import { Component, Vue } from "vue-property-decorator";
import { getModule } from "vuex-module-decorators";

@Component
export default class Language extends Vue {
    locales = LOCALES;
    defaultLocale = getModule(UserModule, store).lang;

    updateLanguage(lang: Locales) {
        store.commit("setLanguage", lang);
    }
}
</script>

<style scoped>

</style>
