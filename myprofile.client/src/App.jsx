import { useEffect, useState } from "react";
import { Route, BrowserRouter as Router, Routes } from "react-router-dom";
import Home from "./components/Home";
import Login from "./components/Login";
import RegisterFromInvitation from "./components/RegisterFromInvitation";
import SendInvitationForm from "./components/SendInvitationForm";
import AdminPanel from "./components/AdminPanel";
import AdminLayout from "./components/AdminLayout";
import StudentProfile from "./components/StudentProfile";

function App() {
    const [user, setUser] = useState(null);

    useEffect(() => {
        const token = localStorage.getItem("token");
        if (token) {
            try {
                const payload = JSON.parse(atob(token.split(".")[1]));
                setUser({
                    username: payload.name,
                    email: payload.email,
                    role: payload.role,
                });
            } catch (e) {
                console.error("Невалиден токен:", e);
                localStorage.removeItem("token");
            }
        }
    }, []);

    return (
        <Router>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/login" element={<Login setUser={setUser} />} />
                <Route path="/register" element={<RegisterFromInvitation />} />
                <Route path="/students/:id" element={<StudentProfile />} />
                <Route path="/profile" element={<StudentProfile />} />
                {/* временно само за тест */}
                <Route path="/admin" element={<AdminLayout user={user ?? { role: "admin" }} />}>
                    <Route index element={<AdminPanel />} />
                    <Route path="send-invite" element={<SendInvitationForm />} />
                </Route>
                {/*<Route path="/admin" element={<AdminLayout user={user} />}>*/}
                {/*    <Route index element={<AdminPanel />} />*/}
                {/*    <Route path="send-invite" element={<SendInvitationForm />} />*/}
                {/*</Route>*/}
                {/*{user?.role === "admin" && (*/}
                {/*    <Route path="/admin" element={<AdminLayout user={user} />}>*/}
                {/*        <Route index element={<AdminPanel />} />*/}
                {/*        <Route path="send-invite" element={<SendInvitationForm />} />*/}
                {/*    </Route>*/}
                {/*)}*/}
            </Routes>
        </Router>
    );
}

export default App;
