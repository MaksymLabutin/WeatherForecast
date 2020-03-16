import axios from "axios"; 

const serverUrl = "https://localhost:5001/api/weather/forecast";

export async function fetchData() {
    var res = await axios.get(serverUrl);
    
    return res.data;
}


export async function fetchByCityName(link: string, cityName: string) {
    var res = await axios.get(link + cityName);
    return res.data;
}