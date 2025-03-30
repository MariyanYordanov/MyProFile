import { useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { jwtDecode } from "jwt-decode";
import { useAuth } from "../context/AuthProvider";

export default function Login() {
    const [form, setForm] = useState({ email: "", password: "" });
    const [error, setError] = useState("");
    const navigate = useNavigate();
    const { setUser } = useAuth();

    const handleChange = (e) => {
        setForm({ ...form, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError("");
        try {
            const res = await axios.post("/auth/login", form);
            const token = res.data.token;
            const refreshToken = res.data.refreshToken;

            localStorage.setItem("token", token);
            localStorage.setItem("refreshToken", refreshToken);

            const payload = jwtDecode(token);
            setUser({
                id: payload.nameid,
                email: payload.email,
                username: payload.unique_name,
                role: payload.role
            });

            if (payload.role === "admin") navigate("/admin");
            else if (payload.role === "student") navigate(`/students/${payload.nameid}`);
            else if (payload.role === "teacher") navigate("/teacher");
            else navigate("/guest");

        } catch (err) {
            setError("Грешен имейл или парола.");
        }
    };

    return (
        <form onSubmit={handleSubmit} className="max-w-md mx-auto mt-10 space-y-4">
            <h2 className="text-xl font-bold">Вход</h2>
            <input type="email" name="email" value={form.email} onChange={handleChange} placeholder="Имейл" className="w-full border p-2" />
            <input type="password" name="password" value={form.password} onChange={handleChange} placeholder="Парола" className="w-full border p-2" />
            {error && <div className="text-red-600 text-sm">{error}</div>}
            <button type="submit" className="bg-blue-600 text-white py-2 px-4 rounded">Влез</button>
        </form>
    );
}
