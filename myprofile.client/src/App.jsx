import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Home from "./components/Home";
import Login from "./components/Login";
import RegisterFromInvitation from "./components/RegisterFromInvitation";
import SendInvitationForm from "./components/SendInvitationForm";
import AdminPanel from "./components/AdminPanel";
import AdminLayout from "./components/AdminLayout";
import StudentProfile from "./components/StudentProfile";
import RequireAuth from "./components/RequireAuth";
import RequireRole from "./components/RequireRole";
import { useAuth } from "./context/AuthProvider";
import TeacherPage from "./pages/TeacherPage";
import GuestPage from "./pages/GuestPage";
import StudentOverviewPage from "./pages/StudentOverviewPage"; // ✅ важен import

function App() {
    const { user } = useAuth();

    return (
        <Router>
            <Routes>
                {/* Публични маршрути */}
                <Route path="/" element={<Home />} />
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<RegisterFromInvitation />} />

                {/* Защитени маршрути */}
                <Route element={<RequireAuth />}>
                    <Route path="/students/:id" element={<StudentProfile />} />
                    <Route path="/profile" element={<StudentProfile />} />
                    <Route path="/teacher" element={<TeacherPage />} />
                    <Route path="/guest" element={<GuestPage />} />
                </Route>

                {/* Админ маршрути */}
                <Route element={<RequireRole allowedRoles={["admin"]} />}>
                    <Route path="/admin" element={<AdminLayout user={user} />}>
                        <Route index element={<AdminPanel />} />
                        <Route path="send-invite" element={<SendInvitationForm />} />
                    </Route>
                </Route>
            </Routes>
        </Router>
    );
}

export default App;
