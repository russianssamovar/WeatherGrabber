import React from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import {weatherGrabberClient} from "../api/WeatherGrabberClient";

export class WeatherTable extends React.Component {
    async getData(cityId) {
        let client = weatherGrabberClient();
        return await client.getWeather(cityId);
    }

    constructor(...args) {
        super(...args);
        this.state = {
            weather: null
        };
    }

    componentDidMount() {
        if (!this.state.weather) {
            this.getData(this.props.cityId).then(weather => this.setState({weather}))
                .catch(err => { /*...handle the error...*/
                });
        }
    }

    componentWillReceiveProps(){
        this.getData(this.props.cityId).then(weather => this.setState({weather}))
            .catch(err => { /*...handle the error...*/
            });
    }

    render() {
        return (
            <div className="TableContainer">
                {this.state.weather ?
                    <TableContainer component={Paper}>
                        <Table aria-label="Weather table">
                            <TableHead>
                                <TableRow>
                                    <TableCell>Дата</TableCell>
                                    {
                                        this.state.weather.map((w) =>
                                            <TableCell>{w.date}</TableCell>
                                        )
                                    }
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                <TableRow>
                                    <TableCell>Max t(c)</TableCell>
                                    {
                                        this.state.weather.map((w) =>
                                            <TableCell>{w.maxTemp}</TableCell>
                                        )
                                    }
                                </TableRow>
                                <TableRow>
                                    <TableCell>Min t(c)</TableCell>
                                    {
                                        this.state.weather.map((w) =>
                                            <TableCell>{w.minTemp}</TableCell>
                                        )
                                    }
                                </TableRow>
                            </TableBody>
                        </Table>
                    </TableContainer> : <em>Loading...</em>}
            </div>
        );
    }
}
