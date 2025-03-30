import { useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";

export default function Login() {
    const [form, setForm] = useState({ email: "", password: "" });
    const [error, setError] = useState("");
    const navigate = useNavigate();

    const handleChange = (e) => {
        setForm({ ...form, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError("");
        try {
            const res = await axios.post("/api/auth/login", form);
            const token = res.data.token;
            localStorage.setItem("token", token);

            // Декодиране на токена за да получим ролята и ID-то
            const payload = JSON.parse(atob(token.split(".")[1]));

            if (payload.role === "admin") {
                navigate("/admin");
            } else if (payload.role === "student") {
                navigate(`/students/${payload.nameid}`); // nameid е user.Id
            } else {
                navigate("/"); // fallback
            }
        } catch (err) {
            setError("Грешен имейл или парола.");
        }
    };

    return (
        <div className="max-w-md mx-auto mt-10 p-4 border rounded shadow">
            <h2 className="text-2xl font-bold mb-4">Вход</h2>
            <form onSubmit={handleSubmit} className="space-y-4">
                <input
                    type="email"
                    name="email"
                    placeholder="Имейл"
                    value={form.email}
                    onChange={handleChange}
                    className="w-full p-2 border rounded"
                    autoComplete="email"
                    required
                />
                <input
                    type="password"
                    name="password"
                    placeholder="Парола"
                    value={form.password}
                    onChange={handleChange}
                    className="w-full p-2 border rounded"
                    autoComplete="current-password"
                    required
                />
                {error && <p className="text-red-500 text-sm">{error}</p>}
                <button type="submit" className="bg-blue-600 text-white py-2 px-4 rounded">
                    Влез
                </button>
            </form>
        </div>
    );
}
