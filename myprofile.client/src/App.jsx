import { Routes, Route, BrowserRouter as Router } from "react-router-dom";
import Home from "./pages/Home";
import Login from "./pages/Login";
import Dashboard from "./pages/Dashboard";
import AdminPanel from "./pages/AdminPanel";
import StudentProfile from "./pages/StudentProfile";
import Unauthorized from "./pages/Unauthorized";
import NotFound from "./pages/NotFound";
import RequireAuth from "./components/RequireAuth";
import RequireRole from "./components/RequireRole";
import { AuthProvider } from "./context/AuthProvider"; // ❗ добавен import

function App() {
    return (
        <AuthProvider>
            <Router>
                <Routes>
                    {/* 🟢 Публични маршрути */}
                    <Route path="/" element={<Home />} />
                    <Route path="/login" element={<Login />} />
                    <Route path="/unauthorized" element={<Unauthorized />} />

                    {/* 🔐 Защитени маршрути */}
                    <Route element={<RequireAuth />}>
                        <Route path="/dashboard" element={<Dashboard />} />
                        <Route path="/students/:id" element={<StudentProfile />} />
                    </Route>

                    {/* 🔐 Само за админ */}
                    <Route element={<RequireRole allowedRoles={["admin"]} />}>
                        <Route path="/admin" element={<AdminPanel />} />
                    </Route>

                    {/* 🧭 Fallback */}
                    <Route path="*" element={<NotFound />} />
                </Routes>
            </Router>
        </AuthProvider>
    );
}

export default App;
