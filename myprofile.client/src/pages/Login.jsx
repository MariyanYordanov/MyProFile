import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "@/services/api";
import { useAuth } from "@/context/AuthProvider";
import { jwtDecode } from "jwt-decode";

export default function Login() {
    const navigate = useNavigate();
    const { login } = useAuth();
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError("");

        try {
            const res = await api.post("/auth/login", { email, password });
            const { token } = res.data;

            login(token);
            const decoded = jwtDecode(token);
            const role = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

            if (role === "teacher") navigate("/teachers");
            else if (role === "admin") navigate("/admin");
            else navigate("/dashboard");

        } catch (err) {
            console.error("Login error:", err);
            setError("Невалидни данни или проблем със сървъра.");
        }
    };

    return (
        <div className="max-w-md mx-auto mt-20 p-6 border rounded shadow">
            <h2 className="text-2xl font-bold mb-4 text-center">Вход</h2>
            {error && <p className="text-red-500 text-sm mb-4">{error}</p>}
            <form onSubmit={handleSubmit}>
                <input
                    type="email"
                    placeholder="Имейл"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    required
                    className="w-full p-2 border mb-4 rounded"
                />
                <input
                    type="password"
                    placeholder="Парола"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    required
                    className="w-full p-2 border mb-4 rounded"
                />
                <button
                    type="submit"
                    className="w-full bg-blue-600 text-white py-2 rounded"
                >
                    Вход
                </button>
            </form>
        </div>
    );
}
