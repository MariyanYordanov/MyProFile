import { useState } from "react";
import { useSearchParams, useNavigate } from "react-router-dom";
import axios from "axios";

export default function RegisterFromInvitation() {
    const [searchParams] = useSearchParams();
    const token = searchParams.get("token");

    const [form, setForm] = useState({
        username: "",
        email: "",
        password: "",
    });
    const [error, setError] = useState("");
    const [success, setSuccess] = useState(false);
    const [loading, setLoading] = useState(false);

    const navigate = useNavigate();

    const handleChange = (e) => {
        setForm({ ...form, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError("");
        setLoading(true);

        try {
            const res = await axios.post("/api/auth/register", {
                ...form,
                token,
            });
            setSuccess(true);
            setTimeout(() => navigate("/login"), 2000);
        } catch (err) {
            const message = err.response?.data || "Грешка при регистрация.";
            setError(message);
        }
    };

    if (!token) {
        return <div className="text-red-500">Невалиден или липсващ токен за регистрация.</div>;
    }

    return (
        <div className="max-w-md mx-auto mt-10 p-4 border rounded-xl shadow">
            <h2 className="text-2xl font-bold mb-4">Регистрация чрез покана</h2>
            {success ? (
                <p className="text-green-600">Регистрацията е успешна! Пренасочване...</p>
            ) : (
                <form onSubmit={handleSubmit} className="space-y-4">
                    <input
                        type="text"
                        name="username"
                        placeholder="Потребителско име"
                        value={form.username}
                        onChange={handleChange}
                        className="w-full p-2 border rounded"
                            required
                            autoFocus
                    />
                    <input
                        type="email"
                        name="email"
                        placeholder="Имейл адрес"
                        value={form.email}
                        onChange={handleChange}
                        className="w-full p-2 border rounded"
                        required
                    />
                    <input
                        type="password"
                        name="password"
                        placeholder="Парола"
                        value={form.password}
                        onChange={handleChange}
                        className="w-full p-2 border rounded"
                        required
                    />
                    {error && <p className="text-red-500 text-sm">{error}</p>}
                        <button
                            type="submit"
                            className="bg-blue-600 text-white py-2 px-4 rounded">
                            {loading ? "Изпращане...": "Регистрирай се"}
                    </button>
                </form>
            )}
        </div>
    );
}
