import { useState } from "react";
import api from "@/services/api.js";

export default function InterestForm({ studentId, onAdded }) {
    const [interest, setInterest] = useState("");
    const [error, setError] = useState(null);

    const handleSubmit = (e) => {
        e.preventDefault();

        api
            .post(`/students/${studentId}/interests`, { name: interest })
            .then((res) => {
                onAdded?.(res.data);
                setInterest("");
                setError(null);
            })
            .catch((err) => {
                console.error("Error adding interest:", err);
                setError("Грешка при добавяне на интерес.");
            });
    };

    return (
        <form onSubmit={handleSubmit} className="space-y-4">
            <div>
                <label className="block">Интерес:</label>
                <input
                    type="text"
                    name="interest"
                    value={interest}
                    onChange={(e) => setInterest(e.target.value)}
                    required
                    className="w-full border rounded p-2"
                />
            </div>
            {error && <p className="text-red-600">{error}</p>}
            <button
                type="submit"
                className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
            >
                Добави интерес
            </button>
        </form>
    );
}
