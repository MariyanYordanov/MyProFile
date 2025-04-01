import React, { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import api from "@/services/api.js";


export default function RegisterFromInvitation() {
    const navigate = useNavigate();
    const { token } = useParams();

    const [formData, setFormData] = useState({
        username: "",
        email: "",
        password: "",
        confirmPassword: "",
    });

    const [error, setError] = useState("");

    const handleChange = (e) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value,
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError("");

        if (formData.password !== formData.confirmPassword) {
            setError("Паролите не съвпадат.");
            return;
        }

        try {
            await api.post("/auth/register-from-invitation", {
                token,
                username: formData.username,
                email: formData.email,
                password: formData.password,
            });
            navigate("/login");
        } catch (err) {
            console.error(err);
            setError(
                err.response?.data ||
                "Възникна грешка при регистрацията. Моля, опитайте отново."
            );
        }
    };

    return (
        <div className="max-w-md mx-auto mt-10">
            <h2 className="text-xl font-bold mb-4">Регистрация с покана</h2>
            {error && <div className="text-red-500 mb-4">{error}</div>}
            <form onSubmit={handleSubmit} className="space-y-4">
                <input
                    type="text"
                    name="username"
                    value={formData.username}
                    onChange={handleChange}
                    placeholder="Потребителско име"
                    className="w-full p-2 border rounded"
                />
                <input
                    type="email"
                    name="email"
                    value={formData.email}
                    onChange={handleChange}
                    placeholder="Имейл"
                    className="w-full p-2 border rounded"
                />
                <input
                    type="password"
                    name="password"
                    value={formData.password}
                    onChange={handleChange}
                    placeholder="Парола"
                    className="w-full p-2 border rounded"
                />
                <input
                    type="password"
                    name="confirmPassword"
                    value={formData.confirmPassword}
                    onChange={handleChange}
                    placeholder="Потвърди паролата"
                    className="w-full p-2 border rounded"
                />
                <button
                    type="submit"
                    className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
                >
                    Регистрирай се
                </button>
            </form>
        </div>
    );
}