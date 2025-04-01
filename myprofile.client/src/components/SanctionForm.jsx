import { useState } from "react";
import api from "@/services/api.js";


export default function SanctionForm({ studentId, onSanctionAdded }) {
    const [formData, setFormData] = useState({
        reason: "",
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
            .post(`/students/${studentId}/sanctions`, formData)
            .then((res) => {
                onSanctionAdded?.(res.data);
                setFormData({ reason: "", date: "" });
                setError(null);
            })
            .catch((err) => {
                console.error("Sanction error:", err);
                setError("Възникна грешка при добавяне на санкцията.");
            });
    };

    return (
        <form onSubmit={handleSubmit} className="space-y-4">
            <div>
                <label className="block">Причина:</label>
                <input
                    type="text"
                    name="reason"
                    value={formData.reason}
                    onChange={handleChange}
                    required
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
            {error && <p className="text-red-600">{error}</p>}
            <button
                type="submit"
                className="bg-red-600 text-white px-4 py-2 rounded hover:bg-red-700"
            >
                Добави санкция
            </button>
        </form>
    );
}
