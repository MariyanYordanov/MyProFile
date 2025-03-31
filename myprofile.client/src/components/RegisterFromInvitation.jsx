import { useState } from "react";
import axios from "axios";

export default function RegisterFromInvitation() {
    const [formData, setFormData] = useState({
        email: "",
        username: "",
        password: "",
        confirmPassword: ""
    });

    const [error, setError] = useState(null);
    const [success, setSuccess] = useState(false);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prev) => ({ ...prev, [name]: value }));
    };

    const validatePassword = (password) => {
        const regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\w\s]).{8,}$/;
        return regex.test(password);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError(null);
        setSuccess(false);

        const { password, confirmPassword } = formData;

        if (password !== confirmPassword) {
            setError("Паролите не съвпадат.");
            return;
        }

        if (!validatePassword(password)) {
            setError("Паролата трябва да е поне 8 символа, да съдържа главна и малка буква, цифра и специален символ.");
            return;
        }

        try {
            const { confirmPassword, ...payload } = formData;
            await axios.post("/auth/register", payload);
            setSuccess(true);
        } catch (err) {
            setError("Грешка при регистрация. Проверете имейла и поканата.");
        }
    };

    return (
        <div className="max-w-md mx-auto p-6 bg-white shadow-md rounded">
            <h2 className="text-2xl font-bold mb-4">Регистрация с покана</h2>

            {success && (
                <p className="text-green-600 mb-4">
                    Успешна регистрация! Моля, потвърдете имейла си.
                </p>
            )}
            {error && <p className="text-red-500 mb-4">{error}</p>}

            <form onSubmit={handleSubmit} className="space-y-4">
                <div>
                    <label className="block mb-1">Имейл</label>
                    <input
                        type="email"
                        name="email"
                        value={formData.email}
                        onChange={handleChange}
                        className="w-full border p-2 rounded"
                        required
                    />
                </div>
                <div>
                    <label className="block mb-1">Потребителско име</label>
                    <input
                        type="text"
                        name="username"
                        value={formData.username}
                        onChange={handleChange}
                        className="w-full border p-2 rounded"
                        required
                    />
                </div>
                <div>
                    <label className="block mb-1">Парола</label>
                    <input
                        type="password"
                        name="password"
                        value={formData.password}
                        onChange={handleChange}
                        className="w-full border p-2 rounded"
                        autoComplete="new-password"
                        required
                    />
                </div>
                <div>
                    <label className="block mb-1">Потвърди паролата</label>
                    <input
                        type="password"
                        name="confirmPassword"
                        value={formData.confirmPassword}
                        onChange={handleChange}
                        className="w-full border p-2 rounded"
                        autoComplete="new-password"
                        required
                    />
                </div>
                <button
                    type="submit"
                    className="w-full bg-blue-600 text-white py-2 rounded hover:bg-blue-700 transition"
                >
                    Регистрирай се
                </button>
            </form>
        </div>
    );
}
