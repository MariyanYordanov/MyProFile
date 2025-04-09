﻿import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../context/AuthProvider";

export default function RequireAuth() {
    const { user, token } = useAuth();

    // Докато AuthProvider зарежда данни от localStorage → нищо не правим
    if (token && !user) {
        return <div className="text-center mt-20 text-gray-500">Зареждане...</div>;
    }

    if (!user) {
        return <Navigate to="/login" replace />;
    }

    return <Outlet />;
}
