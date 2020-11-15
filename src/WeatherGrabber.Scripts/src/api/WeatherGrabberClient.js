import https from 'https';
import axios from 'axios';
import JSONbig from 'json-bigint';

class WeatherGrabberClient {
    #baseUrl;

    constructor() {
        this.#baseUrl = `http://localhost:45300`;
        this.client = axios.create({
            transformResponse: data => {
                if (data) {
                    console.log(`response: ${data}`);
                    return JSONbig.parse(data)
                }
            },
            httpsAgent: new https.Agent({
                rejectUnauthorized: false
            })
        });
    }

    getCities = () => this.sendGet(`${this.#baseUrl}/api/v1/cities`);
    getWeather = (cityId) => this.sendGet(`${this.#baseUrl}/api/v1/cities/${cityId}/weather`);

    sendGet = (url) => {
        console.log(`try send request to: ${url}`);
        return this.client.get(url).then(function (response) {
            return response.data;
        }).catch(function (error) {
            console.error(`${error}`);
            console.error(`Url: ${error.config.url}`);
            console.error(`Method: ${error.config.method}`);
            console.error(`CorrelationId: ${error.response.headers['x-correlation-id']}`);
            return error.response;
        });
    };
}

export const weatherGrabberClient = () => new WeatherGrabberClient();
