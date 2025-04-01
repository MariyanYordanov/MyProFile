import { useState } from "react";
import api from "@/services/api.js";


export default function EventUploadForm({ studentId, onEventAdded }) {
    const [formData, setFormData] = useState({
        title: "",
        description: "",
        date: "",
    });
    const [error, setError] = useState(null);

    const handleChange = (e) => {
        setFormData((prev) => ({
            ...prev,
            [e.target.name]: e.target.value,
        }));
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        api
            .post(`/students/${studentId}/events`, formData)
            .then((res) => {
                onEventAdded?.(res.data);
                setFormData({ title: "", description: "", date: "" });
                setError(null);
            })
            .catch((err) => {
                console.error("Event upload error:", err);
                setError("Грешка при изпращане на събитието.");
            });
    };

    return (
        <form onSubmit={handleSubmit} className="space-y-4">
            <div>
                <label className="block">Заглавие:</label>
                <input
                    type="text"
                    name="title"
                    value={formData.title}
                    onChange={handleChange}
                    required
                    className="w-full border rounded p-2"
                />
            </div>
            <div>
                <label className="block">Описание:</label>
                <textarea
                    name="description"
                    value={formData.description}
                    onChange={handleChange}
                    className="w-full border rounded p-2"
                />
            </div>
            <div>
                <label className="block">Дата:</label>
                <input
                    type="date"
                    name="date"
                    value={formData.date}
                    onChange={handleChange}
                    required
                    className="w-full border rounded p-2"
                />
            </div>
            {error && <div className="text-red-500">{error}</div>}
            <button
                type="submit"
                className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
            >
                Запиши събитие
            </button>
        </form>
    );
}
