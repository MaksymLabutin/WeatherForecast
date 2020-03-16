export default interface WeatherForecastGeneralDto {
    selectedCity: string,
    links: Array<LinkDto>,
    cities: Array<CityDto>,
    weatherForecast: Array<WeatherForecastDto>,
    errored: boolean
}

export interface LinkDto {
    rel: string,
    href: string
}

export interface CityDto {
    name: string,
    zipCode: string
}

export interface WeatherForecastDto {
    date: string,
    temperature: string,
    humidity: string,
    windSpeed: string
}