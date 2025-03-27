import CreditUploadForm from "./components/CreditUploadForm";
import CreditList from "./components/CreditList";
import ProjectUploadForm from "./components/ProjectUploadForm";
import EventUploadForm from "./components/EventUploadForm";
import GoalForm from "./components/GoalForm";
import SanctionForm from "./components/SanctionForm";
import InterestForm from "./components/InterestForm";
import ProfilePictureUploadForm from "./components/ProfilePictureUploadForm";

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
            <hr />
            <SanctionForm />
            <hr />
            <InterestForm />
            <hr />
            <ProfilePictureUploadForm />
        </div>
    );
}

export default App;
