import { Routes, Route } from "react-router-dom";

import Home from "@/pages/Home";
import Login from "@/pages/Login";
import Dashboard from "@/pages/Dashboard";
import AdminPanel from "@/pages/AdminPanel";
import StudentProfile from "@/pages/StudentProfile";
import Unauthorized from "@/pages/Unauthorized";
import NotFound from "@/pages/NotFound";
import GuestPage from "@/pages/GuestPage";
import TeacherPage from "@/pages/TeacherPage";

import RegisterFromInvitation from "@/components/RegisterFromInvitation";
import RequireAuth from "@/components/RequireAuth";
import RequireRole from "@/components/RequireRole";

export default function App() {
    console.log("App loaded");

    return (
        <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/login" element={<Login />} />
            <Route path="/unauthorized" element={<Unauthorized />} />
            <Route path="/register/:token" element={<RegisterFromInvitation />} />
            <Route path="/guest" element={<GuestPage />} />

            <Route element={<RequireAuth />}>
                <Route path="/dashboard" element={<Dashboard />} />
                <Route path="/students/:id" element={<StudentProfile />} />

                <Route element={<RequireRole allowedRoles={["admin"]} />}>
                    <Route path="/admin" element={<AdminPanel />} />
                </Route>

                <Route element={<RequireRole allowedRoles={["teacher"]} />}>
                    <Route path="/teachers" element={<TeacherPage />} />
                </Route>
            </Route>

            <Route path="*" element={<NotFound />} />
        </Routes>
    );
}
