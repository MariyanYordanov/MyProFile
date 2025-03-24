import { useEffect, useState } from "react";

export default function WeatherForecast() {
    const [forecasts, setForecasts] = useState();

    useEffect(() => {
        populateWeatherData();
    }, []);

    const populateWeatherData = async () => {
        const response = await fetch("/weatherforecast");
        if (response.ok) {
            const data = await response.json();
            setForecasts(data);
        }
    };

    const contents =
        forecasts === undefined ? (
            <p>
                <em>Loading... Please refresh once the backend has started.</em>
            </p>
        ) : (
            <table className="table table-striped" aria-labelledby="tableLabel">
                <thead>
                    <tr>
                        <th>Date</th>
                        <th>Temp. (C)</th>
                        <th>Temp. (F)</th>
                        <th>Summary</th>
                    </tr>
                </thead>
                <tbody>
                    {forecasts.map((forecast) => (
                        <tr key={forecast.date}>
                            <td>{forecast.date}</td>
                            <td>{forecast.temperatureC}</td>
                            <td>{forecast.temperatureF}</td>
                            <td>{forecast.summary}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        );

    return (
        <div>
            <h2 id="tableLabel">Weather forecast</h2>
            {contents}
        </div>
    );
}
