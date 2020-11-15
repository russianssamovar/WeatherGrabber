import './App.css';
import React from "react";
import ComboBox from './components/ComboBox';
import {weatherGrabberClient} from './api/WeatherGrabberClient';
import {WeatherTable} from './components/WeatherTable';

export class App extends React.Component {
    async getData() {
        let client = weatherGrabberClient();
        return await client.getCities();
    }

    changeCity(city) {
        if (city){
            this.setState({cityId: city.id});
        }
    }

    constructor(...args) {
        super(...args);
        this.state = {
            cities: null,
            cityId: null
        };
        this.changeCity = this.changeCity.bind(this)
    }

    componentDidMount() {
        if (!this.state.cities) {
            this.getData().then(cities => this.setState({cities}))
                .catch(err => { /*...handle the error...*/
                });
        }
    }

    render() {
        return (
            <div className="App">
                {this.state.cities ? <ComboBox cities={this.state.cities} changeCity={this.changeCity}/> : <em>Loading...</em>}
                {this.state.cityId ? <WeatherTable cityId={this.state.cityId}/> : <em>Выберите город</em>}
            </div>
        );
    }
}








