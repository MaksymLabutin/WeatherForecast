<template>
  <div id="app" class="container">
    <div v-if="isLoaded" class="row">
      <div class="col-md-9 col-sm-12">
        <div class="row">
          <select
            class="form-control col-md-3 col-xs-12"
            @change="getDataByCityName"
            v-model="data.selectedCity"
          >
            <option v-for="city in data.cities" :key="city.name">{{city.name}}</option>
          </select>
        </div>

        <WeatherTable v-bind:data="data.weatherForecast" v-bind:city="data.selectedCity" />
      </div>

      <div class="col-md-3 col-sm-12">
        <History v-bind:history="history" @load-from-history="loadFromHistory" />
      </div>
    </div>

   
  </div>
</template>

<script lang="ts">
import { Component, Vue, Watch } from "vue-property-decorator";
import { fetchData, fetchByCityName } from "./api/weatherForecastApi";

import { bus } from "./main";
import HistoryDto from "./types/HistoryDto";
import History from "./components/History.vue";
import WeatherTable from "./components/WeatherTable.vue";
import WeatherForecastGeneralDto from "./types/WeatherForecastGeneralDto";

@Component({
  name: "App",
  components: {
    History,
    WeatherTable
  }
})
export default class App extends Vue {
  private isLoaded: boolean = false;

  private data!: WeatherForecastGeneralDto;

  private history: Array<HistoryDto> = new Array<HistoryDto>();

  public async mounted() {
    const res = await fetchData();

    this.data = res;
    this.WriteToHistory(res.selectedCity);
    if(this.data.errored) return;
    this.isLoaded = true;
  }

  private async getDataByCityName(event: any) {
    const cityName = event.target.value;
    const link = this.data.links.find(_ => _.rel === "getByCity");

    if (!link) return;
    this.isLoaded = false;

    const weatherForecastInHistory = this.history.find(
      _ => _.city === cityName
    );

    if (weatherForecastInHistory) {
      this.data.weatherForecast = [];
      this.data.weatherForecast = weatherForecastInHistory.weatherForecast;
    } else {
      const res = await fetchByCityName(link.href, cityName);
      
      if(res.errored) return;
      this.data.weatherForecast = res;
      this.WriteToHistory(cityName);
    }
    this.isLoaded = true;
  }

  private loadFromHistory(event: any) {
    this.isLoaded = false;
    const cityName = event.target.innerText;
    const weatherForecastInHistory = this.history.find(
      _ => _.city === cityName
    );

    if (weatherForecastInHistory) {
      this.data.weatherForecast = [];
      this.data.weatherForecast = weatherForecastInHistory.weatherForecast;
    }
    this.data.selectedCity = cityName;

    this.isLoaded = true;
  }

  private WriteToHistory(cityName: string) {
    this.history.push({
      city: cityName,
      weatherForecast: this.data.weatherForecast
    } as HistoryDto);
  }
}
</script>

<style lang="less">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}

select{
  margin: 10px 30px !important;
}
 
</style> 
 