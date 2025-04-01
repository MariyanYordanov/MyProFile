import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import api from "@/services/api.js";


export default function AdminPanel() {
    const [users, setUsers] = useState([]);

    useEffect(() => {
        const loadUsers = async () => {
            try {
                const res = await api.get("/users");
                setUsers(res.data);
            } catch (err) {
                console.error("Грешка при зареждане на потребители:", err);
            }
        };
        loadUsers();
    }, []);

    return (
        <div className="p-6">
            <h1 className="text-2xl font-bold mb-4">Админ панел</h1>
            <ul className="space-y-2">
                {users.map((user) => (
                    <li key={user.id} className="bg-gray-100 p-4 rounded">
                        <p><strong>{user.username}</strong> – {user.role}</p>
                        <Link
                            className="text-blue-500 hover:underline"
                            to={`/students/${user.id}`}
                        >
                            Прегледай профил
                        </Link>
                    </li>
                ))}
            </ul>
        </div>
    );
}
