import WeatherForecast from "./components/WeatherForecast";
import CreditUploadForm from "./components/CreditUploadForm";
import CreditList from "./components/CreditList";

function App() {
    return (
        <div className="App">
            <h1>Моят профил</h1>
            <CreditUploadForm />
            <hr />
            <CreditList />
            <hr />
            <WeatherForecast />
        </div>
    );
}

export default App;
