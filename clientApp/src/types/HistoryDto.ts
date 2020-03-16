import { WeatherForecastDto } from './WeatherForecastGeneralDto';

export default interface HistoryDto{
    city: string;
    weatherForecast: Array<WeatherForecastDto>
}