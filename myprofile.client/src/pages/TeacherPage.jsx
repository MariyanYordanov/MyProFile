import { useEffect, useState } from "react";
import api from "@/services/api";
import { useAuth } from "@/context/AuthProvider";
import { Link } from "react-router-dom";

export default function TeacherPage() {
    const { user, token } = useAuth();
    const [students, setStudents] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {
        if (!token || !token.includes(".")) return; 

        const fetchStudents = async () => {
            try {
                const res = await api.get("/teachers/students");
                setStudents(res.data);
            } catch (err) {
                console.error("Грешка при зареждане на ученици:", err);
                setError("Неуспешно зареждане на ученици.");
            } finally {
                setLoading(false);
            }
        };

        fetchStudents();
    }, [token]);

    return (
        <div className="max-w-5xl mx-auto mt-10">
            <h1 className="text-2xl font-bold mb-4">👨‍🏫 Добре дошъл, {user?.email}</h1>

            {loading ? (
                <p className="text-gray-500">Зареждане на ученици...</p>
            ) : error ? (
                <p className="text-red-500">{error}</p>
            ) : (
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                    {students.map((s) => (
                        <div key={s.student.id} className="border rounded p-4 shadow">
                            <h3 className="font-semibold">{s.student.fullName}</h3>
                            <p className="text-sm text-gray-500">
                                {s.student.class} клас • {s.student.speciality}
                            </p>
                            <p className="text-sm text-gray-700 mt-2">
                                📌 <b>{s.pendingCount}</b> заявки за одобрение
                            </p>
                            <Link
                                to={`/students/${s.student.id}`}
                                className="inline-block mt-2 text-blue-600 hover:underline"
                            >
                                Прегледай профила →
                            </Link>
                        </div>
                    ))}
                </div>
            )}
        </div>
    );
}
