import CreditUploadForm from "./components/CreditUploadForm";
import CreditList from "./components/CreditList";
import ProjectUploadForm from "./components/ProjectUploadForm";
import EventUploadForm from "./components/EventUploadForm";
import GoalForm from "./components/GoalForm";

function App() {
    return (
        <div className="App">
            <h1>Моят профил</h1>
            <CreditUploadForm />
            <hr />
            <CreditList />
            <hr />
            <ProjectUploadForm />
            <hr />
            <EventUploadForm />
            <hr />
            <GoalForm />
        </div>
    );
}

export default App;
