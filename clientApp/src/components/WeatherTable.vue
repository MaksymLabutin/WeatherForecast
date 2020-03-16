<template>
  <div>
    <div class="page-content page-container" id="page-content">
      <div class="padding">
        <div class="row container d-flex justify-content-center">
          <div class="col-lg-12 grid-margin stretch-card">
            <!--weather card-->
            <div class="card card-weather">
              <div class="card-body">
                <div class="weather-date-location">
                  <h3>{{getDayName(data[0].date)}}</h3>
                  <p class="text-gray">
                    <span class="weather-date">{{ timestamp }}</span>
                    <span class="weather-location"> {{city}}</span>
                  </p>
                </div>
                <div class="weather-data d-flex">
                  <div class="mr-auto">
                    <h4 class="display-3">
                      {{data[0].temperature}}
                      <span class="symbol">°</span>F
                    </h4>
                    
                  </div>
                </div>
              </div>
              <div class="card-body p-0">
                <div class="d-flex weakly-weather">
                  <div class="weakly-weather-item" v-for="el in getFilteredList()" :key="el.date">
                    <h5 class="mb-0">{{getDayName(el.date)}}</h5>
                    <p class="mb-0">Temperature: {{el.temperature}}°F</p>
                    <p class="mb-0">Humidity: {{el.humidity}}</p>
                    <p class="mb-0">Wind speed: {{el.windSpeed}}</p>
                  </div>
                </div>
              </div>
            </div>
            <!--weather card ends-->
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";

@Component
export default class WeatherTable extends Vue {
  @Prop() public data!: Array<object>;
  @Prop() public city!: string;

  private timestamp: string = "";

  private days = [
    "Sunday",
    "Monday",
    "Tuesday",
    "Wednesday",
    "Thursday",
    "Friday",
    "Saturday"
  ];

  created() {
    this.getNow();
  }

  getFilteredList(){
    return this.data.slice(1, this.data.length);
  }

  getNow() {
    const today = new Date();
    const date =
      today.getFullYear() +
      "-" +
      (today.getMonth() + 1) +
      "-" +
      today.getDate();

    this.timestamp = date;
  }

  getDayName(date: string) {
    var d = new Date(date);
    var dayName = this.days[d.getDay()]; 
    return dayName;
  }
}
</script>

<style lang="less">
.stretch-card > .card {
  width: 100%;
  min-width: 100%;
}

body {
  background-color: #f9f9fa;
}

.flex {
  -webkit-box-flex: 1;
  -ms-flex: 1 1 auto;
  flex: 1 1 auto;
}

// @media (max-width: 991.98px) {
//   .padding {
//     padding: 1.5rem;
//   }
// }

// @media (max-width: 767.98px) {
//   .padding {
//     padding: 1rem;
//   }
// }

// .padding {
//   padding: 5rem;
// }

.grid-margin,
.purchace-popup > div {
  margin-bottom: 25px;
}

.card {
  border: 0;
  border-radius: 2px;
}

.card-weather {
  background: #e1ecff;
  background-image: linear-gradient(
    to left bottom,
    #d6eef6,
    #dff0fa,
    #e7f3fc,
    #eff6fe,
    #f6f9ff
  );
}

.card {
  position: relative;
  display: flex;
  flex-direction: column;
  min-width: 0;
  word-wrap: break-word;
  background-color: #fff;
  background-clip: border-box;
  border: 1px solid rgba(0, 0, 0, 0.125);
  border-radius: 0.25rem;
}

.card-weather .card-body:first-child {
  background: url(https://res.cloudinary.com/dxfq3iotg/image/upload/v1557323760/weather.svg)
    no-repeat center;
  background-size: cover;
}

.card .card-body {
  padding: 1.88rem 1.81rem;
}

.card-body {
  flex: 1 1 auto;
  padding: 1.25rem;
}

.card-weather .weather-date-location {
  padding: 0 0 38px;
}

.h3,
h3 {
  font-size: 1.56rem;
}

.h1,
.h2,
.h3,
.h4,
.h5,
.h6,
h1,
h2,
h3,
h4,
h5,
h6 {
  font-family: "Poppins", sans-serif;
  font-weight: 500;
}

.text-gray,
.card-subtitle,
.new-accounts ul.chats li.chat-persons a p.joined-date {
  color: #969696;
}

p {
  font-size: 13px;
}

.text-gray,
.card-subtitle,
.new-accounts ul.chats li.chat-persons a p.joined-date {
  color: #969696;
}

.card-weather .weather-data {
  padding: 0 0 4.75rem;
}

.mr-auto,
.mx-auto {
  margin-right: auto !important;
}

.display-3 {
  font-size: 2.5rem;
}

.card-weather .card-body {
  background: #ffffff;
}

.card-weather .weakly-weather {
  background: #ffffff;
  overflow-x: auto;
}

.card-weather .weakly-weather .weakly-weather-item {
  flex: 0 0 20%;
  border-right: 1px solid #f2f2f2;
  padding: 1rem;
  text-align: center;
}

.mb-0,
.my-0 {
  margin-bottom: 0 !important;
}

.card-weather .weakly-weather .weakly-weather-item i {
  font-size: 1.2rem;
}
</style>
