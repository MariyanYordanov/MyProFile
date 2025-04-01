// src/pages/Login.jsx
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthProvider";
import api from "@/services/api";
import { jwtDecode } from "jwt-decode";

export default function Login() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");
    const navigate = useNavigate();
    const { login } = useAuth();

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError("");

        try {
            const res = await api.post("/auth/login", { email, password });
            const token = res.data.token;

            try {
                jwtDecode(token); // проверка дали е валиден JWT
            } catch {
                setError("Получен е невалиден токен от сървъра.");
                return;
            }

            localStorage.setItem("token", token);
            localStorage.setItem("refreshToken", res.data.refreshToken || "");

            login(token); // подава се само ако е валиден
            navigate("/dashboard");
        } catch (err) {
            setError("Невалидни данни за вход");
        }
    };

    return (
        <div className="flex flex-col items-center justify-center min-h-screen bg-gray-100">
            <form
                onSubmit={handleSubmit}
                className="bg-white p-6 rounded shadow-md w-80"
            >
                <h2 className="text-2xl font-bold mb-4 text-center">Вход</h2>

                <input
                    type="email"
                    placeholder="Имейл"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    required
                    className="mb-4 p-2 border rounded w-full"
                />

                <input
                    type="password"
                    placeholder="Парола"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    required
                    className="mb-4 p-2 border rounded w-full"
                />

                {error && <div className="text-red-500 mb-2 text-sm">{error}</div>}

                <button
                    type="submit"
                    className="bg-blue-600 text-white p-2 rounded w-full hover:bg-blue-700"
                >
                    Вход
                </button>
            </form>
        </div>
    );
}
