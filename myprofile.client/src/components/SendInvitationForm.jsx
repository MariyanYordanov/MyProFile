import { useState } from "react";
import axios from "axios";

export default function SendInvitationForm() {
    const [email, setEmail] = useState("");
    const [role, setRole] = useState("student");
    const [message, setMessage] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await axios.post("/api/invitations/send", form, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem("token")}`
                }
            });
            setMessage("Поканата е изпратена успешно.");
        } catch (err) {
            setMessage("Грешка при изпращането.");
        }
    };

    return (
        <form onSubmit={handleSubmit} className="max-w-md mx-auto space-y-4">
            <h2 className="text-xl font-bold">Изпрати покана</h2>
            <input
                type="email"
                placeholder="Имейл"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                required
                className="w-full p-2 border rounded"
            />
            <select
                value={role}
                onChange={(e) => setRole(e.target.value)}
                className="w-full p-2 border rounded"
            >
                <option value="student">Ученик</option>
                <option value="teacher">Учител</option>
                <option value="admin">Администратор</option>
            </select>
            <button type="submit" className="bg-blue-600 text-white py-2 px-4 rounded">
                Изпрати покана
            </button>
            {message && <p className="mt-2 text-green-600">{message}</p>}
        </form>
    );
}
