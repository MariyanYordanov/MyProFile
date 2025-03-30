import { NavLink, Outlet, Navigate } from "react-router-dom";

export default function AdminLayout({ user }) {
    if (!user || user.role !== "admin") {
        return <Navigate to="/login" replace />;
    }

    return (
        <div className="min-h-screen flex flex-col">
            <header className="bg-gray-800 text-white p-4">
                <h1 className="text-xl font-bold">Админ панел</h1>
                <nav className="mt-2 space-x-4">
                    <NavLink to="/admin" className={({ isActive }) => isActive ? "underline text-blue-300" : "hover:underline"}>
                        Табло
                    </NavLink>
                    <NavLink to="/admin/send-invite" className={({ isActive }) => isActive ? "underline text-blue-300" : "hover:underline"}>
                        Изпрати покана
                    </NavLink>
                </nav>
            </header>
            <main className="flex-1 p-6 bg-gray-100">
                <Outlet />
            </main>
        </div>
    );
}
