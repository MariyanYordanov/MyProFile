import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../context/AuthProvider";

export default function RequireRole({ allowedRoles = [] }) {
    const { user, loading } = useAuth();

    if (loading) return <div className="text-center mt-10">Зареждане...</div>;
    if (!user) return <Navigate to="/login" replace />;
    if (!allowedRoles.includes(user.role)) return <Navigate to="/" replace />;

    return <Outlet />;
}