import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import axios from "axios";

export default function AdminPanel() {
    const [students, setStudents] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        axios.get("/students")
            .then(res => setStudents(res.data))
            .catch(err => setError("Грешка при зареждане на ученици."))
            .finally(() => setLoading(false));
    }, []);

    if (loading) return <p className="p-6">Зареждане на ученици...</p>;
    if (error) return <p className="p-6 text-red-500">{error}</p>;

    return (
        <div className="p-6">
            <h2 className="text-2xl font-bold mb-4">Админ панел</h2>

            <div className="space-y-3 mb-6">
                <Link to="/admin/send-invite" className="block text-blue-600 underline">
                    Изпращане на покана
                </Link>
            </div>

            <h3 className="text-xl font-semibold mb-2">Списък с ученици</h3>
            <div className="overflow-x-auto">
                <table className="min-w-full table-auto border">
                    <thead className="bg-gray-100">
                        <tr>
                            <th className="px-4 py-2 text-left">ID</th>
                            <th className="px-4 py-2 text-left">Потребител</th>
                            <th className="px-4 py-2 text-left">Имейл</th>
                            <th className="px-4 py-2 text-left">Роля</th>
                            <th className="px-4 py-2">Действие</th>
                        </tr>
                    </thead>
                    <tbody>
                        {students.map(student => (
                            <tr key={student.id} className="border-t">
                                <td className="px-4 py-2">{student.id}</td>
                                <td className="px-4 py-2">{student.username}</td>
                                <td className="px-4 py-2">{student.email}</td>
                                <td className="px-4 py-2">{student.role}</td>
                                <td className="px-4 py-2 text-center">
                                    <Link
                                        to={`/students/${student.id}`}
                                        className="text-blue-600 underline"
                                    >
                                        Профил
                                    </Link>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
}
