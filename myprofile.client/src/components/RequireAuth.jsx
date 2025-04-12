import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../context/AuthProvider";

export default function RequireAuth() {
    const { user, token } = useAuth();

    if (token && !user) {
        return <div className="text-center mt-20 text-gray-500">Зареждане...</div>;
    }

    if (!token || !user) {
        return <Navigate to="/login" replace />;
    }

    return <Outlet />;
}