import { Link, Route, BrowserRouter as Router, Routes } from "react-router-dom";
import AdminPanel from "./pages/AdminPanel"; 
import StudentOverviewPage from "./pages/StudentOverviewPage";

function App() {
    return (
        <Router>
            <div className="p-4">
                <nav className="space-x-4 mb-4">
                    <Link to="/">🏠 Начало</Link>
                    <Link to="/admin">🛠️ Админ панел</Link>
                    <Link to="/students/1/overview">👤 Преглед на ученик</Link>
                </nav>

                <Routes>
                    <Route path="/" element={<p>Добре дошъл в MyProFile</p>} />
                    <Route path="/admin" element={<AdminPanel />} />
                    <Route path="/students/:id/overview" element={<StudentOverviewPage />} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;
