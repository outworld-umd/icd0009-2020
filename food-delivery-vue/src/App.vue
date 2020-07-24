<template>
    <div id="app">
        <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
            <a class="navbar-brand" href="#">Navbar</a>
            <div class="collapse navbar-collapse">
                <ul class="navbar-nav mr-auto">
                    <li class="nav-item">
                        <router-link class="nav-link" to="/restaurants">Restaurants</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link class="nav-link" to="/about">About</router-link>
                    </li>
                </ul>
                <ul class="navbar-nav ml-auto">
                    <Language/>
                    <Account/>
                </ul>
            </div>
        </nav>
        <router-view class="container my-4"/>
    </div>
</template>

<style>
    #app {
        text-align: center;
    }

    html {
        margin-bottom: 7em
    }
</style>
<script>
import Vue from "vue";
import { Component } from "vue-property-decorator";
import Language from "@/components/Language";
import CurrentOrder from "@/components/CurrentOrder";
import Account from "@/components/Account";

@Component({
    components: {
        Account,
        CurrentOrder,
        Language
    }
})
export default class App extends Vue {
}
</script>
