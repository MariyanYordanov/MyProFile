import { useAuth } from "@/context/AuthProvider";
import { Link } from "react-router-dom";

export default function Dashboard() {
    const { auth } = useAuth();

    if (!auth?.user) {
        return (
            <div className="text-center p-10">
                <h2 className="text-xl">Грешка</h2>
                <p>Няма активен потребител.</p>
                <Link to="/login" className="text-blue-500 underline">
                    Влез в системата
                </Link>
            </div>
        );
    }

    const { username, email, role } = auth.user;

    return (
        <div className="p-10">
            <h1 className="text-2xl mb-4">Добре дошъл, {username}!</h1>
            <p className="mb-2">Имейл: {email}</p>
            <p className="mb-4">Роля: {role}</p>

            {role === "admin" && (
                <div className="bg-gray-100 p-4 rounded shadow">
                    <h2 className="text-lg font-semibold mb-2">Администраторски панел</h2>
                    <Link to="/admin" className="text-blue-600 underline">
                        Влез в панела
                    </Link>
                </div>
            )}

            {role === "teacher" && (
                <div className="bg-green-100 p-4 rounded shadow">
                    <h2 className="text-lg font-semibold mb-2">Учителски интерфейс</h2>
                    <p>Очаквай скоро – преглед на ученици, верификация и статистика.</p>
                </div>
            )}

            {role === "student" && (
                <div className="bg-blue-100 p-4 rounded shadow">
                    <h2 className="text-lg font-semibold mb-2">Ученическо портфолио</h2>
                    <Link to={`/student-profile/${auth.user.id}`} className="text-blue-600 underline">
                        Виж своя профил
                    </Link>
                </div>
            )}

            {role === "guest" && (
                <div className="bg-yellow-100 p-4 rounded shadow">
                    <h2 className="text-lg font-semibold mb-2">Гост достъп</h2>
                    <p>Разглеждаш публична информация. Ако имаш покана, регистрирай се.</p>
                </div>
            )}
        </div>
    );
}
