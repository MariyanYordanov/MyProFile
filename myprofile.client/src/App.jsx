import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import StudentOverviewPage from "./pages/StudentOverviewPage";

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
        <Router>
            <div className="App">
                <Routes>
                    <Route path="/students/:id/overview" element={<StudentOverviewPage />} />
                    {/* Други маршрути могат да бъдат добавени тук */}
                </Routes>

                {/* Тестови компоненти на една страница (временно) */}
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
                <ProfilePictureUploadForm studentId={1} />
            </div>
        </Router>
    );
}

export default App;
