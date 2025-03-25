import WeatherForecast from "./components/WeatherForecast";
import CreditUploadForm from "./components/CreditUploadForm";
import CreditList from "./components/CreditList";
import ProjectUploadForm from "./components/ProjectUploadForm";

function App() {
    return (
        <div className="App">
            <h1>Моят профил</h1>
            <CreditUploadForm />
            <hr />
            <CreditList />
            <hr />
            <WeatherForecast />
            <hr />
            <ProjectUploadForm />
        </div>
    );
}

export default App;
