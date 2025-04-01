import { useState } from "react";
import api from "@/services/api.js";

export default function AchievementUploadForm({ studentId, onUpload }) {
    const [formData, setFormData] = useState({
        title: "",
        details: "",
        date: "",
        proof: null,
    });
    const [error, setError] = useState(null);

    const handleChange = (e) => {
        const { name, value, files } = e.target;
        if (name === "proof") {
            setFormData((prev) => ({ ...prev, proof: files[0] }));
        } else {
            setFormData((prev) => ({ ...prev, [name]: value }));
        }
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        const data = new FormData();
        data.append("title", formData.title);
        data.append("details", formData.details);
        data.append("date", formData.date);
        if (formData.proof) data.append("proof", formData.proof);

        api
            .post(`/students/${studentId}/achievements`, data, {
                headers: {
                    "Content-Type": "multipart/form-data",
                },
            })
            .then((res) => {
                onUpload?.(res.data);
                setFormData({
                    title: "",
                    details: "",
                    date: "",
                    proof: null,
                });
                setError(null);
            })
            .catch((err) => {
                console.error("Upload error:", err);
                setError("Неуспешно качване на постижение.");
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
                    name="details"
                    value={formData.details}
                    onChange={handleChange}
                    className="w-full border rounded p-2"
                ></textarea>
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
            <div>
                <label className="block">Доказателство:</label>
                <input
                    type="file"
                    name="proof"
                    accept="image/*,application/pdf"
                    onChange={handleChange}
                    className="w-full border rounded p-2"
                />
            </div>
            {error && <p className="text-red-600">{error}</p>}
            <button
                type="submit"
                className="bg-green-600 text-white px-4 py-2 rounded hover:bg-green-700"
            >
                Качи постижение
            </button>
        </form>
    );
}
