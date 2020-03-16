<template>
  <div>
    <h2>Search history</h2>
    <ol>
      <li
        v-for="cityData in history"
        :key="cityData.city"
        v-on:click="$emit('load-from-history', $event)"
      >{{cityData.city}}</li>
    </ol>
  </div>
</template>
 
<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import HistoryDto from "@/types/HistoryDto";

import { bus } from "../main";

@Component
export default class History extends Vue {
  @Prop() private history!: Array<HistoryDto>;
}
</script>
 
<style lang="less">
li {
  cursor: pointer; 
}
</style> 
 